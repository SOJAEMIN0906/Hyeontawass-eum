using System;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public EPlayerSkill ePlayerSkill { get; private set; }

    public int level { get; protected set; }

    private void Awake()
    {
        level = 0;

        ePlayerSkill = (EPlayerSkill)Enum.Parse(typeof(EPlayerSkill), name.Replace("(Clone)", "").Trim());
    }

    protected virtual void SetDetailStatus()
    {

    }

    public virtual void Init()
    {

    }

    public virtual void LevelUp()
    {
        level++;
    }

    public virtual string GetExplain()
    {
        return CSVLoader.Instance.GetSkillInfo(name.Replace("(Clone)", "").Trim(), "Explain");
    }

    public virtual string GetNextLevelExplain()
    {
        return CSVLoader.Instance.GetSkillInfo(name.Replace("(Clone)", "").Trim(), "Explain");
    }

    public virtual string GetDetailExplain()
    {
        return CSVLoader.Instance.GetSkillInfo(name.Replace("(Clone)", "").Trim(), "DetailExplain");
    }
}
