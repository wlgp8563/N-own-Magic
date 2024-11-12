using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public Enemy enemyData; // ScriptableObject 연결
    public Slider hpSlider; // HP 슬라이더 참조

    private int currentHp;
    private int currentShield;
    private int remainingTurns;

    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        InitializeEnemy();
        InitializeSlider();
    }

    private void InitializeEnemy()
    {
        if (enemyData != null)
        {
            currentHp = enemyData.enemyHp;
            currentShield = 0;
            remainingTurns = enemyData.turnCount;
        }
    }

    private void InitializeSlider()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = enemyData.enemyHp;
            hpSlider.value = currentHp;
        }
    }

    private void UpdateHp(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, enemyData.enemyHp);
        if (hpSlider != null)
        {
            hpSlider.value = currentHp;
        }
    }

    private void PerformAction()
    {
        float randomValue = Random.value;

        if (randomValue < 0.4f)
        {
            Attack();
        }
        else if (randomValue < 0.6f)
        {
            Heal();
        }
        else
        {
            Shield();
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Attack() 
    {   /* 공격 코드 */
        ani.SetTrigger("Punch_01");
    }
    private void Heal() { UpdateHp(enemyData.healSelf); }
    private void Shield() { /* 쉴드 코드 */ }

    private void Die()
    {
        Debug.Log($"{enemyData.enemyName} has been defeated!");
        // 보상 화면 표시
    }
}
