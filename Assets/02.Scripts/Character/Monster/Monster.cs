using UnityEngine;

public partial class Monster : Character
{
    public Status finalStatus { get; protected set; }

    public Transform EffectTrans;

    int expReward;
    int moneyReward;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameManager.Instance.player.transform;

        Init();
    }

    private void OnEnable()
    {
        //expReward
        //moneyReward

        finalStatus = status * (1 + GameManager.Instance.gameTime.Minutes * 0.01f);
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void Update()
    {
        base.Update();

        StateCheck();
        MoveCheck();
    }

    protected override bool Dead()
    {
        if (!base.Dead())
        {
            return false;
        }

        GameManager.Instance.player.GetExp(expReward);
        GameManager.Instance.MoneyGet(moneyReward);

        return true;
    }
}
