using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public int health = 100;
    public int damage = 10;
    public TurnManager turnManager;

    public void PerformAction()
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
    }
}
