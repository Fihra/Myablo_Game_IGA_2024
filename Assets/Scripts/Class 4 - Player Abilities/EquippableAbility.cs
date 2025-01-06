using UnityEngine;

public class EquippableAbility : ClassSkill
{
    [SerializeField] protected GameObject spawnablePrefab;
    [SerializeField] protected float attackRange = 1.5f;

    protected CombatReceiver targetedReceiver;
    protected PlayerController myPlayer;

    public virtual void RunAbilityClicked(PlayerController player)
    {
        if (MouseIsOverUI()) return;
        myPlayer = player;
        targetedReceiver = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.queriesHitTriggers = false;

        if (Physics.Raycast(ray, out hit))
        {
            SuccessfulRaycastFunctionality(hit);
        }
    }

    protected virtual void SuccessfulRaycastFunctionality(RaycastHit hit)
    {
        myPlayer.Movement().MoveToLocation(hit.point);

        if (hit.collider.gameObject.GetComponent<Clickable>())
        {
            targetedReceiver = hit.collider.gameObject.GetComponent<CombatReceiver>();
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.playerAttack, transform.position);
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.enemyReceiveDamage, transform.position);
        }
    }
    protected virtual void SpawnEquippedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(spawnablePrefab, location, Quaternion.identity);
            newAttack.GetComponent<CombatActor>().SetFactionID(myPlayer.GetFactionID());

        float critMod = 1;
        int random = Random.Range(0, 100);
        float playerDex = PlayerCharacterSheet.instance.GetDexterity();
        if (random < playerDex) critMod = 2;

        float playerStrength = PlayerCharacterSheet.instance.GetStrength();
        float calculatedDamage = (playerStrength / 5) * critMod;

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
    }

    public virtual void CancelAbility()
    {
        targetedReceiver = null;
    }
    protected virtual void Update()
    {
        if (targetedReceiver != null) RunTargetAttack();
    }
    protected virtual void RunTargetAttack()
    {
        if(Vector3.Distance(myPlayer.transform.position, targetedReceiver.transform.position) <= attackRange)
        {
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);

            Vector3 lookPos = new Vector3(targetedReceiver.transform.position.x, myPlayer.transform.position.y, targetedReceiver.transform.position.z);
            myPlayer.transform.LookAt(lookPos);

            SpawnEquippedAttack(myPlayer.transform.position + myPlayer.transform.forward);
            myPlayer.GetAnimator().TriggerAttack();

            targetedReceiver = null;
        }
        else
        {
            myPlayer.Movement().MoveToLocation(targetedReceiver.transform.position);
        }
    }

    public bool MouseIsOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    public override void LevelUp()
    {
        if (PlayerCharacterSheet.instance.skillPointSpendSuccessful())
            skillLevel++;
    }

}
