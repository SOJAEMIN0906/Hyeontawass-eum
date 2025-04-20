using System;
using UnityEngine;

public class DamageApplier : MonoBehaviour
{
    [SerializeField] EDamageApplier EDamageApplier;

    int damage;
    int criticalRate;
    float criticalDamage;

    Action<int> hitReact;

    public virtual void SetValue(int damage, int criticalRate, float criticalDamage)
    {
        this.damage = damage;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;
    }

    public virtual void SetValue(int damage, int criticalRate, float criticalDamage, Action<int> hitReact)
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
            character.ApplyDamage((int)(damage * UnityEngine.Random.Range(0, 1000) < criticalRate ? 1 : criticalDamage), out int finalDamage);

            hitReact?.Invoke(finalDamage);
        }
    }
}
