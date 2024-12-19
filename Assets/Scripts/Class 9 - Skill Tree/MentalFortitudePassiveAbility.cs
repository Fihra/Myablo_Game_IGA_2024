using UnityEngine;

public class MentalFortitudePassiveAbility : ClassSkill
{
    public override void LevelUp()
    {
        if(PlayerCharacterSheet.instance.skillPointSpendSuccessful())
        {
            skillLevel++;

            float manaRegenMod = 1 + .5f * skillLevel;
            PlayerController.instance.Combat().SetManaRegenMod(manaRegenMod);
        }
    }
}
