using UnityEngine;

public class PlayerBaseAttack : Skill
{
    int damage;
    int criticalRate;

    float criticalDamage;

    float cooldown;
    float coolCnt;

    float atkSpeed;

    public float damageRate => 0.9f + level * 0.1f;

    public override void Init()
    {
        level = 1;

        cooldown = 2;

        GameManager.Instance.player.StatusChanged += SetDetailStatus;

        SetDetailStatus();
    }

    public void Update()
    {
        if ((coolCnt += Time.deltaTime * atkSpeed) >= cooldown)
        {
            coolCnt = 0;

            Player player = GameManager.Instance.player;

            DamageApplier damageApplier = PoolManager.Instance.PoolDamageApplier(EDamageApplier.PlayerBaseAttack);

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

    protected override void SetDetailStatus()
    {
        Player player = GameManager.Instance.player;

        damage = player.AttackPower;
        criticalRate = player.CriticalRate;

        criticalDamage = player.CriticalDamage;

        coolCnt = 0;

        atkSpeed = player.AttackSpeed;
    }

    public override void LevelUp()
    {
        level++;

        coolCnt = 0;
    }

    public override string GetExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Explain");

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            explainTxt.Replace("level", level.ToString());
            explainTxt.Replace("damage", ((int)damage).ToString());
            explainTxt.Replace("percentage", ((int)(percent * 100)).ToString());
        }

        return explainTxt;
    }

    public override string GetNextLevelExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Explain");

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * (level + 1);
            float damage = GameManager.Instance.player.AttackPower * percent;

            explainTxt.Replace("level", (level + 1).ToString());
            explainTxt.Replace("damage", ((int)damage).ToString());
            explainTxt.Replace("percentage", ((int)(percent * 100)).ToString());
        }

        return explainTxt;
    }
}
