using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tester : MonoBehaviour
{
    public void GetSwordRain()
    {
        Skill skill = GameManager.Instance.player.GetSkill(EPlayerSkill.SwordRain);

        if (skill == null)
        {
            skill = Instantiate(
                  Resources.Load<GameObject>("Prefab/Player/Skill/" + EPlayerSkill.SwordRain.ToString()),
                  Vector3.zero,
                  Quaternion.identity
                  ).GetComponent<Skill>();
        }

        GameManager.Instance.player.SkillLevelUp(EPlayerSkill.SwordRain, skill);
    }

    public void GetSwordSpin()
    {
        Skill skill = GameManager.Instance.player.GetSkill(EPlayerSkill.SwordSpin);

        if (skill == null)
        {
            skill = Instantiate(
                  Resources.Load<GameObject>("Prefab/Player/Skill/" + EPlayerSkill.SwordSpin.ToString()),
                  Vector3.zero,
                  Quaternion.identity
                  ).GetComponent<Skill>();
        }

        GameManager.Instance.player.SkillLevelUp(EPlayerSkill.SwordSpin, skill);
    }

    public void GetSwordThrow()
    {
        Skill skill = GameManager.Instance.player.GetSkill(EPlayerSkill.SwordThrow);

        if (skill == null)
        {
            skill = Instantiate(
                  Resources.Load<GameObject>("Prefab/Player/Skill/" + EPlayerSkill.SwordThrow.ToString()),
                  Vector3.zero,
                  Quaternion.identity
                  ).GetComponent<Skill>();
        }

        GameManager.Instance.player.SkillLevelUp(EPlayerSkill.SwordThrow, skill);
    }

    public void GetEarthquake()
    {
        Skill skill = GameManager.Instance.player.GetSkill(EPlayerSkill.Earthquake);

        if (skill == null)
        {
            skill = Instantiate(
                  Resources.Load<GameObject>("Prefab/Player/Skill/" + EPlayerSkill.Earthquake.ToString()),
                  Vector3.zero,
                  Quaternion.identity
                  ).GetComponent<Skill>();
        }

        GameManager.Instance.player.SkillLevelUp(EPlayerSkill.Earthquake, skill);
    }
}
