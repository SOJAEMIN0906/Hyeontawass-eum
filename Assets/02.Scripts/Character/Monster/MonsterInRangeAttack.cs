using UnityEngine;

public class MonsterInRangeAttack : MonoBehaviour
{
    bool playerInRange;

    bool canAttack { get { return currentCount >= attackCount; } }

    public int attackCount = 10;
    int currentCount = 0;

    int damage;

    private void OnEnable()
    {
        damage = GetComponentInParent<Monster>().finalStatus.Power;
    }

    private void FixedUpdate()
    {
        if (!canAttack)
        {
            currentCount++;
            return;
        }

        if (playerInRange)
        {
            GameManager.Instance.player.ApplyDamage(damage);
            currentCount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }
}
