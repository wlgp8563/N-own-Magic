using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TurnManager : MonoBehaviour
{
    public enum Turn
    {
        Player,
        Enemy
    }

    public Turn currentTurn;
    public CardManager cardManager;
    public LightEnergyManager energyManager;
    public Player player;
    public Enemy enemy;

    void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        currentTurn = Turn.Player;
        Debug.Log("플레이어 턴 시작");
        energyManager.RestoreEnergyAtTurnStart(3);
        //cardManager.DrawCards(player.cardDrawCount);
    }

    public void StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        Debug.Log("적 턴 시작");
        enemy.PerformAction();
        EndTurn();
    }

    public void EndTurn()
    {
        if (currentTurn == Turn.Player)
        {
            Debug.Log("플레이어 턴 종료");
            //cardManager.ResetDeck();
            StartEnemyTurn();
        }
        else
        {
            Debug.Log("적 턴 종료");
            StartPlayerTurn();
        }
    }

    public void OnCardUsed()
    {
        EndTurn();
    }
}
