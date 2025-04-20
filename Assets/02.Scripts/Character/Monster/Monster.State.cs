public partial class Monster : Character
{
    protected override void StateCheck()
    {
        base.StateCheck();

        characterSR.enabled = (player.position - transform.position).sqrMagnitude < 36;
    }
}
