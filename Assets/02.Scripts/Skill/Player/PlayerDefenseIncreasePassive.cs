public class PlayerDefenseIncreasePassive : Skill, IPassive
{
    public readonly float addDefense = 0.1f;

    public override void LevelUp()
    {
        base.LevelUp();
    }

    public void Passive()
    {
        GameManager.Instance.player.DefenseBonus(addDefense);
    }
}
