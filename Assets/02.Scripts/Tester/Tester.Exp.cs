using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tester : MonoBehaviour
{
    public void GetExp()
    {
        GameManager.Instance.player.GetExp(100);
    }
}
