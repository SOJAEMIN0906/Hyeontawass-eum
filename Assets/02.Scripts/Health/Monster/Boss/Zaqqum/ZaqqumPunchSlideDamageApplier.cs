using System;
using UnityEngine;

public class ZaqqumPunchSlideDamageApplier : DamageApplier
{
    public Action endCallBack;
    
    public void SetPosition()
    {
        transform.position = GameManager.Instance.player.transform.position;
    }

    public void EndAttack()
    {
        endCallBack();

        Destroy(gameObject);
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
