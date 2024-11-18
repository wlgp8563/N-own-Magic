using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TurnManager : MonoBehaviour
{
    public static TurnManager TurnManagerInstance;

    private bool isTurnActive = false;
    private bool isPlayerTurn = true;

    public int currentTurn;
    private CardManager cardManager;
    //public LightEnergyManager energyManager;
    private Player player;
    private Enemy enemy;
    //private EnemyControl enemyControl;


    private void Awake()
    {
        if (TurnManagerInstance == null)
        {
            TurnManagerInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
        //currentTurn = player.playerturn;
        isTurnActive = true;

        CardGameManager.cardGameManager.DrawHandDeck();
        CardGameManager.cardGameManager.DisplayHandDeckUI();
        Debug.Log("플레이어 턴 시작");
    }

    public void StartEnemyTurn()
    {
        isPlayerTurn = false;
        isTurnActive = true;
        Debug.Log("적 턴 시작");
        /*if(!BossControl.bossControlInstance.isboss)
        {
            EnemyControl.enemyControlInstance.StartPerformActions();
        }
        else
        {
            BossControl.bossControlInstance.StartPerformActions();
        }*/
        EnemyControl.enemyControlInstance.StartPerformActions();
    }

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        isTurnActive = false;
        CardGameManager.cardGameManager.ClearHandDeck();
        StartCoroutine(DelayEnemyTurn());
        //StartEnemyTurn();
    }

    public void EndEnemyTurn()
    {
        isTurnActive = false;
        StartCoroutine(DelayPlayerTurn());
        //StartPlayerTurn();
    }

    private IEnumerator DelayEnemyTurn()
    {
        yield return new WaitForSeconds(1.0f);

        StartEnemyTurn();
    }

    private IEnumerator DelayPlayerTurn()
    {
        yield return new WaitForSeconds(1.0f);

        StartPlayerTurn();
    }
}
