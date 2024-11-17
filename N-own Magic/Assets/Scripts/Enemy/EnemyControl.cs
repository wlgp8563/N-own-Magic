using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl enemyControlInstance;

    //public GameObject enemyShields;
    public TMP_Text enemyShieldNum;
    public Enemy enemyData; // ScriptableObject 연결
    public Slider hpSlider; // HP 슬라이더 참조
    public TMP_Text hpText;
    public GameObject reward;
    public Slider playerExp;
    public TMP_Text playerExpNum;
    public TMP_Text enemyGiveMoney;
    public TMP_Text playerLevel;

    public int currentEnemyHp;
    public int currentEnemyShield;
    public int remainingTurns;

    public SpriteRenderer[] enemyParts;

    private Animator ani;

    private void Awake()
    {
        if (enemyControlInstance == null)
        {
            enemyControlInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeEnemy();
        InitializeSlider();
        ani = GetComponent<Animator>();
        reward.SetActive(false);
    }

    private void InitializeEnemy()
    {
        if (enemyData != null)
        {
            currentEnemyHp = enemyData.enemyHp;
            remainingTurns = enemyData.turnCount;
            currentEnemyShield = enemyData.enemyShield;
            enemyShieldNum.text = $"Shield : {currentEnemyShield}";
        }
    }

    private void InitializeSlider()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = enemyData.enemyHp;
            hpSlider.value = currentEnemyHp;
            hpText.text = $"{currentEnemyHp} / {enemyData.enemyHp}";
        }
    }

    private void UpdateEnemyHp(int currentHp)
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHp;
            hpText.text = $"{currentHp} / {enemyData.enemyHp}";
        }
    }

    public void StartPerformActions()
    {
        StartCoroutine(PerformActionsWithDelay());
    }

    // 적의 행동을 1초 텀을 두고 실행하는 코루틴
    private IEnumerator PerformActionsWithDelay()
    {
        while (remainingTurns > 0 && currentEnemyHp > 0)
        {
            PerformAction(); // 행동 수행
            remainingTurns--;
            yield return new WaitForSeconds(1.5f); // 1초 대기
        }

        if(remainingTurns <= 0)
        {
            remainingTurns = enemyData.turnCount;
            TurnManager.TurnManagerInstance.EndEnemyTurn();
        }
    }

    public void PerformAction()
    {
        float hpRatio = (float)currentEnemyHp / enemyData.enemyHp;
        float randomValue = Random.value; // 0에서 1 사이의 랜덤 값
        int attackValue = enemyData.attack;
        int pierceValue = enemyData.pierce;
        int shieldValue = enemyData.shieldSelf;
        int healValue = enemyData.healSelf;

        // 현재 HP가 2/3 이상일 때
        if (hpRatio > 2f / 3f)
        {
            // Attack 50%, Pierce 30%, Shield 20%
            if (randomValue < 0.5f)
            {
                Attack(attackValue);
            }
            else if (randomValue < 0.8f)
            {
                Pierce(pierceValue);
            }
            else
            {
                Shield(shieldValue);
            }
        }
        // 현재 HP가 1/3 이상 2/3 이하일 때
        else if (hpRatio > 1f / 3f && hpRatio <= 2f / 3f)
        {
            // Attack 30%, Pierce 30%, Shield 20%, Heal 20%
            if (randomValue < 0.3f)
            {
                Attack(attackValue);
            }
            else if (randomValue < 0.6f)
            {
                Pierce(pierceValue);
            }
            else if (randomValue < 0.8f)
            {
                Shield(shieldValue);
            }
            else
            {
                Heal(healValue);
            }
        }
        // 현재 HP가 1/3 이하일 때
        else
        {
            // Attack 20%, Pierce 20%, Shield 30%, Heal 30%
            if (randomValue < 0.2f)
            {
                Attack(attackValue);
            }
            else if (randomValue < 0.4f)
            {
                Pierce(attackValue);
            }
            else if (randomValue < 0.7f)
            {
                Shield(shieldValue);
            }
            else
            {
                Heal(healValue);
            }
        }
    }

    private void Attack(int attackvalue)
    {
        ani.SetTrigger("Punch_01");
        ani.SetTrigger("Idle");
        int remainingValue = attackvalue- Player.Instance.currentShield;
        if(Player.Instance.currentShield > 0)
        {
            Player.Instance.PlayerShieldAttack(attackvalue);
            
        }
        if(remainingValue > 0)
        {
            Player.Instance.PlayerTakeDamage(remainingValue);
        }
    }
    private void Pierce(int attackValue)
    {
        ani.SetTrigger("Punch_02");
        ani.SetTrigger("Idle");
        Player.Instance.PlayerTakeDamage(attackValue);
    }

    private void Heal(int healValue) 
    {
        if(currentEnemyHp == enemyData.enemyHp)
        {
            currentEnemyHp = enemyData.enemyHp;
        }
        else
        {
            currentEnemyHp += healValue;
            if(currentEnemyHp >= enemyData.enemyHp)
            {
                currentEnemyHp = enemyData.enemyHp;
            }
        }
        ani.SetTrigger("Victory");
        ani.SetTrigger("Idle");
        UpdateEnemyHp(currentEnemyHp); 
    }
    private void Shield(int shieldValue) 
    {
        currentEnemyShield += shieldValue;
        ani.SetTrigger("Victory");
        ani.SetTrigger("Idle");
        //enemyShields.SetActive(true);
        enemyShieldNum.text = $"Shield : {currentEnemyShield}";
    }

    private void Die()
    {
        //보상 선택지 넣기
        ani.SetTrigger("Death");
        CardGameManager.cardGameManager.ClearHandDeck();

        StartCoroutine(DelayedReward());
    }

    private IEnumerator DelayedReward()
    {
        yield return new WaitForSeconds(1.5f);

        reward.SetActive(true);
        CardGameManager.cardGameManager.ResetPlayerState();
        for(int i = 0; i < enemyParts.Length; i++)
        {
            enemyParts[i].enabled = false;
        }
        Player.Instance.haveMoney += enemyData.giveMoney;
        playerLevel.text = $"Level. {Player.Instance.playerlevel}";
        Player.Instance.exp += enemyData.giveExp;
        Debug.Log(Player.Instance.exp);
        Player.Instance.AddExp(Player.Instance.exp);
        playerExp.value = Player.Instance.exp;
        playerExp.maxValue = Player.Instance.nexttoexp;
        playerExpNum.text = $"{Player.Instance.exp} / {Player.Instance.nexttoexp}";
        enemyGiveMoney.text = $"+ {enemyData.giveMoney} Coin";
    }

    public void ReduceShield(int value)
    {
        currentEnemyShield -= value;
        enemyShieldNum.text = $"Shield : {currentEnemyShield}";

        if (currentEnemyShield <= 0)
        {
            currentEnemyShield = 0;
            enemyShieldNum.text = $"Shield : {currentEnemyShield}";
            //enemyShields.SetActive(false);
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        if(currentEnemyHp > 0)
        {
            currentEnemyHp -= damage;
        }
        // HP가 0보다 작아지는 경우 0으로 고정
        if (currentEnemyHp <= 0)
        {
            currentEnemyHp = 0;
            Die();
        }

        UpdateEnemyHp(currentEnemyHp);

        /*if (currentEnemyHp == 0)
        {
            Die();
        }*/
    }
}
