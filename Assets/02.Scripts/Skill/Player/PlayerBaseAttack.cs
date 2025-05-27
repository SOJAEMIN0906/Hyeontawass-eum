using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerBaseAttack : Skill
{
    //Transform lastTarget;

    int damage;
    int criticalRate;

    int criticalDamage;

    float cooldown = 1;

    float atkSpeed;

    public float damageRate => 0.9f + level * 0.1f;

    public override void Init()
    {
        cooldown = 1;

        GameManager.Instance.player.StatusChanged += SetDetailStatus;

        SetDetailStatus();

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        float cnt = 0;

        while (true)
        {
            while (cnt < cooldown)
            {
                yield return new WaitForEndOfFrame();

                cnt += Time.deltaTime * atkSpeed;
            }

            cnt = 0;

            Player player = GameManager.Instance.player;

            DamageApplier damageApplier = PoolManager.Instance.PoolDamageApplier(EDamageApplier.BaseAttack);

            float rad = Mathf.Atan2(player.Dir.y, player.Dir.x);
            float angle = rad * Mathf.Rad2Deg;
            damageApplier.transform.SetPositionAndRotation(
                transform.position,
                Quaternion.Euler(0, 0, angle)
                );
            damageApplier.transform.localScale = Vector3.one * player.AttackScaleIncrease;
            damageApplier.gameObject.SetActive(true);

            damageApplier.SetValue(damage, criticalRate, criticalDamage, player.AttackOnHit);
        }
    }

    /*
    public void Update()
    {
        if ((coolCnt += Time.deltaTime * atkSpeed) >= cooldown)
        {
            coolCnt = 0;

            Player player = GameManager.Instance.player;

            DamageApplier damageApplier = PoolManager.Instance.PoolDamageApplier(EDamageApplier.BaseAttack);

            float rad = Mathf.Atan2(player.Dir.y, player.Dir.x);
            //if (lastTarget == null)
            //{
            //    rad = Mathf.Atan2(player.Dir.y, player.Dir.x);
            //}
            //else
            //{
            //    Vector2 dirTmp = (lastTarget.position - transform.position).normalized;
            //    rad = Mathf.Atan2(dirTmp.y, dirTmp.x);
            //}
            float angle = rad * Mathf.Rad2Deg;
            damageApplier.transform.SetPositionAndRotation(
                transform.position, 
                Quaternion.Euler(0, 0, angle)
                );
            damageApplier.transform.localScale = Vector3.one * player.AttackScaleIncrease;
            damageApplier.gameObject.SetActive(true);

            damageApplier.SetValue(damage, criticalRate, criticalDamage, player.AttackOnHit);
        }
    }*/

    protected override void SetDetailStatus()
    {
        Player player = GameManager.Instance.player;

        damage = (int)(player.AttackPower * damageRate);
        criticalRate = player.CriticalRate;

        criticalDamage = player.CriticalDamage;

        atkSpeed = player.AttackSpeed;
    }

    public override void LevelUp()
    {
        level++;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (lastTarget == null)
    //    {
    //        lastTarget = collision.transform;
    //    }
    //    else if ((lastTarget.position - transform.position).sqrMagnitude > (collision.transform.position - transform.position).sqrMagnitude)
    //    {
    //        lastTarget = collision.transform;
    //    }
    //}

    public override string GetExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Explain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", level.ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }

    public override string GetNextLevelExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Explain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * (level + 1);
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", (level + 1).ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }

    public override string GetDetailExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "DetailExplain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", level.ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }
}
