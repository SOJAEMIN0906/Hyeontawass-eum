using UnityEngine;

public partial class Player : Character
{
    public int CriticalRate { get; private set; }
    public float CriticalDamage { get; private set; }

    public float AttackScaleIncrease { get; private set; }

    float AttackSpeedIncrease = 1;
    float PowerIncrease;
    float DefenseIncrease;

    public float AttackSpeed { get { return AttackSpeedIncrease; } }
    public int AttackPower { get { return (int)(status.Power * (1 + PowerIncrease)); } }
    public int Defense { get { return (int)(status.Defense * (1 + DefenseIncrease)); } }

    public void AttackSpeedBonus(float addRate)
    {
        AttackSpeedIncrease += addRate;
    }

    public void AttackScaleBonus(float addRate)
    {
        AttackScaleIncrease += addRate;
    }

    public void PowerBonus(float addRate)
    {
        PowerIncrease = Mathf.Max(PowerIncrease + addRate, 0);
    }

    public void DefenseBonus(float addRate)
    {
        DefenseIncrease = Mathf.Max(DefenseIncrease + addRate, 0);
    }

    public void HealthBonus(float addRate)
    {
        int addMass = (int)(status.Health * addRate);
        status.Health += addMass;
        CurrentHealth += addMass;
    }

    public void HealthRegenBonus(int addMass)
    {
        status.HealthRegen += addMass;
    }
}
