using UnityEngine;

public class Skill : MonoBehaviour
{
    public int level { get; protected set; }

    private void Awake()
    {
        level = 0;
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
        return CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Explain");
    }

    public virtual string GetNextLevelExplain()
    {
        return CSVLoader.Instance.GetSkillInfo(EPlayerSkill.BaseAttack.ToString(), "Explain");
    }
}
