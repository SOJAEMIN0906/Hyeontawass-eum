using System;
using UnityEngine;

public class DamageApplier : MonoBehaviour
{
    [SerializeField] protected EDamageApplier EDamageApplier;

    protected int damage;
    protected int criticalRate;
    protected int criticalDamage;

    protected Action<int> hitReact;

    public virtual void SetValue(int damage, int criticalRate, int criticalDamage)
    {
        this.damage = damage;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;
    }

    public virtual void SetValue(int damage, int criticalRate, int criticalDamage, Action<int> hitReact)
    {
        this.damage = damage;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;

        this.hitReact = hitReact;
    }

    public virtual void Destroyed()
    {
        hitReact = null;

        PoolManager.Instance.PushDamageApplier(EDamageApplier, this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.ApplyDamage((int)(damage * (UnityEngine.Random.Range(0, 1000) < criticalRate ? criticalDamage * 0.01f : 1)), out int finalDamage);

            hitReact?.Invoke(finalDamage);
        }
    }
}
