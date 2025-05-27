using System;
using UnityEngine;

public class PlayerEarthquakeSwordDamageApplier : DamageApplier
{
    Player player;

    bool ready;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    private void FixedUpdate()
    {
        if (ready) return;

        float rad = Mathf.Atan2(player.Dir.y, player.Dir.x);
        float angle = rad * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage)
    {
        base.SetValue(damage, criticalRate, criticalDamage);

        ready = false;
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage, Action<int> hitReact)
    {
        base.SetValue(damage, criticalRate, criticalDamage, hitReact);

        ready = false;
    }

    public void Ready()
    {
        ready = true;
    }

    public void SummonEarthquake()
    {
        DamageApplier damageApplier = PoolManager.Instance.PoolDamageApplier(EDamageApplier.EarthquakeRemain);
        damageApplier.transform.SetPositionAndRotation(
            transform.position,
            transform.rotation
            );
        damageApplier.transform.localScale = transform.localScale + Vector3.one * player.AttackScaleIncrease;
        damageApplier.gameObject.SetActive(true);
    }
}
