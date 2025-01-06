using UnityEngine;

public class PlayerCombat : CombatReceiver
{
    protected float currentMana = 35f;
    protected float maxMana = 35f;

    // RegenVariables
    protected float healthRegenBase = 0.5f;
    protected float healthRegenMod = 1f;
    protected float manaRegenBase = 0.5f;
    protected float manaRegenMod = 1f;
    protected float regenUpdateTickTimer = 0;
    protected float regenUpdateTickTime = 2f;

    protected void Update()
    {
        if(alive) RunRegen();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        factionID = GetComponent<PlayerController>().GetFactionID();
        EventsManager.instance.onPlayerLeveledUp.AddListener(LevelUp);
        EventsManager.instance.onStatPointSpent.AddListener(StatsChangedAdjustment);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(LevelUp);
        EventsManager.instance.onStatPointSpent.RemoveListener(StatsChangedAdjustment);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
        FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.playerReceiveDamage, transform.position);
    }

    public override void Die()
    {
        base.Die();
        GetComponent<PlayerController>().TriggerDeath();
        EventsManager.instance.onPlayerDied.Invoke();
    }

    #region Mana Management
    public float GetMana()
    {
        return currentMana;
    }

    public void SpendMana(float amount)
    {
        currentMana -= amount;
        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
    }
    #endregion

    #region Level Up Events
    void LevelUp()
    {
        currentHP = maxHP;
        currentMana = maxMana;

        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }
    void StatsChangedAdjustment()
    {
        UpdateBaseRegen();

        float oldMaxHP = maxHP;
        float oldMaxMana = maxMana;

        maxHP = PlayerCharacterSheet.instance.getMaxHP();
        maxMana = PlayerCharacterSheet.instance.getMaxMana();

        currentHP += maxHP - oldMaxHP;
        currentMana += maxMana - oldMaxMana;
        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }
    #endregion

    #region Regen
    protected void RunRegen()
    {
        currentHP += (healthRegenBase * healthRegenMod * Time.deltaTime);
        if (currentHP > maxHP) currentHP = maxHP;

        currentMana += (manaRegenBase * manaRegenMod * Time.deltaTime);
        if (currentMana > maxMana) currentMana = maxMana;

        regenUpdateTickTimer += Time.deltaTime;
        if(regenUpdateTickTimer >= regenUpdateTickTime)
        {
            regenUpdateTickTimer -= regenUpdateTickTime;
            EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
            EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        }
    }

    public void SetHPRegenMod(float newMod)
    {
        healthRegenMod = newMod;
    }

    public void SetManaRegenMod(float newMod)
    {
        manaRegenMod = newMod;
    }

    protected void UpdateBaseRegen()
    {
        healthRegenBase = 0.5f + (0.01f * PlayerCharacterSheet.instance.GetVitality());
        manaRegenBase = 0.5f + (0.01f * PlayerCharacterSheet.instance.GetEnergy());
    }

    #endregion
}
