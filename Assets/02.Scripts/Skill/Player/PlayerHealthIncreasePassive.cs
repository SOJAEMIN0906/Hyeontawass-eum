public class PlayerHealthIncreasePassive : Skill, IPassive
{
    public readonly float addExp = 0.1f;

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