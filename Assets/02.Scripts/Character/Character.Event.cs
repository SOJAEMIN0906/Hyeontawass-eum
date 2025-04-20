using System;
using UnityEngine;

public partial class Character : MonoBehaviour
{
    public event Action<int> Damaged;
    public event Action<int> OnHit;

    public event Action OnDead;
}
