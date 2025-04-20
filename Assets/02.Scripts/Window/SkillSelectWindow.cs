using UnityEngine;

public class SkillSelectWindow : MonoBehaviour
{
    public SkillExplainInfo[] skillExplainInfos = new SkillExplainInfo[4];

    public GameObject skillSelectPanel;

    public void OpenSkillSelectPanel()
    {
        skillSelectPanel.SetActive(true);

        skillExplainInfos[0].SetSkill((EPlayerSkill)Random.Range(0, (int)EPlayerSkill.Max));
        skillExplainInfos[1].SetSkill((EPlayerSkill)Random.Range(0, (int)EPlayerSkill.Max));
        skillExplainInfos[2].SetSkill((EPlayerSkill)Random.Range(0, (int)EPlayerSkill.Max));
        skillExplainInfos[3].SetSkill((EPlayerSkill)Random.Range(0, (int)EPlayerSkill.Max));
    }
}
