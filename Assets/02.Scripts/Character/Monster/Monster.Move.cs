using UnityEngine;

public partial class Monster : Character
{
    protected Transform player;

    protected virtual void MoveCheck()
    {
        if (!CanMove)
        {
            rb.velocity = Vector2.zero;

            return;
        }

        Vector2 dis = player.position - transform.position;
        rb.velocity = 0.01f * status.Speed * dis.normalized;
        characterSR.flipX = rb.velocity.x > 0;

        objCtrl[Mathf.Min((int)dis.sqrMagnitude / 70, 2)]();
    }
}
