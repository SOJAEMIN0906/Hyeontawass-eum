public class PlayerAttackSpeedIncreasePassive : Skill, IPassive
{
    public float addBonus { get; private set; }

    void Awake()
    {
        addBonus = 0.1f;

        Passive();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.AttackSpeedBonus(addBonus);
    }
}
