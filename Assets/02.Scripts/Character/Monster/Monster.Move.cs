using UnityEngine;

public partial class Monster : Character
{
    Transform player;

    void MoveCheck()
    {
        if (!CanMove)
        {
            rb.velocity = Vector2.zero;

            return;
        }

        rb.velocity = (player.position - transform.position).normalized * status.Speed * 0.01f;
        characterSR.flipX = rb.velocity.x > 0;
    }
}
