using UnityEngine;

public class ZaqqumDropSoneDamageApplier : DamageApplier
{
    public override void Destroyed()
    {
        hitReact = null;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.ApplyDamage((int)(damage * (UnityEngine.Random.Range(0, 1000) < criticalRate ? criticalDamage * 0.01f : 1)), out int finalDamage);

            character.AddStun(0.5f);

            hitReact?.Invoke(finalDamage);
        }
    }
}
