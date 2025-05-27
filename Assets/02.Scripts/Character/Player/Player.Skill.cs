using System.Collections.Generic;
using System.Linq;

public partial class Player : Character
{
    Dictionary<EPlayerSkill, Skill> skillDic = new();

    public int GetSkillLevel(EPlayerSkill skill)
    {
        if (skillDic.ContainsKey(skill))
        {
            return skillDic[skill].level;
        }
        else
        {
            return 0;
        }
    }

    public Skill GetSkill(EPlayerSkill skill)
    {
        if (skillDic.ContainsKey(skill))
        {
            return skillDic[skill];
        }
        else
        {
            return null;
        }
    }

    public List<Skill> GetSkillList()
    {
        return skillDic.Values.ToList();
    }

    public Skill[] GetSkillArray()
    {
        return skillDic.Values.ToArray();
    }

    public void SkillLevelUp(EPlayerSkill ePlayerSkill, Skill skill)
    {
        if (skillDic.ContainsKey(ePlayerSkill))
        {
            skillDic[ePlayerSkill].LevelUp();
        }
        else
        {
            skill.transform.SetParent(SkillTrans);
            skill.transform.localPosition = UnityEngine.Vector3.zero;
            skill.Init();
            skillDic.Add(ePlayerSkill, skill);
            skill.LevelUp();
        }

        StatusChanged?.Invoke();

        SaveSkillData();
    }
}
