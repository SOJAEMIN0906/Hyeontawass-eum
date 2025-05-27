using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DetailExplain : MonoBehaviour
{
    public TMP_Text explainTxt;

    public void Set(string explain)
    {
        explainTxt.text = explain;
    }
}
