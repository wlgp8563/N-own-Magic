using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossControl : MonoBehaviour
{
    public static BossControl bossControlInstance;

    public TMP_Text enemyShieldNum;
    public Enemy enemyData; // ScriptableObject 연결
    public Slider hpSlider;
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

    private bool isReborn = false; // 부활 여부
    private bool isSecondPhase = false; // 2페이즈 여부

    public bool isboss = true;

    public GameObject cardEffectPrefab;
    public Transform attackEffectSpawnPoint;
    public Transform healshieldEffectSpawnPoint;
    public Transform playerAttackEffectSpawnPoint;
    public Transform playerShieldEffectSpawnPoint;
    public Transform playerHealEffectSpawnPoint;
    public Transform playerAddTurnCardEffectSpawnPoint;

    public AudioClip enemyDeathAudio;
    public AudioSource audioSource;

    private void Awake()
    {
        if (bossControlInstance == null)
        {
            bossControlInstance = this;
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
        isboss = true;
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

    private IEnumerator PerformActionsWithDelay()
    {
        while (remainingTurns > 0 && currentEnemyHp > 0)
        {
            PerformAction();
            remainingTurns--;
            yield return new WaitForSeconds(1.5f);
        }

        if (remainingTurns <= 0)
        {
            remainingTurns = enemyData.turnCount;
            TurnManager.TurnManagerInstance.EndEnemyTurn();
        }
    }

    public void PerformAction()
    {
        float hpRatio = (float)currentEnemyHp / enemyData.enemyHp;
        float randomValue = Random.value;

        int attackValue = enemyData.attack;
        int pierceValue = enemyData.pierce;
        int shieldValue = enemyData.shieldSelf;
        int healValue = enemyData.healSelf;
        int ultiValue = enemyData.uitiAttack;

        if (!isSecondPhase)
        {
            // 1페이즈 행동 (enemy와 동일)
            if (hpRatio > 2f / 3f)
            {
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
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
            }
            else if (hpRatio > 1f / 3f && hpRatio <= 2f / 3f)
            {
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
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
                else
                {
                    Heal(healValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
            }
            else
            {
                if (randomValue < 0.2f)
                {
                    Attack(attackValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyAttack");
                    AttackEffect();
                }
                else if (randomValue < 0.4f)
                {
                    Pierce(pierceValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                    AttackEffect();
                }
                else if (randomValue < 0.7f)
                {
                    Shield(shieldValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
                else
                {
                    Heal(healValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
            }
        }
        else
        {
            
            // 2페이즈 행동
            if (hpRatio > 2f / 3f)
            {
                if (randomValue < 0.45f)
                {
                    Ulti(ultiValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossUlti");
                    AttackEffect();
                }
                else if (randomValue < 0.65f)
                {
                    Attack(attackValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyAttack");
                    AttackEffect();
                }
                else if (randomValue < 0.9f)
                {
                    Pierce(pierceValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                    AttackEffect();
                }
                else
                {
                    Shield(shieldValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
            }
            else if (hpRatio > 1f / 3f && hpRatio <= 2f / 3f)
            {
                if (randomValue < 0.15f)
                {
                    Ulti(ultiValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossUlti");
                    AttackEffect();
                }
                else if (randomValue < 0.35f)
                {
                    Attack(attackValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyAttack");
                    AttackEffect();
                }
                else if (randomValue < 0.55f)
                {
                    Pierce(pierceValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                    AttackEffect();
                }
                else if (randomValue < 0.7f)
                {
                    Shield(shieldValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
                else
                {
                    Heal(healValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
            }
            else
            {
                if (randomValue < 0.5f)
                {
                    Ulti(ultiValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossUlti");
                    AttackEffect();
                }
                else if (randomValue < 0.8f)
                {
                    Heal(healValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/BossHS");
                    HealShieldEffect();
                }
                else
                {
                    Pierce(pierceValue);
                    cardEffectPrefab = Resources.Load<GameObject>("Effects/EnemyPierce");
                    AttackEffect();
                }
            }
        }
    }

    private void AttackEffect()
    {
        if (cardEffectPrefab != null && attackEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, attackEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(attackEffectSpawnPoint, false); // effectSpawnPoint를 부모로 설정 (Canvas 내부)
            //effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
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
            //effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
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
            //effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
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
            //effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
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
            //effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
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
            //effect.transform.localPosition = new Vector3(0, 0, 10); // 부모 위치 기준으로 로컬 위치 설정
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

    private void Ulti(int ultiValue)
    {
        ani.SetTrigger("Cast3");
        ani.SetTrigger("Idle");
        Player.Instance.PlayerTakeDamage(ultiValue);
    }

    private void Attack(int attackValue)
    {
        ani.SetTrigger("Cast1");
        ani.SetTrigger("Idle");
        int remainingValue = attackValue - Player.Instance.currentShield;
        if (Player.Instance.currentShield > 0)
        {
            Player.Instance.PlayerShieldAttack(attackValue);

        }
        if (remainingValue > 0)
        {
            Player.Instance.PlayerTakeDamage(remainingValue);
        }
    }

    private void Pierce(int pierceValue)
    {
        ani.SetTrigger("Cast2");
        ani.SetTrigger("Idle");
        Player.Instance.PlayerTakeDamage(pierceValue);
    }

    private void Heal(int healValue)
    {
        //currentEnemyHp = Mathf.Min(currentEnemyHp + healValue, enemyData.enemyHp);
        if (currentEnemyHp == enemyData.enemyHp)
        {
            currentEnemyHp = enemyData.enemyHp;
        }
        else
        {
            currentEnemyHp += healValue;
            if (currentEnemyHp >= enemyData.enemyHp)
            {
                currentEnemyHp = enemyData.enemyHp;
            }
        }
        ani.SetTrigger("HealShield");
        ani.SetTrigger("Idle");
        UpdateEnemyHp(currentEnemyHp);
    }

    private void Shield(int shieldValue)
    {
        currentEnemyShield += shieldValue;
        ani.SetTrigger("HealShield");
        ani.SetTrigger("Idle");
        enemyShieldNum.text = $"Shield : {currentEnemyShield}";
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.0f);

        if (!isReborn)
        {
            isReborn = true;
            isSecondPhase = true;
            ani.SetTrigger("Dead");
            StartCoroutine(RebornDealy());
            /*ani.SetTrigger("Reborn");
            enemyData.enemyHp -= 50;
            hpSlider.maxValue = enemyData.enemyHp;
            currentEnemyHp = enemyData.enemyHp;
            UpdateEnemyHp(currentEnemyHp);
            Debug.Log("Boss has reborn to phase 2!");*/
        }
        else
        {
            ani.SetTrigger("Dead");
            StartCoroutine(DelayedReward());
        }
    }

    private IEnumerator RebornDealy()
    {
        yield return new WaitForSeconds(2.0f);
        cardEffectPrefab = Resources.Load<GameObject>("Effects/Reborn");
        HealShieldEffect();
        ani.SetTrigger("Reborn");
        enemyData.enemyHp -= 50;
        hpSlider.maxValue = enemyData.enemyHp;
        currentEnemyHp = enemyData.enemyHp;
        UpdateEnemyHp(currentEnemyHp);
    }

    private IEnumerator DelayedReward()
    {
        yield return new WaitForSeconds(1.5f);
        CardGameManager.cardGameManager.ClearHandDeck();
        CardGameManager.cardGameManager.ResetPlayerState();
        reward.SetActive(true); //이거 대신 이제 아빠 편지로 대체
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
        if (currentEnemyHp > 0)
        {
            currentEnemyHp -= damage;
        }
        // HP가 0보다 작아지는 경우 0으로 고정
        if (currentEnemyHp <= 0)
        {
            currentEnemyHp = 0;
            StartCoroutine(Die());
        }

        UpdateEnemyHp(currentEnemyHp);
    }
}
