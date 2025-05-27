using System;

public partial class Monster : Character
{
    Action[] objCtrl = new Action[3];

    protected override void StateCheck()
    {
        base.StateCheck();

        characterSR.enabled = (player.position - transform.position).sqrMagnitude < 36;
    }

    void EnableSprite()
    {
        characterSR.enabled = true;
    }

    void DisableSprite()
    {
        characterSR.enabled = false;
    }

    void DestroyObj()
    {
        if (!IsAlive) return;

        IsAlive = false;

        GameManager.Instance.EnemyDestroyed();
        PoolManager.Instance.PushMonster(eMonsterName, this);
    }
}
