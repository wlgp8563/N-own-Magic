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
        Debug.Log($"{enemyName}의 턴이 시작됩니다. 남은 턴: {currentTurnCount}");
    }

    // 적군의 행동 수행
    public void TakeTurn()
    {
        while (currentTurnCount > 0 && enemyHp > 0)
        {
            PerformRandomAction();
            currentTurnCount--;
        }

        Debug.Log($"{enemyName}의 턴이 종료됩니다.");
    }

    // 랜덤한 행동 수행
    private void PerformRandomAction()
    {
        float randomValue = Random.Range(0f, 1f);
        Player player = FindObjectOfType<Player>();

        if (randomValue <= 0.4f)
        {
            // 공격
            if (player != null)
            {
                player.TakeDamage(attack);

                Debug.Log($"{enemyName}이(가) {attack}의 피해를 입혔습니다.");
            }
        }
        else if (randomValue <= 0.7f)
        {
            // 방어
            shieldAmount += shieldSelf;
            Debug.Log($"{enemyName}이(가) {shieldSelf}의 방어막을 얻었습니다. 현재 방어막: {shieldAmount}");
        }
        else
        {
            // 치유
            enemyHp += healSelf;
            Debug.Log($"{enemyName}이(가) {healSelf}만큼 체력을 회복했습니다. 현재 체력: {enemyHp}");
        }
    }

    // 적군의 피해 처리
    public void TakeDamage(int damageAmount)
    {
        int damageAfterShield = Mathf.Max(damageAmount - shieldAmount, 0);
        shieldAmount = Mathf.Max(shieldAmount - damageAmount, 0);
        enemyHp -= damageAfterShield;

        Debug.Log($"{enemyName}이(가) {damageAfterShield}의 피해를 입었습니다. 남은 체력: {enemyHp}");

        if (enemyHp <= 0)
        {
            Debug.Log($"{enemyName}이(가) 쓰러졌습니다!");
            // 적군 패배 로직
        }
    }



    /*public void PerformAction()
    {
        // 플레이어에게 데미지 주기
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
            // 승리 로직 추가
        }
    }*/
}
