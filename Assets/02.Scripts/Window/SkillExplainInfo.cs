using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillExplainInfo : MonoBehaviour
{
    EPlayerSkill ePlayerSkill;

    public Image SkillImage;

    public TMP_Text SkillExplain;

    Skill skill;

    public void SetSkill(EPlayerSkill ePlayerSkill)
    {
        this.ePlayerSkill = ePlayerSkill;

        //SkillImage.sprite = Resources.Load<Sprite>("Image/Player/Skill/" + ePlayerSkill.ToString());

        if (GameManager.Instance.player.GetSkillLevel(ePlayerSkill) > 0)
        {
            skill = GameManager.Instance.player.GetSkill(ePlayerSkill);
        }
        else
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
        GameManager.Instance.player.SkillLevelUp(ePlayerSkill, skill);
    }
}
