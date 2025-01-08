using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected bool alive = true;
    protected int factionID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (alive) RunAI();
    }

    public bool GetAlive()
    {
        return alive;
    }

    protected virtual void RunAI()
    {

    }

    public virtual void SetFactionID(int newID)
    {
        factionID = newID;
        GetComponent<CombatReceiver>().SetFactionID(factionID);
    }

    public virtual void TriggerDeath()
    {
        if (!alive) return;

        alive = false;

        if(GetComponent<EnemyAnimator>() != null)
        {
            GetComponent<EnemyAnimator>().TriggerDeath();
        }

        MusicTrigger.enemyCounter -= 2;

        //Collider[] attachedColliders = GetComponents<Collider>();
        //foreach(Collider c in attachedColliders)
        //{
        //    c.enabled = false;
        //}

        agent.enabled = false;
    }
}
