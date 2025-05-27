using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerSwordSpin : Skill
{
    [SerializeField] Rigidbody2D rb;

    int damage;
    int criticalRate;

    int criticalDamage;

    float atkSpeed;

    Player player;

    public float damageRate => 0.5f + level * 0.2f;

    private void Awake()
    {
        player = GameManager.Instance.player;

        level = 0;
    }

    public override void Init()
    {
        player.StatusChanged += SetDetailStatus;

        SetDetailStatus();

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            rb.MoveRotation(rb.rotation + 90 * atkSpeed * Time.deltaTime);
        }
    }
    /*
    public void Update()
    {
        rb.MoveRotation(rb.rotation + 90 * atkSpeed * Time.deltaTime);
    }*/

    private void FixedUpdate()
    {
        rb.MovePosition(player.transform.position);
    }

    protected override void SetDetailStatus()
    {
        damage = (int)(player.AttackPower * damageRate);
        criticalRate = player.CriticalRate;

        criticalDamage = player.CriticalDamage;

        atkSpeed = player.AttackSpeed;

        transform.localScale = Vector3.one * player.AttackScaleIncrease;
    }

    public override void LevelUp()
    {
        level++;

        if (1 + level / 5 > transform.childCount && transform.childCount < 8)
        {
            Instantiate(Resources.Load<GameObject>("Prefab/DamageApplier/Sword")).transform.SetParent(transform);

            float angle = 360f / Mathf.Min(1 + level / 5, 8);
            float radAngle = Mathf.Deg2Rad * angle;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform sword = transform.GetChild(i);

                sword.transform.SetLocalPositionAndRotation(
                    new(Mathf.Cos(radAngle * i) * 0.5f, Mathf.Sin(radAngle * i) * 0.5f, 0),
                    Quaternion.Euler(0, 0, angle * i - 90)
                    );
                sword.transform.localScale = Vector3.one * player.AttackScaleIncrease;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mon = collision.GetComponent<Monster>();

        if (mon != null)
        {
            mon.ApplyDamage((int)(damage * (Random.Range(0, 1000) < criticalRate ? criticalDamage * 0.01f : 1)), out int finalDamage);

            player.AttackOnHit(finalDamage);
        }
    }

    public override string GetExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "Explain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", level.ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("sword_mass", Mathf.Min(1 + level / 5, 8).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }

    public override string GetNextLevelExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "Explain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * (level + 1);
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", (level + 1).ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("sword_mass", Mathf.Min(1 + (level + 1) / 5, 8).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }

    public override string GetDetailExplain()
    {
        string explainTxt = CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "DetailExplain");

        StringBuilder stringBuilder = new();
        stringBuilder.Append(explainTxt);

        if (float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "Percent"), out float rslt1) &&
            float.TryParse(CSVLoader.Instance.GetSkillInfo(EPlayerSkill.SwordSpin.ToString(), "PercentPerLevel"), out float rslt2))
        {
            float percent = rslt1 + rslt2 * level;
            float damage = GameManager.Instance.player.AttackPower * percent;

            stringBuilder = stringBuilder.Replace("level", level.ToString());
            stringBuilder = stringBuilder.Replace("damage", ((int)damage).ToString());
            stringBuilder = stringBuilder.Replace("percentage", ((int)(percent * 100)).ToString());
            stringBuilder = stringBuilder.Replace("sword_mass", Mathf.Min(1 + level / 5, 8).ToString());
            stringBuilder = stringBuilder.Replace("\\n", "\n");
        }

        return stringBuilder.ToString();
    }
}
