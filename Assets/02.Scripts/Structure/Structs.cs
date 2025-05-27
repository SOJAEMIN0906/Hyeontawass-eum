[System.Serializable]
public struct Status
{
    public Status(int power, int health, int healthRegen, int defense, int speed)
    {
        Power = power;
        Health = health;
        HealthRegen = healthRegen;
        Defense = defense;
        Speed = speed;
    }

    public Status(float power, float health, float healthRegen, float defense, float speed)
    {
        Power = (int)power;
        Health = (int)health;
        HealthRegen = (int)healthRegen;
        Defense = (int)defense;
        Speed = (int)speed;
    }

    public static Status operator * (Status s, float v)
    {
        return new(s.Power * v, s.Health * v, s.HealthRegen * v, s.Defense * v, s.Speed * v);
    }

    public int Power;
    public int Health;
    public int HealthRegen;
    public int Defense;
    public int Speed;
}

[System.Serializable]
public struct TimeStruct
{
    public System.Text.StringBuilder ToStringBuilder()
    {
        if (Hour > 0)
        {
            return new($"{Hour}:{(Minute >= 10 ? Minute : "0" + Minute)}:{(Second >= 10 ? Second : "0" + Second)}");
        }
        else
        {
            return new($"{(Minute >= 10 ? Minute : "0" + Minute)}:{(Second >= 10 ? Second : "0" + Second)}");
        }
    }

    public void AddSeconds(float seconds)
    {
        Seconds += seconds;
    }

    public float GetTotalSeconds()
    {
        return Seconds;    }

    public static TimeStruct operator +(TimeStruct timeStruct, float seconds)
    {
        return new(timeStruct.Seconds + seconds);
    }

    public TimeStruct(float second)
    {
        Seconds = second;
    }

    /// <summary>
    /// Hour
    /// </summary>
    public int Hour
    {
        get
        {
            return Minute / 60;
        }
    }
    /// <summary>
    /// Minute
    /// </summary>
    public int Minute
    {
        get
        {
            return (int)Seconds / 60 % 60;
        }
    }
    /// <summary>
    /// Total minute
    /// </summary>
    public int Minutes
    {
        get
        {
            return (int)Seconds / 60;
        }
    }
    /// <summary>
    /// Second
    /// </summary>
    public int Second
    {
        get
        {
            return (int)Seconds % 60;
        }
    }
    /// <summary>
    /// Total second
    /// </summary>
    float Seconds;
}