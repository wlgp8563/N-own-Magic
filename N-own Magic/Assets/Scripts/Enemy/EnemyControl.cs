using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl enemyControlInstance;

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

    public GameObject level2reward;
    private bool isLevelup2 = false;

    public GameObject cardEffectPrefab;
    public Transform attackEffectSpawnPoint;
    public Transform healshieldEffectSpawnPoint;
    public Transform playerAttackEffectSpawnPoint;
    public Transform playerShieldEffectSpawnPoint;
    public Transform playerHealEffectSpawnPoint;
    public Transform playerAddTurnCardEffectSpawnPoint;

    private Animator ani;

    public AudioClip levelupAudio;
    public AudioSource levelupSource;

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
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyAttack");
                AttackEffect();
            }
            else if (randomValue < 0.8f)
            {
                Pierce(pierceValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                AttackEffect();
            }
            else
            {
                Shield(shieldValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyHealShield");
            }
        }
        // 현재 HP가 1/3 이상 2/3 이하일 때
        else if (hpRatio > 1f / 3f && hpRatio <= 2f / 3f)
        {
            // Attack 30%, Pierce 30%, Shield 20%, Heal 20%
            if (randomValue < 0.3f)
            {
                Attack(attackValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyAttack");
                AttackEffect();
            }
            else if (randomValue < 0.6f)
            {
                Pierce(pierceValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                AttackEffect();
            }
            else if (randomValue < 0.8f)
            {
                Shield(shieldValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyHealShield");
                HealShieldEffect();
            }
            else
            {
                Heal(healValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyHealShield");
                HealShieldEffect();
            }
        }
        // 현재 HP가 1/3 이하일 때
        else
        {
            // Attack 20%, Pierce 20%, Shield 30%, Heal 30%
            if (randomValue < 0.2f)
            {
                Attack(attackValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyAttack");
                AttackEffect();
            }
            else if (randomValue < 0.4f)
            {
                Pierce(attackValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                AttackEffect();
            }
            else if (randomValue < 0.7f)
            {
                Shield(shieldValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyHealShield");
                HealShieldEffect();
            }
            else
            {
                Heal(healValue);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyHealShield");
                HealShieldEffect();
            }
        }
    }

    private void AttackEffect()
    {
        if (cardEffectPrefab != null && attackEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, attackEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(attackEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Prefab";
            renderer.sortingOrder = 15; // UI 위에 나타나도록 값을 높게 설정
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
        }
    }

    private void HealShieldEffect()
    {
        if (cardEffectPrefab != null && healshieldEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, healshieldEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(healshieldEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Prefab";
            renderer.sortingOrder = 15; // UI 위에 나타나도록 값을 높게 설정
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
        }
    }

    public void PlayerAttackEffect(GameObject cardEffectPrefab)
    {
        if (cardEffectPrefab != null && playerAttackEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, playerAttackEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(playerAttackEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Prefab";
            renderer.sortingOrder = 15; // UI 위에 나타나도록 값을 높게 설정
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
        }
    }

    public void PlayerHealEffect(GameObject cardEffectPrefab)
    {
        if (cardEffectPrefab != null && playerHealEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, playerHealEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(playerHealEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Prefab";
            renderer.sortingOrder = 15; // UI 위에 나타나도록 값을 높게 설정
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
        }
    }

    public void PlayerShieldEffect(GameObject cardEffectPrefab)
    {
        if (cardEffectPrefab != null && playerShieldEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, playerShieldEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(playerShieldEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Prefab";
            renderer.sortingOrder = 15; // UI 위에 나타나도록 값을 높게 설정
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
        }
    }

    public void PlayerAddTurnCardEffect(GameObject cardEffectPrefab)
    {
        if (cardEffectPrefab != null && playerAddTurnCardEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, playerAddTurnCardEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(playerAddTurnCardEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Prefab";
            renderer.sortingOrder = 15; // UI 위에 나타나도록 값을 높게 설정
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
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
        CardGameManager.cardGameManager.ResetPlayerState();
        for(int i = 0; i < enemyParts.Length; i++)
        {
            enemyParts[i].enabled = false;
        }
        Player.Instance.haveMoney += enemyData.giveMoney;
        Player.Instance.exp += enemyData.giveExp;
        playerExp.maxValue = Player.Instance.nexttoexp;
        Player.Instance.AddExp(Player.Instance.exp);
        playerExp.value = Player.Instance.exp;
        playerLevel.text = $"Level. {Player.Instance.playerlevel}";
        playerExpNum.text = $"{Player.Instance.exp} / {Player.Instance.nexttoexp}";
        enemyGiveMoney.text = $"+ {enemyData.giveMoney} Coin";
        if(isLevelup2 == true)
        {
            LevelUp2Reward();
        }
        reward.SetActive(true);
    }

    public void ReduceShield(int value)
    {
        //cardEffectPrefab = Resources.Load<GameObject>("Effects/Lighting");
        //PlayerAttackEffect();
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
        //cardEffectPrefab = Resources.Load<GameObject>("Effects/Pierce");
        //PlayerAttackEffect();
        if (currentEnemyHp > 0)
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
    }

    public void LevelUp2Reward()
    {
        isLevelup2 = true;
        level2reward.SetActive(true);
        levelupSource.Play();
        StartCoroutine(DisappearReward());
    }
    private IEnumerator DisappearReward()
    {
        yield return new WaitForSeconds(1.5f);
        level2reward.SetActive(false);
    }
}
