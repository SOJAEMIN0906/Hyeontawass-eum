using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCriticalDamageIncrease : Skill, IPassive
{
    public readonly int addRate = 10;

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.CriticalDamageBonus(addRate);
    }
}
