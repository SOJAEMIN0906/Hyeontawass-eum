using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Zaqqum : Boss
{
    protected override void Init()
    {
        attackPattern = new System.Action[3];

        attackPattern[0] = Pattern1_DropBody;
        attackPattern[1] = Pattern2_DropStone;
        attackPattern[2] = Pattern3_PunchSlide;

        base.Init();
    }
}
