using System.Collections;
using UnityEngine;

public class ZaqqumDropBodyDamageApplier : DamageApplier
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }

    public override void Destroyed()
    {
        hitReact = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            character.ApplyDamage((int)(damage * (UnityEngine.Random.Range(0, 1000) < criticalRate ? criticalDamage * 0.01f : 1)), out int finalDamage);

            character.AddStun(2);

            hitReact?.Invoke(finalDamage);
        }
    }
}
