using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegenIncreasePassive : Skill, IPassive
{
    public int addBonus { get; private set; }

    void Awake()
    {
        addBonus = 1;

        Passive();
    }

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
