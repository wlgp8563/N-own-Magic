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
    public int maxHandDeckNum = 4;

    public ParticleSystem cardEffectPrefab;
    public Transform effectSpawnPoint;

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

        for (int i = 0; i < maxHandDeckNum && allCards.Count > 0; i++)
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

        float angleOffset = 15f;
        float startAngle = -angleOffset * (handDeck.Count - 1) / 2;

        for (int i = 0; i < handDeck.Count; i++)
        {
            Card card = handDeck[i];
            GameObject cardUI = Instantiate(cardUIPrefab, handDeckUIParent);
            CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
            cardUIComponent.SetCardData(card);

            float angle = startAngle + i * angleOffset;
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
                cardEffectPrefab = Resources.Load<ParticleSystem>("Effects/LightingEffects");
                break;

            case CardCategory.Pierce: // Pierce (ID: 4~6)
                ApplyPierceAttack(card, value);
                cardEffectPrefab = Resources.Load<ParticleSystem>("Effects/PierceEffects");
                break;

            case CardCategory.Shield: // Shield (ID: 7~9)
                ApplyShield(card, value);
                cardEffectPrefab = Resources.Load<ParticleSystem>("Effects/ShieldEffects");
                break;

            case CardCategory.Heal: // Heal (ID: 10~12)
                ApplyHeal(card, value);
                cardEffectPrefab = Resources.Load<ParticleSystem>("Effects/HealingEffects");
                break;

            case CardCategory.AddCard: // AddCard (ID: 13~15)
                DrawRandomCard(card, value);
                cardEffectPrefab = Resources.Load<ParticleSystem>("Effects/AddCardTurnEffects");
                break;

            case CardCategory.AddTurn: // AddTurn (ID: 16~18)
                AddExtraTurn(card, value);
                cardEffectPrefab = Resources.Load<ParticleSystem>("Effects/AddCardEffects");
                break;
        }
        player.DecreaseTurn();

        if (player.currentTurn == 0)
        {
            turnManager.EndPlayerTurn();
            player.currentTurn = player.playerturn;
        }
    }

    private void PlayCardEffect()
    {
        if (cardEffectPrefab != null && effectSpawnPoint != null)
        {
            ParticleSystem effect = Instantiate(cardEffectPrefab, effectSpawnPoint.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"ī���� ����Ʈ�� �����ϴ�.");
        }
    }

    public void ApplyAttack(Card card, int value)
    { 
        int shieldValue = EnemyControl.enemyControlInstance.currentEnemyShield;
        int remainingDamage = value - shieldValue;

        if (shieldValue > 0)
        {
            Debug.Log($"���� ���� {shieldValue}�� ����");
            EnemyControl.enemyControlInstance.ReduceShield(value);
        }

        if (remainingDamage > 0)
        {
            Debug.Log($"������ {remainingDamage}�� ������ ���մϴ�.");
            EnemyControl.enemyControlInstance.EnemyTakeDamage(remainingDamage);
        }
    }

    public void ApplyPierceAttack(Card card, int value)
    {
        Debug.Log($"������ {value}�� ���� ������ ���մϴ�. (���� ����)");
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
        float angleOffset = 15f;
        float startAngle = -angleOffset * (handDeck.Count - 1) / 2;
        float cardSpacing = 150f; // ī�� �� ����

        for (int i = 0; i < handDeck.Count; i++)
        {
            Card card = handDeck[i];

            // �����ִ� UI ī�� ��ü ã��
            CardUI[] cardUIs = handDeckUIParent.GetComponentsInChildren<CardUI>();
            foreach (CardUI cardUI in cardUIs)
            {
                if (cardUI.cardData == card)
                {
                    // ī�� ��ġ �� ȸ�� ����
                    float angle = startAngle + i * angleOffset;
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
