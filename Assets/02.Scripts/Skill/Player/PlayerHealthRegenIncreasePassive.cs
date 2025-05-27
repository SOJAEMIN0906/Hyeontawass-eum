using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegenIncreasePassive : Skill, IPassive
{
    public readonly int addBonus = 2;

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.HealthRegenBonus(addBonus);
    }
}
