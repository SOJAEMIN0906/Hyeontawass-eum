using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillExplainInfo : MonoBehaviour
{
    EPlayerSkill ePlayerSkill;

    public Image SkillImage;

    public TMP_Text SkillExplain;

    Skill skill;

    public event Action skillSelected;

    bool alreadyGet;
    bool isTarget;

    public void SetSkill(EPlayerSkill ePlayerSkill)
    {
        this.ePlayerSkill = ePlayerSkill;

        SkillImage.sprite = Resources.Load<Sprite>("Image/Player/Skill/" + ePlayerSkill.ToString());

        skill = GameManager.Instance.player.GetSkill(ePlayerSkill);

        alreadyGet = skill != null;

        if (!alreadyGet)
        {
            skill = Instantiate(
                Resources.Load<GameObject>("Prefab/Player/Skill/" + ePlayerSkill.ToString()),
                Vector3.zero,
                Quaternion.identity
                ).GetComponent<Skill>();
        }

        SkillExplain.text = skill.GetNextLevelExplain();
    }

    public void SkillSelected()
    {
        isTarget = true;

        skillSelected();

        GameManager.Instance.Resume();

        GameManager.Instance.player.SkillLevelUp(ePlayerSkill, skill);
        GameManager.Instance.player.GetExp(0);
    }

    private void OnEnable()
    {
        isTarget = false;
    }

    public void OnDisable()
    {
        if (!isTarget)
        {
            if (!alreadyGet)
            {
                Destroy(skill.gameObject);
            }
            skill = null;
        }
    }
}
