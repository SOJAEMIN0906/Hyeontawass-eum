using System;
using System.Text;
using TMPro;
using UnityEngine;

using SF = UnityEngine.SerializeField;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player player;

    StringBuilder gameTimeSb = new();

    [SF] TMP_Text gameTimeTxt;

    public TimeStruct gameTime { get; private set; }

    int currentMonsterMass;
    readonly int maxMonsterMass = 20;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        gameTime.AddSeconds(Time.deltaTime);

        gameTimeTxt.text = gameTime.ToStringBuilder().ToString();
    }

    void GenerateMonster()
    {
        int enemyNum = UnityEngine.Random.Range(0, Mathf.Min(gameTime.Minutes, (int)EMonsterGrade.Max));

        Monster monster = PoolManager.Instance.PoolMonster((EMonsterGrade)enemyNum);
        monster.gameObject.SetActive(true);
        monster.transform.position = player.transform.position + new Vector3(UnityEngine.Random.Range(6f, 8f), UnityEngine.Random.Range(6f, 8f));
    }

    public void MoneyGet(int money)
    {

    }
}
