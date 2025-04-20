public class PlayerHealthIncreasePassive : Skill, IPassive
{
    public float addExp { get; private set; }

    void Awake()
    {
        addExp = 0.1f;

        Passive();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.AddExpBonus(addExp);
    }
}