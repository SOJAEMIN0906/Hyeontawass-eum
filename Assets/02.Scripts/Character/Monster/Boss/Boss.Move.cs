using System;
using UnityEngine;

public partial class Boss : Character
{
    protected Action[] attackPattern;

    protected Action attackAction;

    protected Transform player;

    protected bool onAttacking;

    protected float attackCooldown;

    protected readonly int PatternAnimId = Animator.StringToHash("Pattern");

    protected virtual void MoveCheck()
    {
        if (onAttacking)
        {
            attackAction?.Invoke();

            return;
        }
        else
        {
            if ((attackCooldown += Time.deltaTime) <= 2)
            {
                Vector2 dis = player.position - transform.position;
                rb.velocity = 0.01f * status.Speed * dis.normalized;
                characterSR.flipX = rb.velocity.x > 0;

                return;
            }

            onAttacking = true;

            attackCooldown = 0;

            rb.velocity = Vector2.zero;

            attackPattern[UnityEngine.Random.Range(0, attackPattern.Length)]();
        }
    }
}
