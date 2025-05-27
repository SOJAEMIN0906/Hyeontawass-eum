using UnityEngine;

public partial class Boss : Character
{
    public Status finalStatus { get; protected set; }

    public override bool CanMove => true;

    [SerializeField] protected Animator animator;

    [SerializeField] protected EBossName eBossName;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameManager.Instance.player.transform;

        finalStatus = status * (1 + ((int)GameManager.Instance.gameTime.GetTotalSeconds() >> 4));

        Init();
    }

    protected override void Init()
    {
        CurrentHealth = finalStatus.Health;

        StunRemain = 0;
        RootRemain = 0;

        IsAlive = true;
    }

    protected override void Update()
    {
        base.Update();

        StateCheck();
    }

    protected void FixedUpdate()
    {
        MoveCheck();
    }

    protected override bool Dead()
    {
        if (!base.Dead())
        {
            return false;
        }

        GameManager.Instance.huntCount++;

        //GameManager.Instance.player.GetExp(expReward);
        //GameManager.Instance.MoneyGet(moneyReward);

        GameManager.Instance.BossDestroyed();

        Destroy(gameObject);

        return true;
    }
}
