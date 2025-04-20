public class PlayerPowerIncreasePassive : Skill, IPassive
{
    public float addPower { get; private set; }

    void Awake()
    {
        addPower = 0.1f;

        Passive();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.PowerBonus(addPower);
    }
}
