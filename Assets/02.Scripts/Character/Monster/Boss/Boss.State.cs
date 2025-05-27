using UnityEngine;

public partial class Boss : Character
{
    protected int DamageDecrease; //percentage (10 => 10% (0.1f))

    protected override void StateCheck()
    {

    }

    public override void AddStun(float cnt)
    {

    }

    public override void AddRoot(float cnt)
    {

    }

    public override bool ApplyDamage(int damage)
    {
        bool isDead = base.ApplyDamage((int)((1 - DamageDecrease * 0.01f) * damage));

        GameManager.Instance.BossHealthImg.fillAmount = (float)CurrentHealth / finalStatus.Health;

        return isDead;
    }

    public override bool ApplyDamage(int damage, out int finalDamage)
    {
        bool isDead = base.ApplyDamage((int)((1 - DamageDecrease * 0.01f) * damage), out finalDamage);

        GameManager.Instance.BossHealthImg.fillAmount = (float)CurrentHealth / finalStatus.Health;

        return isDead;
    }
}
