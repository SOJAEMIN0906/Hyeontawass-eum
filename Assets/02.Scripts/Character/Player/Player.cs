using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Player : Character
{
    public Transform SkillTrans;
    public Transform EffectTrans;

    Queue<Action> actionQueue;

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

        actionQueue = new();

        Init();
    }

    protected override void Init()
    {
        base.Init();

        DataPack dataPack = StaticData.Instance.dataPack;

        if (dataPack != null)
        {
            status = dataPack.statusData.status;

            Level = dataPack.statusData.level;

            AttackScaleIncrease = dataPack.statusData.AttackScaleIncrease;
            AttackSpeedIncrease = dataPack.statusData.AttackSpeedIncrease;
            PowerIncrease = dataPack.statusData.PowerIncrease;

            CriticalRate = dataPack.statusData.CriticalRate;
            CriticalDamage = dataPack.statusData.CriticalDamage;

            DefenseIncrease = dataPack.statusData.DefenseIncrease;
            recoverIncrease = dataPack.statusData.recoverIncrease;

            expBonus = dataPack.statusData.expBonus;

            foreach (var s in dataPack.skillData.skillPacks)
            {
                Skill skill = Instantiate(
                    Resources.Load<GameObject>("Prefab/Player/Skill/" + s.EPlayerSkill.ToString()),
                    Vector3.zero,
                    Quaternion.identity,
                    SkillTrans
                ).GetComponent<Skill>();

                skillDic.Add(s.EPlayerSkill, skill);
                skill.Init();

                for (; skill.level < s.level;)
                {
                    skill.LevelUp();
                }
            }

            GameManager.Instance.SetLevelTxt();

            GameManager.Instance.SetGameTime(dataPack.gameData.GameTime);

            GameManager.Instance.huntCount = dataPack.gameData.HuntMass;
        }
        else
        {
            CriticalRate = 0;
            CriticalDamage = 150;

            AttackScaleIncrease = 1;

            expBonus = 1;

            PlayerBaseAttack baseAttack = Instantiate(
                Resources.Load<GameObject>("Prefab/Player/Skill/" + EPlayerSkill.BaseAttack),
                SkillTrans.position,
                Quaternion.identity,
                SkillTrans
                ).GetComponent<PlayerBaseAttack>();
            baseAttack.Init();
            baseAttack.LevelUp();

            skillDic.Add(EPlayerSkill.BaseAttack, baseAttack);

            LevelUp();
        }
    }

    protected override void Update()
    {
        base.Update();

        //Debug.Log($"{Vector2.Distance(joystickController.position, joystickController.parent.position)} | {joystickController.localPosition}");

        //MoveCheck();
        StateCheck();
        RegenHealth();

        //rb.velocity = Dir;

        //for (; actionQueue.Count > 0;)
        //{
        //    actionQueue.Dequeue()();
        //}
    }

    void SaveStatusData()
    {
        StatusData statusData = new();

        statusData.status = status;

        statusData.level = Level;

        statusData.AttackScaleIncrease = AttackScaleIncrease;
        statusData.AttackSpeedIncrease = AttackSpeedIncrease;
        statusData.PowerIncrease = PowerIncrease;

        statusData.CriticalRate = CriticalRate;
        statusData.CriticalDamage = CriticalDamage;

        statusData.DefenseIncrease = DefenseIncrease;
        statusData.recoverIncrease = recoverIncrease;

        statusData.expBonus = expBonus;

        FileLoader.SaveFile(statusData);

        GameData gameData = new();
        gameData.GameTime = GameManager.Instance.gameTime.GetTotalSeconds();
        gameData.HuntMass = GameManager.Instance.huntCount;

        FileLoader.SaveFile(gameData);
    }

    void SaveSkillData()
    {
        var skills = GetSkillArray();

        SkillData skillData = new();

        List<SkillPack> skillPacks = new();

        foreach (Skill skill in skills)
        {
            skillPacks.Add(new(skill.ePlayerSkill, skill.level));
        }

        skillData.skillPacks = skillPacks.ToArray();

        FileLoader.SaveFile(skillData);
    }

    protected override bool Dead()
    {
        if (!base.Dead())
        {
            return false;
        }

        GameManager.Instance.GameOver();

        return true;
    }

    public override void AttackOnHit(int damage)
    {
        base.AttackOnHit(damage);

        GameManager.Instance.damageDealt += damage;
    }
}
