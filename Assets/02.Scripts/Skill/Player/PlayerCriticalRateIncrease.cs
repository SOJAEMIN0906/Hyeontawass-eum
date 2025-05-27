using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCriticalRateIncrease : Skill, IPassive
{
    public readonly int addRate = 25;

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.CriticalRateBonus(addRate);
    }
}
