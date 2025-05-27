using System;
using UnityEngine;

public class SwordThrowDamageApplier : DamageApplier
{
    float cnt;

    public override void SetValue(int damage, int criticalRate, int criticalDamage)
    {
        base.SetValue(damage, criticalRate, criticalDamage);

        GetComponent<Rigidbody2D>().velocity = transform.up * 10;
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage, Action<int> hitReact)
    {
        base.SetValue(damage, criticalRate, criticalDamage, hitReact);

        GetComponent<Rigidbody2D>().velocity = transform.up * 10;
    }

    public override void Destroyed()
    {
        cnt = 0;

        base.Destroyed();
    }

    // Update is called once per frame
    void Update()
    {
        if ((cnt += Time.deltaTime) >= 5)
        {
            cnt = 0;
            Destroyed();
        }
    }
}
