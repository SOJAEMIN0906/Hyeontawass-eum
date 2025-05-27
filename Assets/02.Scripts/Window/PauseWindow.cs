using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using SF = UnityEngine.SerializeField;

public class PauseWindow : MonoBehaviour
{
    [SF] Transform skillsTrans;

    [SF] TMP_Text powerTxt, aSpeedTxt, critRateTxt, hpTxt, defTxt, mSpeedTxt, aScaleTxt, critDmgTxt, hpRegenTxt, expIncTxt;

    private void OnEnable()
    {
        Skill[] skills = GameManager.Instance.player.GetSkillArray();

        SkillImg skillImg = Resources.Load<SkillImg>("Prefab/Window/SkillImg");

        foreach (Skill skill in skills)
        {
            SkillImg s = Instantiate(skillImg, skillsTrans);
            s.Set(skill);
        }

        powerTxt.text = GameManager.Instance.player.AttackPower.ToString();
        aSpeedTxt.text = GameManager.Instance.player.AttackSpeed.ToString("#.##");
        critRateTxt.text = $"{GameManager.Instance.player.CriticalRate * 0.1f}%";
        hpTxt.text = GameManager.Instance.player.MaxHealth.ToString();
        defTxt.text = GameManager.Instance.player.Defense.ToString();
        mSpeedTxt.text = GameManager.Instance.player.MovementSpeed.ToString();
        aScaleTxt.text = $"{(int)(GameManager.Instance.player.AttackScaleIncrease * 100)}%";
        critDmgTxt.text = $"{GameManager.Instance.player.CriticalDamage}%";
        hpRegenTxt.text = GameManager.Instance.player.HealthRegen.ToString();
        expIncTxt.text = $"{(int)(GameManager.Instance.player.expBonus * 100) - 100}%";
    }

    private void OnDisable()
    {
        for (int i = skillsTrans.childCount - 1; i >= 0; i--)
        {
            Destroy(skillsTrans.GetChild(i).gameObject);
        }
    }

    public void CloseWindow()
    {
        GameManager.Instance.Resume();
        gameObject.SetActive(false);
    }

    public void BackToTitle()
    {
        GameManager.Instance.Resume();

        FileLoader.DeleteData();

        SceneManager.LoadScene("01.TitlePC");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
