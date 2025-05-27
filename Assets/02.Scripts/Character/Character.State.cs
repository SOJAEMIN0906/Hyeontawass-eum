using UnityEngine;

using SF = UnityEngine.SerializeField;

public partial class Character : MonoBehaviour
{
    [SF] protected GameObject StunEff;
    [SF] protected GameObject RootEff;

    protected virtual void StateCheck()
    {
        StunEff.SetActive(IsStuned);
        RootEff.SetActive(IsRooted);
    }

    protected virtual void HealthCheck()
    {

    }

    public virtual void AddStun(float cnt)
    {
        StunRemain += cnt;
    }

    public virtual void AddRoot(float cnt)
    {
        RootRemain += cnt;
    }

    public virtual bool ApplyDamage(int damage)
    {
        if (!IsAlive)
        {
            return false;
        }

        damage = Mathf.Max(damage - status.Defense, 0);

        Damaged?.Invoke(damage);

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        HealthCheck();

        if (CurrentHealth <= 0)
        {
            Dead();

            return true;
        }

        return false;
    }

    public virtual bool ApplyDamage(int damage, out int finalDamage)
    {
        if (!IsAlive)
        {
            finalDamage = 0;

            return false;
        }

        damage = Mathf.Max(damage - status.Defense, 0);

        finalDamage = damage;

        Damaged?.Invoke(damage);

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        HealthCheck();

        if (CurrentHealth <= 0)
        {
            Dead();

            return true;
        }

        return false;
    }
}
