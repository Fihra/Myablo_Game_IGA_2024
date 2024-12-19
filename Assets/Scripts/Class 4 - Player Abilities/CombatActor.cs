using UnityEngine;

public class CombatActor : MonoBehaviour
{
    protected int factionID = 0;
    protected float damage = 1;

    public virtual void InitializeDamage(float amount)
    {
        damage = amount;
    }

    public void SetFactionID(int newID)
    {
        factionID = newID;
    }

    protected virtual void HitReceiver(CombatReceiver target)
    {
        target.TakeDamage(damage);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        CombatReceiver check = other.GetComponent<CombatReceiver>();

        if(check != null && !other.isTrigger)
        {
            if(check.GetFactionID() != factionID)
            {
                HitReceiver(check);
            }
        }
    }
}
