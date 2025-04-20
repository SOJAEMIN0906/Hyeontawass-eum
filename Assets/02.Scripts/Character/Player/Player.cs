using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player : Character
{
    public Transform SkillTrans;
    public Transform EffectTrans;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        EventTrigger joystickTrigger = GameObject.Find("TouchInputPanel").GetComponent<EventTrigger>();
        joystickTrigger.AddComponent<EventTrigger>();

        EventTrigger.Entry beginDrag = new EventTrigger.Entry();
        beginDrag.eventID = EventTriggerType.BeginDrag;
        beginDrag.callback.AddListener((data) => { BeginDrag((PointerEventData)data); });
        joystickTrigger.triggers.Add(beginDrag);

        EventTrigger.Entry onDrag = new EventTrigger.Entry();
        onDrag.eventID = EventTriggerType.Drag;
        onDrag.callback.AddListener((data) => {MoveCheck((PointerEventData)data);});
        joystickTrigger.triggers.Add(onDrag);

        EventTrigger.Entry endDrag = new EventTrigger.Entry();
        endDrag.eventID = EventTriggerType.EndDrag;
        endDrag.callback.AddListener((data) => { EndDrag((PointerEventData)data); });
        joystickTrigger.triggers.Add(endDrag);

        Dir = Vector2.right;

        Init();
    }

    protected override void Init()
    {
        base.Init();

        AttackScaleIncrease = 1;

        PlayerBaseAttack baseAttack = Instantiate(
            Resources.Load<GameObject>("Prefab/Player/Skill/PlayerBaseAttack"),
            SkillTrans.position,
            Quaternion.identity,
            SkillTrans
            ).GetComponent<PlayerBaseAttack>();
        baseAttack.Init();

        skillDic.Add(EPlayerSkill.BaseAttack, baseAttack);
    }

    protected override void Update()
    {
        base.Update();

        //Debug.Log($"{Vector2.Distance(joystickController.position, joystickController.parent.position)} | {joystickController.localPosition}");

        //MoveCheck();
        StateCheck();
    }
}
