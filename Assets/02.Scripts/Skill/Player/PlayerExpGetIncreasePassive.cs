public class PlayerExpGetIncreasePassive : Skill, IPassive
{
    public readonly float addBonus = 0.1f;

    public override void LevelUp()
    {
        base.LevelUp();

        Passive();
    }

    public void Passive()
    {
        GameManager.Instance.player.AddExpBonus(addBonus);
    }
}
