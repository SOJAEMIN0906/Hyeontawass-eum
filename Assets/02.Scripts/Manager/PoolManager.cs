using System.Collections.Generic;
using UnityEngine;

public enum EDamageApplier
{
    PlayerBaseAttack,
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    Queue<DamageApplier>[] damageApplierPools = new Queue<DamageApplier>[1];

    Queue<Monster>[] monsterPools = new Queue<Monster>[(int)EMonsterGrade.Max];

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < damageApplierPools.Length; i++)
        {
            damageApplierPools[i] = new();
        }

        for (int i = 0; i < monsterPools.Length; i++)
        {
            monsterPools[i] = new();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public DamageApplier PoolDamageApplier(EDamageApplier eDamageApplier)
    {
        if (damageApplierPools[(int)eDamageApplier].Count > 0)
        {
            return damageApplierPools[(int)eDamageApplier].Dequeue();
        }
        else
        {
            return Instantiate(
                Resources.Load<GameObject>("Prefab/DamageApplier/" + eDamageApplier.ToString())
                ).GetComponent<DamageApplier>();
        }
    }

    public void PushDamageApplier(EDamageApplier eDamageApplier, DamageApplier damageApplier)
    {
        damageApplier.gameObject.SetActive(false);
        damageApplier.transform.SetParent(transform);
        damageApplierPools[(int)eDamageApplier].Enqueue(damageApplier);
    }

    public Monster PoolMonster(EMonsterGrade eMonster)
    {
        if (monsterPools[(int)eMonster].Count > 0)
        {
            return monsterPools[(int)eMonster].Dequeue();
        }
        else
        {
            return Instantiate(
                Resources.Load<GameObject>("Prefab/Enemy/" + eMonster.ToString())
                ).GetComponent<Monster>();
        }
    }

    public void PushMonster(EMonsterGrade eMonster, Monster monster)
    {
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        monsterPools[(int)eMonster].Enqueue(monster);
    }
}
