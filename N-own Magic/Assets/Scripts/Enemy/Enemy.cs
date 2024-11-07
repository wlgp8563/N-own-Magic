using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int enemyHp;
    public int attack;
    public int healSelf;
    public int shieldSelf;
    public int turnCount;
    //public TurnManager turnManager;



    /*public void PerformAction()
    {
        // �÷��̾�� ������ �ֱ�
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log($"{enemyName} took {damageAmount} damage. Remaining Health: {health}");
        if (health <= 0)
        {
            Debug.Log($"{enemyName} has been defeated!");
            // �¸� ���� �߰�
        }
    }*/
}
