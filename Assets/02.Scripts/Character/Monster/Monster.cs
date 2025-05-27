using UnityEngine;

public partial class Monster : Character
{
    public Status finalStatus { get; protected set; }

    public Transform EffectTrans;

    [SerializeField] EMonsterName eMonsterName;

    int expReward;
    int moneyReward;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameManager.Instance.player.transform;

        GameManager.Instance.DestroyAllEnemy += DestroyObj;

        objCtrl[0] = EnableSprite;
        objCtrl[1] = DisableSprite;
        objCtrl[2] = DestroyObj;

        Init();
    }

    private void OnEnable()
    {
        //expReward
        //moneyReward

        finalStatus = status * (1 + ((int)GameManager.Instance.gameTime.GetTotalSeconds() >> 5));
        CurrentHealth = finalStatus.Health;

        expReward = Mathf.Min(Mathf.Max((finalStatus.Power + finalStatus.Health + finalStatus.HealthRegen + finalStatus.Defense + finalStatus.Speed) >> 8, 4), 400);

        Init();
    }

    protected override void Init()
    {
        base.Init();
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

        GameManager.Instance.player.GetExp(expReward);
        //GameManager.Instance.MoneyGet(moneyReward);

        GameManager.Instance.EnemyDestroyed();
        PoolManager.Instance.PushMonster(eMonsterName, this);

        return true;
    }
}
