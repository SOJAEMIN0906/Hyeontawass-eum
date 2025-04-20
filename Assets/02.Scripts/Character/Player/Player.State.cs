using TMPro;
using UnityEngine;

using SF = UnityEngine.SerializeField;

public partial class Player : Character
{
    [SF] TMP_Text StunRemainTxt;
    [SF] TMP_Text RootRemainTxt;

    protected override void StateCheck()
    {
        StunEff.SetActive(IsStuned);
        RootEff.SetActive(IsRooted);

        StunRemainTxt.text = StunRemain.ToString();
        RootRemainTxt.text = RootRemain.ToString();
    }

    protected override void HealthCheck()
    {

    }
}
