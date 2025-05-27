public class PlayerPowerIncreasePassive : Skill, IPassive
{
    public readonly float addPower = 0.1f;

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
