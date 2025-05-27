using UnityEngine;

public partial class Player : Character
{
    public int CriticalRate { get; private set; }
    public int CriticalDamage { get; private set; }

    public float AttackScaleIncrease { get; private set; }

    float AttackSpeedIncrease = 1;
    float PowerIncrease;
    float DefenseIncrease;
    float recoverIncrease;

    public float AttackSpeed { get { return AttackSpeedIncrease; } }
    public int AttackPower { get { return (int)(((Level >> 3) + status.Power) * (1 + PowerIncrease)); } }
    public int Defense { get { return (int)(((Level >> 4) + status.Defense) * (1 + DefenseIncrease)); } }
    public float Recover { get { return 1 + recoverIncrease; } }

    public int MaxHealth { get { return Level * 5 + status.Health; } }
    public int HealthRegen { get { return status.HealthRegen; } }
    public int MovementSpeed { get { return status.Speed; } }

    public void AttackSpeedBonus(float addRate)
    {
        AttackSpeedIncrease += addRate;

        SaveStatusData();
    }

    public void AttackScaleBonus(float addRate)
    {
        AttackScaleIncrease += addRate;

        SaveStatusData();
    }

    public void PowerBonus(float addRate)
    {
        PowerIncrease = Mathf.Max(PowerIncrease + addRate, 0);

        SaveStatusData();
    }

    public void DefenseBonus(float addRate)
    {
        DefenseIncrease = Mathf.Max(DefenseIncrease + addRate, 0);

        SaveStatusData();
    }

    public void HealthBonus(float addRate)
    {
        int addMass = (int)(status.Health * addRate);
        status.Health += addMass;
        CurrentHealth += addMass;

        HealthCheck();

        SaveStatusData();
    }

    public void HealthRegenBonus(int addMass)
    {
        status.HealthRegen += addMass;

        SaveStatusData();
    }

    public void RecoverBonus(int addMass)
    {
        recoverIncrease += addMass;

        SaveStatusData();
    }

    public void MovementSpeedBonus(int addMass)
    {
        status.Speed += addMass;

        SaveStatusData();
    }

    public void CriticalRateBonus(int addRate)
    {
        int addCriticalDmg = Mathf.Max((int)((CriticalRate + addRate - 1000) * 0.2f), 0);

        CriticalRate = Mathf.Min(CriticalRate + addRate, 100);

        CriticalDamage += addCriticalDmg;

        SaveStatusData();
    }

    public void CriticalDamageBonus(int addMass)
    {
        CriticalDamage += addMass;

        SaveStatusData();
    }
}
