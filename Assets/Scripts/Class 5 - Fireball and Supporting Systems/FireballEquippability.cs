using UnityEngine;

public class FireballEquippability : EquippableAbility
{
    [SerializeField] float manaCost = 5f;
    //public override void RunAbilityClicked(PlayerController player)
    //{
    //    myPlayer = player;
    //    targetedReceiver = null;

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    Physics.queriesHitTriggers = false;

    //    if(Physics.Raycast(ray, out hit))
    //    {

    //    }
    //}

    protected override void SuccessfulRaycastFunctionality(RaycastHit hit)
    {
        if (CanCastFireball(ref hit))
        {
            SpawnEquippedAttack(hit.point);
            myPlayer.Movement().MoveToLocation(myPlayer.transform.position);
            //Play Fireball SFX here
            myPlayer.Combat().SpendMana(manaCost);
        }
        else
        {
            myPlayer.Movement().MoveToLocation(hit.point);
        }
    }
    private bool CanCastFireball(ref RaycastHit hit)
    {
        return myPlayer.Combat().GetMana() >= manaCost && (hit.collider.gameObject.GetComponent<Clickable>() || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {
        myPlayer.GetAnimator().TriggerAttack();
        myPlayer.transform.LookAt(new Vector3(location.x, myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = myPlayer.transform.position + myPlayer.transform.forward;

        GameObject newAttack = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<FireballCA>().SetFactionID(myPlayer.GetFactionID());
        newAttack.GetComponent<FireballCA>().SetShootDirection(myPlayer.transform.forward);

        float calculatedDamage = 1 + (2 * skillLevel);
        newAttack.GetComponent<FireballCA>().InitializeDamage(calculatedDamage);

    }

}
