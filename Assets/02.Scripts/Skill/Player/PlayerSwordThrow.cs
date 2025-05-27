using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerSwordThrow : Skill
{
    int damage;
    int criticalRate;

    int criticalDamage;

    int mass;

    float cooldown = 3;

    float atkSpeed;

    public float damageRate => 0.5f + level * 0.1f;

    public override void Init()
    {
        cooldown = 3;

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

            Instantiate(
                Resources.Load<PlayerSwordThrowGenerator>("Prefab/DamageApplier/SwordThrowGenerator"),
                transform.position,
                transform.rotation,
                transform
                ).Set(damage, criticalRate, criticalDamage, mass);
        }
    }
    /*
    public void Update()
    {
        if ((coolCnt += Time.deltaTime * atkSpeed) >= cooldown)
        {
            coolCnt = 0;

            Instantiate(
                Resources.Load<PlayerSwordThrowGenerator>("Prefab/DamageApplier/SwordThrowGenerator"),
                transform.position,
                transform.rotation,
                transform
                ).Set(damage, criticalRate, criticalDamage, mass);
        }
    }*/

    protected override void SetDetailStatus()
    {
        Player player = GameManager.Instance.player;

        damage = (int)(player.AttackPower * damageRate);
        criticalRate = player.CriticalRate;

        criticalDamage = player.CriticalDamage;

        mass = 1 + level / 5;

        atkSpeed = player.AttackSpeed;
    }

    public override void LevelUp()
    {
        level++;
    }

    public override string GetExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "Explain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", level.ToString());
            stringBuilder = stringBuilder.Replace("throw_mass", (1 + level / 5).ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }

    public override string GetNextLevelExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "Explain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * (level + 1);
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", (level + 1).ToString());
            stringBuilder = stringBuilder.Replace("throw_mass", (1 + (level + 1) / 5).ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }

    public override string GetDetailExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "DetailExplain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordThrow.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", level.ToString());
            stringBuilder = stringBuilder.Replace("throw_mass", (1 + level / 5).ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }
}
