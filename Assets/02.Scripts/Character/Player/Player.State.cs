using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

public partial class Player : Character
{
    [SF] TMP_Text StunRemainTxt;
    [SF] TMP_Text RootRemainTxt;

    public Image hpBar;

    float hpRegenCnt;

    protected override void StateCheck()
    {
        StunEff.SetActive(IsStuned);
        RootEff.SetActive(IsRooted);

        StunRemainTxt.text = StunRemain.ToString("#.##");
        RootRemainTxt.text = RootRemain.ToString("#.##");
    }

    protected override void HealthCheck()
    {
        hpBar.fillAmount = (float)CurrentHealth / status.Health;
    }

    protected void RegenHealth()
    {
        if (HealthRegen <= 0) return;

        if ((hpRegenCnt += Time.deltaTime) > 1)
        {
            hpRegenCnt = 0;

            CurrentHealth = Mathf.Min(CurrentHealth + HealthRegen, status.Health);

            HealthCheck();
        }
    }
}
