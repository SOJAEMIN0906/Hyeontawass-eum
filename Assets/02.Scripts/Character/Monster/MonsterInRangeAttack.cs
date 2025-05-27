using UnityEngine;

public class MonsterInRangeAttack : MonoBehaviour
{
    Monster monster;

    bool canAttack { get { return currentCnt >= 1; } }

    float currentCnt = 0;
    float radius;

    int damage;

    private void Awake()
    {
        monster = GetComponentInParent<Monster>();
        radius = GetComponent<CircleCollider2D>().radius;
    }

    private void OnEnable()
    {
        damage = monster.finalStatus.Power;
    }

    private void Update()
    {
        if (!canAttack)
        {
            currentCnt += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = GameManager.Instance.player;

        if (canAttack && monster.CanMove && (transform.position - player.transform.position).sqrMagnitude <= radius)
        {
            player.ApplyDamage(damage);
            currentCnt = 0;
        }
    }
}
