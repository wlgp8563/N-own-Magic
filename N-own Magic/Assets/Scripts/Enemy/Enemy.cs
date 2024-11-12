using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int enemyHp;
    public int attack;
    public int pierce;
    public int healSelf;
    public int shieldSelf;
    public int turnCount;

    private int currentTurnCount;
    private int shieldAmount;
    //public TurnManager turnManager;
    public void StartTurn()
    {
        currentTurnCount = turnCount;
        Debug.Log($"{enemyName}�� ���� ���۵˴ϴ�. ���� ��: {currentTurnCount}");
    }

    // ������ �ൿ ����
    public void TakeTurn()
    {
        while (currentTurnCount > 0 && enemyHp > 0)
        {
            PerformRandomAction();
            currentTurnCount--;
        }

        Debug.Log($"{enemyName}�� ���� ����˴ϴ�.");
    }

    // ������ �ൿ ����
    private void PerformRandomAction()
    {
        float randomValue = Random.Range(0f, 1f);
        Player player = FindObjectOfType<Player>();

        if (randomValue <= 0.4f)
        {
            // ����
            if (player != null)
            {
                player.TakeDamage(attack);

                Debug.Log($"{enemyName}��(��) {attack}�� ���ظ� �������ϴ�.");
            }
        }
        else if (randomValue <= 0.7f)
        {
            // ���
            shieldAmount += shieldSelf;
            Debug.Log($"{enemyName}��(��) {shieldSelf}�� ���� ������ϴ�. ���� ��: {shieldAmount}");
        }
        else
        {
            // ġ��
            enemyHp += healSelf;
            Debug.Log($"{enemyName}��(��) {healSelf}��ŭ ü���� ȸ���߽��ϴ�. ���� ü��: {enemyHp}");
        }
    }

    // ������ ���� ó��
    public void TakeDamage(int damageAmount)
    {
        int damageAfterShield = Mathf.Max(damageAmount - shieldAmount, 0);
        shieldAmount = Mathf.Max(shieldAmount - damageAmount, 0);
        enemyHp -= damageAfterShield;

        Debug.Log($"{enemyName}��(��) {damageAfterShield}�� ���ظ� �Ծ����ϴ�. ���� ü��: {enemyHp}");

        if (enemyHp <= 0)
        {
            Debug.Log($"{enemyName}��(��) ���������ϴ�!");
            // ���� �й� ����
        }
    }



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
