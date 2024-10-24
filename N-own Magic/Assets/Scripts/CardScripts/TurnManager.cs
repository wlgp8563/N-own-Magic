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
        Debug.Log("�÷��̾� �� ����");
        energyManager.RestoreEnergyAtTurnStart(3);
        //cardManager.DrawCards(player.cardDrawCount);
    }

    public void StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        Debug.Log("�� �� ����");
        enemy.PerformAction();
        EndTurn();
    }

    public void EndTurn()
    {
        if (currentTurn == Turn.Player)
        {
            Debug.Log("�÷��̾� �� ����");
            //cardManager.ResetDeck();
            StartEnemyTurn();
        }
        else
        {
            Debug.Log("�� �� ����");
            StartPlayerTurn();
        }
    }

    public void OnCardUsed()
    {
        EndTurn();
    }
}
