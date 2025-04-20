using UnityEngine;

public partial class Player : Character
{
    public Transform cameraParent;

    private void LateUpdate()
    {
        cameraParent.position = transform.position;
    }
}
