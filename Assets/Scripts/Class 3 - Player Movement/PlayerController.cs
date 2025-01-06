using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ClassSkillManager skillManager;

    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;

    int factionID = 1;
    bool alive = true;

    bool inDialog = false;

    bool IsInCombat = false;

    public static PlayerController instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Camera.main.gameObject.AddComponent<CameraController>();
        Camera.main.gameObject.GetComponent<CameraController>().SetFollowTarget(gameObject);
        EventsManager.instance.onDialogueStarted.AddListener(StartDialogMode);
        EventsManager.instance.onDialogueEnded.AddListener(EndDialogMode);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onDialogueStarted.RemoveListener(StartDialogMode);
        EventsManager.instance.onDialogueEnded.RemoveListener(EndDialogMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialog) return;
        if (!alive) return;

        if (Input.GetMouseButtonDown(0) && ability1 != null) UseAbility1();
        if (Input.GetMouseButtonDown(1) && ability2 != null) UseAbility2();
    }

    #region Ability Stuff
    void UseAbility1()
    {
        ability1.RunAbilityClicked(this);
    }

    void UseAbility2()
    {
        ability2.RunAbilityClicked(this);
    }

    public void SetAbility2(EquippableAbility newAbility)
    {
        ability2 = newAbility;
        EventsManager.instance.onNewAbility2Equipped.Invoke(ability2);
    }

    public EquippableAbility GetAbility2()
    {
        return ability2;
    }
    #endregion

    #region Utility
    public PlayerMovement Movement()
    {
        return GetComponent<PlayerMovement>();
    }

    public PlayerAnimator GetAnimator()
    {
        return GetComponent<PlayerAnimator>();
    }

    public PlayerCharacterSheet GetCharacterSheet()
    {
        return GetComponent<PlayerCharacterSheet>();
    }

    public PlayerCombat Combat()
    {
        return GetComponent<PlayerCombat>();
    }

    public ClassSkillManager SkillManager()
    {
        return skillManager;
    }

    public int GetFactionID()
    {
        return factionID;
    }
    
    #endregion

    public void TriggerDeath()
    {
        alive = false;
        GetAnimator().TriggerDeath();
    }

    public void TriggerRevive()
    {
        alive = true;
        GetAnimator().TriggerRevive();
    }

    #region Dialog Mode Listeners
    public void StartDialogMode()
    {
        inDialog = true;
    }

    public void EndDialogMode()
    {
        inDialog = false;
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CombatReceiver>() != null && !other.isTrigger)
        {
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
            {
                Debug.Log("In INside Triggered");
                MusicManager.instance.ChangeMusicLayer(MusicLayers.Battle);
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CombatReceiver>() != null && !other.isTrigger)
        {
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
                MusicManager.instance.ChangeMusicLayer(MusicLayers.Exploration);
        }
    }
}
