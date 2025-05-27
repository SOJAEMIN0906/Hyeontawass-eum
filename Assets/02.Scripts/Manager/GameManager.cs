using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SF = UnityEngine.SerializeField;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SF] SkillSelectWindow SkillSelectWindow;

    public Player player;
    
    [SF] TMP_Text gameTimeTxt;
    [SF] TMP_Text levelTxt;
    [SF] TMP_Text gameOverTxt;

    public TimeStruct gameTime { get; private set; }

    [HideInInspector] public int huntCount;
    [HideInInspector] public int damageDealt;

    int currentMonsterMass;
    readonly int maxMonsterMass = 30;

    int BossCount;

    //public float deltaTime { get; private set; }

    GameObject pauseWindow;
    [SF] GameObject GameOverWindow;
    [SF] GameObject CageObj;
    [SF] GameObject BossHealthObj;

    public Image BossHealthImg;

    public bool IsPaused { get; private set; }

    bool bossTime;

    public event Action DestroyAllEnemy;

    //public Transform enemyTrans;

    private void Awake()
    {
        Instance = this;

        player.LevelUped += PlayerLevelUped;
    }

    private void OnDestroy()
    {
        Instance = null;

        player.LevelUped -= PlayerLevelUped;
    }

    private void Update()
    {
        //deltaTime = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseWindow == null || !pauseWindow.activeSelf)
            {
                OpenPauseWindow();
            }
            else
            {
                Resume();
                pauseWindow.SetActive(false);
            }
        }

        if (bossTime)
        {
            return;
        }

        gameTime += Time.deltaTime;

        if (gameTime.Minutes / 15 >= 1 && BossCount < gameTime.Minutes / 15 && BossCount < (int)EBossName.Max)
        {
            GenerateBoss();
        }

        gameTimeTxt.text = gameTime.ToStringBuilder().ToString();

        if (currentMonsterMass < maxMonsterMass)
        {
            GenerateMonster();
        }
    }

    void GenerateMonster()
    {
        currentMonsterMass++;

        int enemyNum = UnityEngine.Random.Range(0, Mathf.Min(gameTime.Minutes, (int)EMonsterName.Max));

        Monster monster = PoolManager.Instance.PoolMonster((EMonsterName)enemyNum);
        monster.gameObject.SetActive(true);
        monster.transform.position = player.transform.position + new Vector3(UnityEngine.Random.Range(-6f, 6f), UnityEngine.Random.Range(-8f, 8f));

        //monster.transform.SetParent(enemyTrans);
    }

    void GenerateBoss()
    {
        BossHealthObj.SetActive(true);

        BossHealthImg.fillAmount = 1;

        CageObj.transform.position = player.transform.position;
        CageObj.SetActive(true);

        bossTime = true;

        DestroyAllEnemy();

        Instantiate(
            Resources.Load<GameObject>("Prefab/Enemy/Boss/" + (EBossName)BossCount),
            player.transform.position + new Vector3(0, 5, 0),
            Quaternion.identity
            );

        BossCount++;
    }

    public void SetLevelTxt()
    {
        levelTxt.text = $"Lv.{player.Level}";
    }

    public void PlayerLevelUped()
    {
        SetLevelTxt();

        Pause();

        SkillSelectWindow.OpenSkillSelectPanel();
    }

    public void OpenPauseWindow()
    {
        Pause();

        if (pauseWindow == null)
        {
            pauseWindow = Instantiate(
                Resources.Load<GameObject>("Prefab/Window/PauseWindow")
                );
        }
        else
        {
            pauseWindow.SetActive(true);
        }
    }

    public void SetGameTime(float gameTime)
    {
        this.gameTime = new(gameTime);
    }

    public void Pause()
    {
        IsPaused = true;

        Time.timeScale = 0;
    }

    public void Resume()
    {
        IsPaused = false;

        Time.timeScale = 1;
    }

    public void MoneyGet(int money)
    {

    }

    public void EnemyDestroyed()
    {
        currentMonsterMass--;
    }

    public void BossDestroyed()
    {
        BossHealthObj.SetActive(false);

        CageObj.SetActive(false);

        bossTime = false;
    }

    public void GameOver()
    {
        Pause();

        GameOverWindow.SetActive(true);

        StringBuilder sb = new();
        sb.Append(
            $"생존 시간: {gameTime.ToStringBuilder().ToString()}\n<size=30> </size>\n" +
            $"사냥한 몬스터 수: {huntCount}\n<size=30> </size>\n" +
            $"레벨: {player.Level}Lv\n<size=30> </size>\n");// +
            //$"가한 총 피해량: {damageDealt}");

        gameOverTxt.text = sb.ToString();
    }

    public void ReturnToTitle()
    {
        Resume();

        FileLoader.DeleteData();

        SceneManager.LoadScene("01.TitleAndroid");
    }
}
