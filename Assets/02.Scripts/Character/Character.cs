using UnityEngine;

public partial class Character : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField] protected Status status;

    public SpriteRenderer characterSR;

    [SerializeField] protected int CurrentHealth;

    protected float StunRemain;
    protected float RootRemain;

    protected bool IsAlive;

    public bool IsStuned { get { return StunRemain > 0; } }
    public bool IsRooted { get { return RootRemain > 0; } }

    public virtual bool CanMove { get { return !IsStuned && !IsRooted; } }

    public virtual void Awake()
    {

    }

    protected virtual void Update()
    {
        StunRemain = Mathf.Max(StunRemain - Time.deltaTime, 0);
        RootRemain = Mathf.Max(RootRemain - Time.deltaTime, 0);
    }

    protected virtual void Init()
    {
        CurrentHealth = status.Health;

        StunRemain = 0;
        RootRemain = 0;

        IsAlive = true;
    }

    protected virtual bool Dead()
    {
        if (!IsAlive)
        {
            return false;
        }

        IsAlive = false;

        OnDead?.Invoke();

        return true;
    }

    public virtual void AttackOnHit(int damage)
    {
        OnHit?.Invoke(damage);
    }
}
