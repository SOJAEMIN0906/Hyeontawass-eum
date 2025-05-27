using UnityEngine;

public class PlayerEarthquakeRemainDamageApplier : DamageApplier
{
    float cnt;

    public override void SetValue(int damage, int criticalRate, int criticalDamage)
    {
        base.SetValue(damage, criticalRate, criticalDamage);

        damage >>= 3;
    }

    public override void SetValue(int damage, int criticalRate, int criticalDamage, System.Action<int> hitReact)
    {
        base.SetValue(damage, criticalRate, criticalDamage, hitReact);

        damage >>= 3;
    }

    private void Update()
    {
        if ((cnt += Time.deltaTime) >= 3)
        {
            cnt = 0;
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.ApplyDamage((int)(damage * (Random.Range(0, 1000) < criticalRate ? criticalDamage * 0.01f : 1)), out int finalDamage);
            character.AddStun(0.5f);

            hitReact?.Invoke(finalDamage);
        }
    }
}
