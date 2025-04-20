public class PlayerDefenseIncreasePassive : Skill, IPassive
{
    public float addDefense { get; private set; }

    void Awake()
    {
        addDefense = 0.1f;

        Passive();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.DefenseBonus(addDefense);
    }
}
