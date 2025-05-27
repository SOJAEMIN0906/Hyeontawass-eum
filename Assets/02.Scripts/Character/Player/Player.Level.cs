using System;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Character
{
    public Image expBar;

    public int Level { get; private set; }

    int needToLevelUpEasy { get { return 9 + Level; } }
    int needToLevelUpNorm { get { return 5 + Level * 5; } }
    int needToLevelUpHard { get { return (int)(8 * Mathf.Pow(Level, 3f / 4) * Mathf.Log10(8 * Level)); } }

    int needToLevelUp
    {
        get
        {
            switch (StaticData.Instance.gameHardness)
            {
                case EGameHardness.Normal:
                    return needToLevelUpNorm;

                case EGameHardness.Hard:
                    return needToLevelUpHard;

                case EGameHardness.Easy:
                default:
                    return needToLevelUpEasy;
            }
        }
    }

    public int nowExp { get; private set; }

    public float expBonus { get; private set; }

    public void AddExpBonus(float expBonus)
    {
        this.expBonus += expBonus;
    }

    public void GetExp(int mass)
    {
        nowExp += (int)(mass * expBonus);

        if (nowExp >= needToLevelUp)
        {
            LevelUp();
        }

        ExpBarCtrl();
    }

    public void LevelUp()
    {
        nowExp = Mathf.Max(nowExp - needToLevelUp, 0);

        Level++;

        //CurrentHealth = status.Health;
        StatusChanged?.Invoke();

        LevelUped();
    }

    public void ExpBarCtrl()
    {
        expBar.fillAmount = Mathf.Min((float)nowExp / needToLevelUp, 1f);
    }
}
