using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillImg : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] TMP_Text lvTxt;

    [SerializeField] DetailExplain detailExplain;

    Skill skill;

    GameObject explainWindow;

    Transform canvasTrans;

    private void Awake()
    {
        canvasTrans = transform.GetComponentInParent<Canvas>().transform;
    }

    public void Set(Skill skill)
    {
        this.skill = skill;

        img.sprite = Resources.Load<Sprite>("Image/Player/Skill/" + skill.name.Replace("(Clone)", "").Trim());
        lvTxt.text = skill.level.ToString();
    }

    public void PointerDown()
    {
        detailExplain.transform.SetParent(canvasTrans);
        detailExplain.transform.localPosition = new Vector3(0, 0, 0);
        detailExplain.gameObject.SetActive(true);
        detailExplain.Set(skill.GetDetailExplain());
    }

    public void PointerUp()
    {
        detailExplain.transform.SetParent(transform);
        detailExplain.gameObject.SetActive(false);
    }
}
