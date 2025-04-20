using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player : Character
{
    public Vector2 Dir { get; private set; }

    Vector2 startPos;

    public Transform joystickParent;
    public Transform joystickController;

    void BeginDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.position);

        joystickParent.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
        startPos = Camera.main.ScreenToWorldPoint(eventData.position) - transform.position;
    }

    void MoveCheck(PointerEventData eventData)
    {
        //rb.velocity = CanMove ? Status.Speed * Time.deltaTime * Dir : Vector2.zero;
        //playerSr.flipX = Dir.x < 0;
        Vector2 touchDir = ((Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - startPos - (Vector2)transform.position).normalized;
        //float ratio = Mathf.Min(touchDir.magnitude * 2.5f, 1);
        joystickController.localPosition = 80 * touchDir.normalized;

        if (!CanMove)
        {
            rb.velocity =  Vector2.zero;
            return;
        }

        characterSR.flipX = touchDir.x < 0;
        rb.velocity = status.Speed * 0.01f * touchDir;

        Dir = touchDir;
    }

    void EndDrag(PointerEventData eventData)
    {
        startPos = Vector2.zero;

        joystickParent.localPosition = new(0, -600, 0);
        joystickController.localPosition = Vector2.zero;

        rb.velocity = Vector2.zero;
    }
}
