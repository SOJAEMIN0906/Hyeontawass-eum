public partial class Player : Character
{
    public int Level { get; private set; }

    int needToLevelUp; //8 + level * 2
    public int nowExp { get; private set; }

    float expBonus = 1;

    public void AddExpBonus(float expBonus)
    {
        this.expBonus += expBonus;
    }

    public void GetExp(int mass)
    {
        nowExp += (int)(mass * expBonus);

        int levelUpCount = 0;

        for (; nowExp >= needToLevelUp;)
        {
            nowExp -= needToLevelUp;

            Level++;
            levelUpCount++;

            needToLevelUp = 8 + Level * 2;
        }

        if (levelUpCount > 0)
        {
            CurrentHealth = status.Health;
            StatusChanged?.Invoke();
        }
    }
}
