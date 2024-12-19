using UnityEngine;

public class MultiBallEquippableAbility : FireballEquippability
{
    protected override void SpawnEquippedAttack(Vector3 location)
    {
        base.SpawnEquippedAttack(location);
        base.SpawnEquippedAttack(location + transform.right);
        base.SpawnEquippedAttack(location - transform.right);
    }
}
