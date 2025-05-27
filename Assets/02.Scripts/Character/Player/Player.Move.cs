using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player : Character
{
    public Vector2 Dir/* => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));*/ { get; private set; }

    Vector2 startPos;

    public Transform joystickParent;
    public Transform joystickController;

    bool mouseLock;
    bool onDrag;

    void BeginDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.position);

        joystickParent.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position);
        startPos = Camera.main.ScreenToWorldPoint(eventData.position) - transform.position;
    }

    void MoveCheck()
    {
        if (!mouseLock)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mouseLock = true;
                onDrag = false;

                joystickParent.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            }
            else
            {
                if (!onDrag)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        onDrag = true;

                        joystickParent.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        Vector2 touchDir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
                        //float ratio = Mathf.Min(touchDir.magnitude * 2.5f, 1);

                        SetDir(touchDir);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        onDrag = false;

                        startPos = Vector2.zero;

                        joystickParent.localPosition = new(0, -330, 0);
                        joystickController.localPosition = Vector2.zero;

                        rb.velocity = Vector2.zero;
                    }
                    else
                    {
                        Vector2 touchDir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos - (Vector2)transform.position).normalized;
                        //float ratio = Mathf.Min(touchDir.magnitude * 2.5f, 1);

                        SetDir(touchDir);
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                mouseLock = false;

                startPos = Vector2.zero;

                joystickParent.localPosition = new(0, -330, 0);
                joystickController.localPosition = Vector2.zero;

                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 touchDir = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPos - (Vector2)transform.position).normalized;
                //float ratio = Mathf.Min(touchDir.magnitude * 2.5f, 1);

                SetDir(touchDir);
            }
        }
    }

    void MoveCheck(PointerEventData eventData)
    {
        //rb.velocity = CanMove ? Status.Speed * Time.deltaTime * Dir : Vector2.zero;
        //playerSr.flipX = Dir.x < 0;
        Vector2 touchDir = ((Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - startPos - (Vector2)transform.position).normalized;
        //float ratio = Mathf.Min(touchDir.magnitude * 2.5f, 1);

        SetDir(touchDir);
    }

    void EndDrag(PointerEventData eventData)
    {
        startPos = Vector2.zero;

        joystickParent.localPosition = new(0, -330, 0);
        joystickController.localPosition = Vector2.zero;

        rb.velocity = Vector2.zero;
    }

    void SetDir(Vector2 touchDir)
    {
        joystickController.localPosition = 80 * touchDir.normalized;

        if (!CanMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        characterSR.flipX = touchDir.x < 0;
        rb.velocity = status.Speed * 0.01f * touchDir;

        Dir = touchDir;
    }
}
