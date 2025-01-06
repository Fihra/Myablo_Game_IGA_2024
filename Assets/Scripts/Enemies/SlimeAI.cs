using UnityEngine;

public class SlimeAI : BasicAI
{

    enum SlimeState
    {
        Wandering, Pursuing, Attacking, Dead
    }

    SlimeState aiState = SlimeState.Wandering;

    [SerializeField] float maxWanderDistance = 6f;
    Vector3 startPosition = Vector3.zero;

    GameObject target;
    [SerializeField] float maxPursuitDistance = 15f;
    [SerializeField] float attackRange = 1.75f;

    [SerializeField] float damage = 1f;
    [SerializeField] float attackCooldown = 1.5f;
    float attackCooldownTimer = 0.0f;
    [SerializeField] GameObject attackPrefab;

    [SerializeField] float experienceValue = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        startPosition = transform.position;
        TriggerWandering();

        EventsManager.instance.onPlayerDied.AddListener(TriggerWandering);
    }

    protected override void RunAI()
    {
        switch(aiState)
        {
            case SlimeState.Wandering:
                RunWandering();
                break;
            case SlimeState.Pursuing:
                RunPursuing();
                break;
            case SlimeState.Attacking:
                RunAttacking();
                break;
            case SlimeState.Dead:
                break;
        }
    }

    #region Wandering
    void TriggerWandering()
    {
        aiState = SlimeState.Wandering;
        GetNewWanderDestination();
    }

    void RunWandering()
    {
        float x = agent.destination.x;
        float y = transform.position.y;
        float z = agent.destination.z;

        Vector3 checkPosition = new Vector3(x, y, z);

        if(Vector3.Distance(transform.position, checkPosition) < 1)
        {
            GetNewWanderDestination();
        }
    }

    void GetNewWanderDestination()
    {
        float randomX = Random.Range(-maxWanderDistance, maxWanderDistance);
        float randomZ = Random.Range(-maxWanderDistance, maxWanderDistance);
        float x = randomX + startPosition.x;
        float y = startPosition.y;
        float z = randomZ + startPosition.z;

        agent.destination = new Vector3(x, y, z);
    }

    #endregion

    #region Pursuing
    void TriggerPursuing(GameObject targetToPursue)
    {
        aiState = SlimeState.Pursuing;
        target = targetToPursue;
    }

    void RunPursuing()
    {
        if(target == null)
        {
            TriggerWandering();
            return;
        }

        agent.destination = target.transform.position;

        if (TargetIsInAttackRange())
            TriggerAttacking();
        else if (TargetIsBeyondMaxPursuitDistance())
            TriggerWandering();
    }

    private bool TargetIsBeyondMaxPursuitDistance()
    {
        return Vector3.Distance(transform.position, target.transform.position) > maxPursuitDistance;
    }

    private bool TargetIsInAttackRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    #endregion

    #region Attacking
    void TriggerAttacking()
    {
        aiState = SlimeState.Attacking;
        agent.destination = transform.position;
    }

    void RunAttacking()
    {
        attackCooldownTimer += Time.deltaTime;

        if(attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer -= attackCooldown;
            SpawnAttackPrefab();
            GetComponent<EnemyAnimator>().TriggerAttack();
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.slimeAttack, transform.position);
        }

        if (!TargetIsInAttackRange())
            TriggerPursuing(target);
    }

    void SpawnAttackPrefab()
    {
        Vector3 attackDirection = (target.transform.position - transform.position);
        Vector3 spawnPosition = (attackDirection.normalized * attackRange) + transform.position;

        GameObject newAttack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(factionID);
    }

    #endregion

    #region Dead
    public override void TriggerDeath()
    {
        //Debug.Log("Or am i dead here");
        base.TriggerDeath();
        EventsManager.instance.onExperienceGranted.Invoke(experienceValue);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CombatReceiver>() != null && !other.isTrigger)
        {
            if (other.GetComponent<CombatReceiver>().GetFactionID() != factionID)
                TriggerPursuing(other.gameObject);
        }
    }
}
