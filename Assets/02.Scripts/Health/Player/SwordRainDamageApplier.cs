using System;
using UnityEngine;

public class SwordRainDamageApplier : DamageApplier
{
    public override void SetValue(int damage, int criticalRate, int criticalDamage)
    {
        this.damage = damage;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage, Action<int> hitReact)
    {
        this.damage = damage;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;

        this.hitReact = hitReact;
    }

    public void SummonSwords()
    {
        int maxLv = Mathf.Min((int)(damage * criticalDamage * 0.01f) >> 3, 18);
        int minLv = Mathf.Max(damage >> 3, 0);

        float scaleInc = GameManager.Instance.player.AttackScaleIncrease;

        int mass = Mathf.Min(Mathf.Max((int)((maxLv + minLv) * scaleInc) >> 1, 3), 10);

        //Debug.Log($"{maxLv} | {minLv} | {mass}");

        for (int i = 0; i < mass; i++)
        {
            int randomLv = Mathf.Min(UnityEngine.Random.Range(minLv, maxLv + 1), 18);

            DamageApplier damageApplier = PoolManager.Instance.PoolDamageApplier(EDamageApplier.SwordRainSword);
            damageApplier.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/Swords_Origin/evo_bundle"+(randomLv + 2));
            damageApplier.transform.SetParent(transform);
            damageApplier.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.45f, 0.45f), UnityEngine.Random.Range(-0.45f, 0.45f), 0);
            damageApplier.transform.eulerAngles = new(0, 0, 180);
            damageApplier.gameObject.SetActive(true);
            damageApplier.SetValue(damage / (19 - randomLv), criticalRate, criticalDamage, hitReact);

            damageApplier.transform.SetParent(null);
            damageApplier.transform.localScale = Vector3.one * scaleInc;
        }

        Destroyed();
    }
}
