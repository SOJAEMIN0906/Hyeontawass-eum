using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    Queue<DamageApplier>[] damageApplierPools = new Queue<DamageApplier>[(int)EDamageApplier.Max];

    Queue<Monster>[] monsterPools = new Queue<Monster>[(int)EMonsterName.Max];

    Dictionary<string, Queue<GameObject>> objPools = new();

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

    //int mass;

    public Monster PoolMonster(EMonsterName eMonster)
    {
        if (monsterPools[(int)eMonster].Count > 0)
        {
            return monsterPools[(int)eMonster].Dequeue();
        }
        else
        {
            GameObject obj = Instantiate(
                Resources.Load<GameObject>("Prefab/Enemy/" + eMonster.ToString())
                );
            //obj.name = obj.name + " " + mass.ToString();
            //mass++;
            obj.hideFlags = HideFlags.HideInHierarchy;
            return obj.GetComponent<Monster>();
        }
    }

    public void PushMonster(EMonsterName eMonster, Monster monster)
    {
        monster.gameObject.SetActive(false);
        monster.transform.SetParent(transform);
        monsterPools[(int)eMonster].Enqueue(monster);
    }

    public GameObject PoolObject(string path)
    {
        if (objPools.ContainsKey(path) && objPools[path].Count > 0)
        {
            return objPools[path].Dequeue();
        }
        else
        {
            return Resources.Load<GameObject>(path);
        }
    }

    public void PushObject(string path, GameObject obj)
    {
        if (objPools.ContainsKey(path))
        {
            objPools[path].Enqueue(obj);
        }
        else
        {
            Queue<GameObject> objs = new();
            objs.Enqueue(obj);

            objPools.Add(path, objs);
        }
    }
}
