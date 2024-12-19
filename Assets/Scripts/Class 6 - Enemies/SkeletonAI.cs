using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : BasicAI
{
    enum SkeletonState
    {
        Wandering, Pursuing, Attacking, Dead
    }

    SkeletonState aiState = SkeletonState.Wandering;

    // Wander State variables
    [SerializeField] float maxWanderDistance = 6f;
    Vector3 startPosition = Vector3.zero;

    // Pursuit State Variables
    GameObject target;
    [SerializeField] float maxPursuitDistance = 15f;
    [SerializeField] float attackRange = 1.75f;

    // Attack State Variables
    [SerializeField] float damage = 3;
    [SerializeField] float attackCooldown = 2.5f;
    float attackCooldownTimer = 0.0f;
    [SerializeField] GameObject attackPrefab;

    // Death variables
    [SerializeField] float experienceValue = 45;

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
            case SkeletonState.Wandering:
                // Run the Wandering Functionality
                RunWandering();
                break;
            case SkeletonState.Pursuing:
                //Run the Pursuing
                RunPursuing();
                break;
            case SkeletonState.Attacking:
                //RRun atacking
                RunAttacking();
                break;
            case SkeletonState.Dead:
                //dead
                break;
        }
    }

    #region Wandering
    void TriggerWandering()
    {
        aiState = SkeletonState.Wandering;
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
        aiState = SkeletonState.Pursuing;
        target = targetToPursue;
    }

    void RunPursuing()
    {
        if(target == null)
        {
            TriggerWandering();
            return;
        }

        // Go to your target's position
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
        aiState = SkeletonState.Attacking;
        agent.destination = transform.position; //stop runnning
    }

    void RunAttacking()
    {
        // Swing every attacCD second
        attackCooldownTimer += Time.deltaTime;

        // If the cooldown is up, throw a punch
        if(attackCooldownTimer >= attackCooldown)
        {
            attackCooldownTimer -= attackCooldown;
            SpawnAttackPrefab();
            GetComponent<EnemyAnimator>().TriggerAttack();
        }

        // If the target is out of attack range, pursue
        if (!TargetIsInAttackRange())
            TriggerPursuing(target);
    }
    void SpawnAttackPrefab()
    {
        //Utility Function
        Vector3 attackDirection = (target.transform.position - transform.position);
        Vector3 spawnPosition = (attackDirection.normalized * attackRange) + transform.position;

        GameObject newAttack = Instantiate(attackPrefab, spawnPosition, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(factionID);
    }
    #endregion

    #region Dead
    public override void TriggerDeath()
    {
        base.TriggerDeath();

        // Add some experience!
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
