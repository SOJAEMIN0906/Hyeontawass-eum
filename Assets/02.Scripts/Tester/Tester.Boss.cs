using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tester : MonoBehaviour
{
    public void SummonZaqqum()
    {
        GameManager.Instance.SetGameTime(15 * 60);
    }
}
