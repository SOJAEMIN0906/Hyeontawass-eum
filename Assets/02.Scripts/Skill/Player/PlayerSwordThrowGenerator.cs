using UnityEngine;

public class PlayerSwordThrowGenerator : MonoBehaviour
{
    int damage;
    int criticalRate;
    int criticalDamage;

    int mass;

    int count;
    int delay;

    bool onStart;

    public void Set(int damage, int criticalRate, int criticalDamage, int mass)
    {
        this.damage = damage;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;

        this.mass = mass;

        onStart = true;
    }

    private void FixedUpdate()
    {
        if (!onStart) return;

        if (delay < 25)
        {
            delay++;

            return;
        }

        delay = 0;

        Player player = GameManager.Instance.player;

        LayerMask targetLayer = 1 << 8;

        var target = Physics2D.OverlapCircle(transform.position, 5, targetLayer);

        DamageApplier damageApplier = PoolManager.Instance.PoolDamageApplier(EDamageApplier.SwordThrowSword);

        Vector2 dir;

        if (target == null)
        {
            dir = player.Dir;
        }
        else
        {
            dir = (target.transform.position - transform.position).normalized;
        }

        float rad = Mathf.Atan2(dir.y, dir.x);

        float angle = rad * Mathf.Rad2Deg;
        damageApplier.transform.SetPositionAndRotation(
            transform.position,
            Quaternion.Euler(0, 0, angle - 90)
            );
        damageApplier.transform.localScale = Vector3.one * player.AttackScaleIncrease;
        damageApplier.gameObject.SetActive(true);

        damageApplier.SetValue(damage, criticalRate, criticalDamage, player.AttackOnHit);

        count++;

        if (count >= mass)
        {
            Destroy(gameObject);
        }
    }
}
