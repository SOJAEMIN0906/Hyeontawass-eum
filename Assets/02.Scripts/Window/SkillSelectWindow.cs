using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelectWindow : MonoBehaviour
{
    public SkillExplainInfo[] skillExplainInfos = new SkillExplainInfo[4];

    public GameObject skillSelectPanel;

    private void Awake()
    {
        skillExplainInfos[0].skillSelected += CloseWindow;
        skillExplainInfos[1].skillSelected += CloseWindow;
        skillExplainInfos[2].skillSelected += CloseWindow;
        skillExplainInfos[3].skillSelected += CloseWindow;
    }

    public void OpenSkillSelectPanel()
    {
        skillSelectPanel.SetActive(true);

        HashSet<int> skillNums = new();

        for (; skillNums.Count < 4;)
        {
            int skillNum = Random.Range(0, (int)EPlayerSkill.Max);
            if (!skillNums.Contains(skillNum))
            {
                skillNums.Add(skillNum);
            }
        }

        int[] skillArray = skillNums.ToArray();

        skillExplainInfos[0].SetSkill((EPlayerSkill)skillArray[0]);
        skillExplainInfos[1].SetSkill((EPlayerSkill)skillArray[1]);
        skillExplainInfos[2].SetSkill((EPlayerSkill)skillArray[2]);
        skillExplainInfos[3].SetSkill((EPlayerSkill)skillArray[3]);
    }

    void CloseWindow()
    {
        skillSelectPanel.SetActive(false);
    }
}
