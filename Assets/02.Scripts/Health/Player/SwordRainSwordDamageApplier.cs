using System;
using UnityEngine;

public class SwordRainSwordDamageApplier : DamageApplier
{
    bool isFlying;

    float lifeTime;

    private void Update()
    {
        if ((lifeTime += Time.deltaTime) >= 5)
        {
            lifeTime = 0;
            isFlying = false;

            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFlying)
        {
            if (collision.gameObject.layer != 7) return;

            lifeTime = 0;

            Vector2 dir = (transform.position - collision.transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * 5;

            float rad = Mathf.Atan2(dir.y, dir.x);
            float angle = rad * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            gameObject.layer = 7;
            
            isFlying = true;

            return;
        }

        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.ApplyDamage((int)(damage * (UnityEngine.Random.Range(0, 1000) < criticalRate ? criticalDamage * 0.01f : 1)), out int finalDamage);

            hitReact?.Invoke(finalDamage);
        }
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage)
    {
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;

        base.SetValue(damage, criticalRate, criticalDamage);
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage, Action<int> hitReact)
    {
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;

        base.SetValue(damage, criticalRate, criticalDamage, hitReact);
    }

    public override void Destroyed()
    {
        if (TryGetComponent<BoxCollider2D>(out var col))
        {
            Destroy(col);
        }

        gameObject.layer = 8;

        base.Destroyed();
    }
}
