using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSpeedIncrease : Skill, IPassive
{
    public readonly int addBonus = 10;

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.MovementSpeedBonus(addBonus);
    }
}
