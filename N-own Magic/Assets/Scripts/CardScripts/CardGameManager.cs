using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CardGameManager : MonoBehaviour
{
    public static CardGameManager cardGameManager;
    
    public Transform handDeckUIParent;
    public GameObject cardUIPrefab;
    public Vector3 handDeckCenterPosition = Vector3.zero;
    public float cardSpacing = 100f;
    //public int maxHandDeckNum = 4;

    public GameObject cardEffectPrefab;
    public Transform attackEffectSpawnPoint;
    public Transform healEffectSpawnPoint;
    public Transform shieldEffectSpawnPoint;
    public Transform addCardTurnEffectSpawnPoint;


    //private int shieldValue;
    private List<Card> handDeck = new List<Card>();
    private Dictionary<int, Card> playerDeck;
    private Player player; // Player ��ũ��Ʈ ����
    private EnemyControl enemyControl; // Enemy ��ũ��Ʈ ����
    private TurnManager turnManager;

    private void Awake()
    {
        if (cardGameManager == null)
        {
            cardGameManager = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        //enemy = FindObjectOfType<EnemyControl>();

        //shieldValue = EnemyControl.Instance.currentShield;
        playerDeck = CardManager.CardManagerInstance.playerDeck;
        DrawHandDeck();
        DisplayHandDeckUI();
    }

    // �ڵ� �� �ʱ�ȭ
    public void DrawHandDeck()
    {
        handDeck.Clear();
        List<Card> allCards = playerDeck.Values.SelectMany(card => Enumerable.Repeat(card, card.count)).ToList();

        for (int i = 0; i < Player.Instance.handdecknum && allCards.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, allCards.Count);
            Card randomCard = allCards[randomIndex];
            handDeck.Add(randomCard);
            allCards.RemoveAt(randomIndex);
        }
    }

    public void ClearHandDeck()
    {
        handDeck.Clear();
        foreach (Transform child in handDeckUIParent)
        {
            Destroy(child.gameObject);
        }
    }

    // �ڵ� �� UI ǥ��
    public void DisplayHandDeckUI()
    {
        foreach (Transform child in handDeckUIParent)
        {
            Destroy(child.gameObject);
        }

        float angleOffset = 9f;
        float startAngle = -angleOffset * (handDeck.Count - 1) / 2;

        for (int i = 0; i < handDeck.Count; i++)
        {
            Card card = handDeck[i];
            GameObject cardUI = Instantiate(cardUIPrefab, handDeckUIParent);
            CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
            cardUIComponent.SetCardData(card);

            float angle = startAngle + i * angleOffset;
            //Vector3 position = handDeckCenterPosition + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * cardSpacing, Mathf.Sin(Mathf.Deg2Rad * angle) * cardSpacing, 0);
            Vector3 position = handDeckCenterPosition + new Vector3(i * cardSpacing - (cardSpacing * (handDeck.Count - 1) / 2), 0, 0);
            cardUI.transform.localPosition = position;
            cardUI.transform.rotation = Quaternion.Euler(0, 0, angle);

            AddMouseEffects(cardUI);
        }
    }

    // ���콺 ���� ȿ�� �� �巡�� �̺�Ʈ �߰�
    private void AddMouseEffects(GameObject cardUI)
    {
        RectTransform rectTransform = cardUI.GetComponent<RectTransform>();
        EventTrigger eventTrigger = cardUI.GetComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        pointerEnter.callback.AddListener((eventData) =>
        {
            rectTransform.localScale = Vector3.one * 1.3f;
            rectTransform.SetAsLastSibling();
        });
        eventTrigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        pointerExit.callback.AddListener((eventData) =>
        {
            rectTransform.localScale = Vector3.one;
        });
        eventTrigger.triggers.Add(pointerExit);

        EventTrigger.Entry drag = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
        drag.callback.AddListener((data) => OnDragCard(rectTransform, (PointerEventData)data));
        eventTrigger.triggers.Add(drag);

        EventTrigger.Entry endDrag = new EventTrigger.Entry { eventID = EventTriggerType.EndDrag };
        endDrag.callback.AddListener((data) => OnEndDragCard(rectTransform, (PointerEventData)data));
        eventTrigger.triggers.Add(endDrag);
    }

    // ī�� �巡�� ��
    private void OnDragCard(RectTransform rectTransform, PointerEventData data)
    {
        rectTransform.position = data.position;
    }

    // ī�� �巡�� ���� ��
    private void OnEndDragCard(RectTransform rectTransform, PointerEventData data)
    {
        if (IsDroppedOnEnemyArea(data.position))
        {
            Card usedCard = rectTransform.GetComponent<CardUI>().cardData;
            UseCard(usedCard);
            handDeck.Remove(usedCard);
            Destroy(rectTransform.gameObject);

            RepositionHandDeck();
        }
        else
        {
            rectTransform.localPosition = handDeckCenterPosition;
        }
    }

    // ī�� ��� ����
    private void UseCard(Card card)
    {
        int value = card.GetEffectValueByCategoryAndLevel();
        //Player.Instance.currentLightEnergy = Player.Instance.lightenergy;
        int lightEnergyCost = card.lightEnergy;

        if(Player.Instance.currentLightEnergy < lightEnergyCost)
        {
            Debug.Log($"{card.cardName} ī�带 ����� �� �����ϴ�. LightEnergy�� �����մϴ�. �ʿ� ������: {lightEnergyCost}, ���� ������: {player.currentLightEnergy}");
            return;
        }
        else
        {
            Player.Instance.DecreaseLightEnergy(lightEnergyCost);
        }

        Debug.Log($"ī�� ���: {card.cardName}, ȿ��: {value}");

        switch (card.category)
        {
            case CardCategory.Attack: // Attack (ID: 1~3)
                ApplyAttack(card, value);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/Lighting");
                EnemyControl.enemyControlInstance.PlayerAttackEffect(cardEffectPrefab);
                //BossControl.bossControlInstance.PlayerAttackEffect(cardEffectPrefab);
                //cardEffectPrefab = Resources.Load<GameObject>("Effects/Lighting");
                //AttackEffect();
                break;

            case CardCategory.Pierce: // Pierce (ID: 4~6)
                ApplyPierceAttack(card, value);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/Pierce");
                EnemyControl.enemyControlInstance.PlayerAttackEffect(cardEffectPrefab);
                //BossControl.bossControlInstance.PlayerAttackEffect(cardEffectPrefab);
                //cardEffectPrefab = Resources.Load<GameObject>("Effects/Pierce");
                //AttackEffect();
                break;

            case CardCategory.Shield: // Shield (ID: 7~9)
                ApplyShield(card, value);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/Healing");
                EnemyControl.enemyControlInstance.PlayerShieldEffect(cardEffectPrefab);
                //BossControl.bossControlInstance.PlayerShieldEffect(cardEffectPrefab);
                //ShieldEffect();
                break;

            case CardCategory.Heal: // Heal (ID: 10~12)
                ApplyHeal(card, value);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/Healing");
                EnemyControl.enemyControlInstance.PlayerHealEffect(cardEffectPrefab);
                //BossControl.bossControlInstance.PlayerHealEffect(cardEffectPrefab);
                //HealEffect();
                break;

            case CardCategory.AddCard: // AddCard (ID: 13~15)
                DrawRandomCard(card, value);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/AddCardTurn");
                EnemyControl.enemyControlInstance.PlayerAddTurnCardEffect(cardEffectPrefab);
                //BossControl.bossControlInstance.PlayerAddTurnCardEffect(cardEffectPrefab);
                //AddCardTurnEffect();
                break;

            case CardCategory.AddTurn: // AddTurn (ID: 16~18)
                AddExtraTurn(card, value);
                cardEffectPrefab = Resources.Load<GameObject>("Effects/AddCardTurn");
                EnemyControl.enemyControlInstance.PlayerAddTurnCardEffect(cardEffectPrefab);
                //BossControl.bossControlInstance.PlayerAddTurnCardEffect(cardEffectPrefab);
                //AddCardTurnEffect();
                break;
        }
        player.DecreaseTurn();

        if (player.currentTurn == 0)
        {
            turnManager.EndPlayerTurn();
            player.currentTurn = player.playerturn;
        }
    }

    /*private void HealEffect()
    {
        if (cardEffectPrefab != null && healEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, healEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(healEffectSpawnPoint, false); // effectSpawnPoint�� �θ�� ���� (Canvas ����)
            effect.transform.localPosition = new Vector3(0, 0, 10); // �θ� ��ġ �������� ���� ��ġ ����
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Effects";
            renderer.sortingOrder = 20; // UI ���� ��Ÿ������ ���� ���� ����
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"ī���� ����Ʈ�� �����ϴ�.");
        }
    }

    private void ShieldEffect()
    {
        if (cardEffectPrefab != null && shieldEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, shieldEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(shieldEffectSpawnPoint, false); // effectSpawnPoint�� �θ�� ���� (Canvas ����)
            effect.transform.localPosition = new Vector3(0, 0, 10); // �θ� ��ġ �������� ���� ��ġ ����
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Effects";
            renderer.sortingOrder = 20; // UI ���� ��Ÿ������ ���� ���� ����
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"ī���� ����Ʈ�� �����ϴ�.");
        }
    }

    private void AddCardTurnEffect()
    {
        if (cardEffectPrefab != null && addCardTurnEffectSpawnPoint != null)
        {
            GameObject effect = Instantiate(cardEffectPrefab, addCardTurnEffectSpawnPoint.position, Quaternion.identity);
            effect.transform.SetParent(addCardTurnEffectSpawnPoint, false); // effectSpawnPoint�� �θ�� ���� (Canvas ����)
            effect.transform.localPosition = new Vector3(0, 0, 10); // �θ� ��ġ �������� ���� ��ġ ����
            var renderer = effect.GetComponent<Renderer>();
            renderer.sortingLayerName = "Effects";
            renderer.sortingOrder = 20; // UI ���� ��Ÿ������ ���� ���� ����
            Destroy(effect, 1.0f);
            //Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"ī���� ����Ʈ�� �����ϴ�.");
        }
    }*/

    public void ApplyAttack(Card card, int value)
    {
        /*int bossshieldValue = BossControl.bossControlInstance.currentEnemyShield;
        int bossremainingDamage = value - bossshieldValue;
        if (bossshieldValue > 0)
        {
            BossControl.bossControlInstance.EnemyTakeDamage(value);
        }
        if (bossremainingDamage > 0)
        {
            BossControl.bossControlInstance.EnemyTakeDamage(bossremainingDamage);
        }*/


        /*if (BossControl.bossControlInstance.isboss == false)
        {
            int shieldValue = EnemyControl.enemyControlInstance.currentEnemyShield;
            int remainingDamage = value - shieldValue;
            if (shieldValue > 0)
            {
                EnemyControl.enemyControlInstance.ReduceShield(value);
            }
            //EnemyControl.enemyControlInstance.ReduceShield(value);
            if (remainingDamage > 0)
            {
                EnemyControl.enemyControlInstance.ReduceShield(remainingDamage);
            }
        }
        else
        {
            int bossshieldValue = BossControl.bossControlInstance.currentEnemyShield;
            int bossremainingDamage = value - bossshieldValue;
            if (bossshieldValue > 0)
            {
                BossControl.bossControlInstance.EnemyTakeDamage(value);
            }
            if(bossremainingDamage > 0)
            {
                BossControl.bossControlInstance.EnemyTakeDamage(bossremainingDamage);
            }
            //BossControl.bossControlInstance.EnemyTakeDamage(value);
        }*/

        int shieldValue = EnemyControl.enemyControlInstance.currentEnemyShield;
        int remainingDamage = value - shieldValue;

        //int bossshieldValue = BossControl.bossControlInstance.currentEnemyShield;
        //int bossremainingDamage = value - bossshieldValue;

        if (shieldValue > 0)
        {
            Debug.Log($"���� ���� {shieldValue}�� ����");
            EnemyControl.enemyControlInstance.ReduceShield(value);
            /*if (!BossControl.bossControlInstance.isboss)
            {
                EnemyControl.enemyControlInstance.ReduceShield(value);
            }
            else
            {
                BossControl.bossControlInstance.EnemyTakeDamage(value);
            }*/
        }

        if (remainingDamage > 0)
        {
            EnemyControl.enemyControlInstance.EnemyTakeDamage(remainingDamage);
            Debug.Log($"������ {remainingDamage}�� ������ ���մϴ�.");
            /*if (!BossControl.bossControlInstance.isboss)
            {
                EnemyControl.enemyControlInstance.ReduceShield(remainingDamage);
            }
            else
            {
                BossControl.bossControlInstance.EnemyTakeDamage(bossremainingDamage);
            }*/
            //EnemyControl.enemyControlInstance.EnemyTakeDamage(remainingDamage);
        }
    }

    public void ApplyPierceAttack(Card card, int value)
    {
        Debug.Log($"������ {value}�� ���� ������ ���մϴ�. (���� ����)");
        /*bool bosscheck = BossControl.bossControlInstance.isboss;
        if (bosscheck == false)
        {
            EnemyControl.enemyControlInstance.EnemyTakeDamage(value);
        }
        else
        {
            BossControl.bossControlInstance.EnemyTakeDamage(value);
        }*/
        EnemyControl.enemyControlInstance.EnemyTakeDamage(value);
    }

    private void ApplyShield(Card card, int value)
    {
        Debug.Log($"�÷��̾ {value}�� ���带 ȹ���մϴ�.");
        Player.Instance.AddShield(value);
    }

    private void ApplyHeal(Card card, int value)
    {
        Debug.Log($"�÷��̾ {value}�� ü���� ȸ���մϴ�.");
        Player.Instance.Heal(value);
    }

    private void DrawRandomCard(Card card, int value)
    {
        handDeck.Remove(card);
        RepositionHandDeck();
        //DisplayHandDeckUI();

        for (int i = 0; i < value; i++)
        {
            if (CardManager.CardManagerInstance.playerDeck.Count > 0)
            {
                // ������ �����ϰ� �� �� �߰�
                int randomIndex = Random.Range(0, CardManager.CardManagerInstance.playerDeck.Count);
                int cardID = new List<int>(CardManager.CardManagerInstance.playerDeck.Keys)[randomIndex];
                Card drawnCard = CardManager.CardManagerInstance.CreateCardByID(cardID);

                // HandDeck�� �߰�
                CardManager.CardManagerInstance.handDeck.Add(drawnCard);
                Debug.Log($"{drawnCard.cardName} ī�带 �̾ҽ��ϴ�.");

                GameObject cardUI = Instantiate(cardUIPrefab, handDeckUIParent);
                CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
                cardUIComponent.SetCardData(drawnCard);

                //RepositionHandDeck();
                AddMouseEffects(cardUI);
            }
        }
        //DisplayHandDeckUI();
        RepositionHandDeck();
        
        //RepositionHandDeck();
    }

    public void ResetPlayerState()
    {
        player.currentTurn = player.playerturn;
        player.currentLightEnergy = player.lightenergy;
        player.currentShield = 0;
    }

    private void AddExtraTurn(Card card, int value)
    {
        player.IncreaseTurn(value);
        Debug.Log($"�÷��̾ {value}�� �߰� ���� ȹ���մϴ�.");
        //TurnManager.Instance.AddTurns(value);
    }

    private void RepositionHandDeck()
    {
        // ��ä�÷� ���ġ
        float angleOffset = 9f;
        float startAngle = -angleOffset * (handDeck.Count - 1) / 2;
        float cardSpacing = 150f; // ī�� �� ����

        CardUI[] cardUIs = handDeckUIParent.GetComponentsInChildren<CardUI>();

        for (int i = 0; i < handDeck.Count; i++)
        {
            Card card = handDeck[i];

            // �����ִ� UI ī�� ��ü ã��
            //CardUI[] cardUIs = handDeckUIParent.GetComponentsInChildren<CardUI>();
            foreach (CardUI cardUI in cardUIs)
            {
                if (cardUI.cardData == card)
                {
                    // ī�� ��ġ �� ȸ�� ����
                    float angle = startAngle + i * angleOffset;
                    //Vector3 position = handDeckCenterPosition + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * cardSpacing, Mathf.Sin(Mathf.Deg2Rad * angle) * cardSpacing, 0);
                    Vector3 position = handDeckCenterPosition + new Vector3(i * cardSpacing - (cardSpacing * (handDeck.Count - 1) / 2), 0, 0);
                    cardUI.transform.localPosition = position;
                    cardUI.transform.rotation = Quaternion.Euler(0, 0, angle);

                    break;
                }
            }
        }
    }

    // ���� ��� ��ġ Ȯ��
    private bool IsDroppedOnEnemyArea(Vector2 position)
    {
        return position.y > Screen.height / 2;
    }
}
