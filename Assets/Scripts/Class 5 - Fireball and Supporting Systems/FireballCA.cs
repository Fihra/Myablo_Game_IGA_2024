using UnityEngine;

public class FireballCA : CombatActor
{
    [SerializeField] float speed = 25f;
    Vector3 shootDirection = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(shootDirection * speed * Time.fixedDeltaTime);
    }

    public void SetShootDirection(Vector3 newDirection)
    {
        shootDirection = newDirection;
    }

    protected override void HitReceiver(CombatReceiver target)
    {
        base.HitReceiver(target);
        EffectsManager.instance.PlaySmallBoom(transform.position, 1);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        CombatReceiver check = other.GetComponent<CombatReceiver>();

        if (check != null && !other.isTrigger)
        {
            if (check.GetFactionID() != factionID)
            {
                HitReceiver(check);
                Destroy(gameObject);
            }
        }
    }
}
