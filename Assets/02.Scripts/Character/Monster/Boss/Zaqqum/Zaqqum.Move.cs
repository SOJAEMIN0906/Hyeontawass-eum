using UnityEngine;

public partial class Zaqqum : Boss
{
    enum EDamage
    {
        DropBody,
        DropStone,
        PunchSlide,
    }

    public DamageApplier[] Applier;

    void Pattern1_DropBody()
    {
        animator.SetInteger(PatternAnimId, 1);
    }

    public void DropBodyStartFlying()
    {
        animator.SetInteger(PatternAnimId, 0);

        attackAction = DropBodyOnFlying;
    }

    public void DropBodyEndFlying()
    {
        rb.velocity = Vector2.zero;
        attackAction = null;
    }

    public void DropBodyDamage()
    {
        DamageApplier damageApplier = Instantiate(Applier[(int)EDamage.DropBody]);
        damageApplier.transform.position = transform.position;
        damageApplier.SetValue(finalStatus.Power << 1, 100, 200);
    }

    public void EndDropBody()
    {
        onAttacking = false;
    }

    void DropBodyOnFlying()
    {
        Vector2 dis = player.position - transform.position;
        rb.velocity = 0.03f * status.Speed * dis.normalized;
    }

    void Pattern2_DropStone()
    {
        animator.SetInteger(PatternAnimId, 2);
    }

    public void DropStoneStart()
    {
        animator.SetInteger(PatternAnimId, 0);

        DamageDecrease += 50;

        attackAction = DropStones;
    }

    public void DropStoneEnd()
    {
        DamageDecrease -= 50;

        attackAction = null;
    }

    public void EndDropStoneEnd()
    {
        onAttacking = false;
    }

    float dropStoneDelay = 0;

    void DropStones()
    {
        if ((dropStoneDelay += Time.deltaTime) >= 0.05f)
        {
            dropStoneDelay = 0;

            DamageApplier damageApplier = Instantiate(Applier[(int)EDamage.DropStone]);

            damageApplier.transform.position = transform.position + new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0);

            damageApplier.SetValue(finalStatus.Power >> 2, 100, 200);
        }
    }

    void Pattern3_PunchSlide()
    {
        animator.SetInteger(PatternAnimId, 3);
    }

    public void PunchSlideStart()
    {
        animator.SetInteger(PatternAnimId, 0);

        DamageApplier damageApplier = Instantiate(Applier[(int)EDamage.PunchSlide]);

        damageApplier.transform.position = transform.position;

        damageApplier.SetValue(finalStatus.Power, 500, 300);

        damageApplier.GetComponent<ZaqqumPunchSlideDamageApplier>().endCallBack = PunchSlideEnd;
    }

    public void PunchSlideEnd()
    {
        animator.SetTrigger("EndAttack");
    }

    public void EndPunchSlideEnd()
    {
        onAttacking = false;
    }
}
