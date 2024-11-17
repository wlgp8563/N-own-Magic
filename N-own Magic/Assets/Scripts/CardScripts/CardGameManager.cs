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
    private Player player; // Player 스크립트 참조
    private EnemyControl enemyControl; // Enemy 스크립트 참조
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

    // 핸드 덱 초기화
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

    // 핸드 덱 UI 표시
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

    // 마우스 오버 효과 및 드래그 이벤트 추가
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

    // 카드 드래그 시
    private void OnDragCard(RectTransform rectTransform, PointerEventData data)
    {
        rectTransform.position = data.position;
    }

    // 카드 드래그 종료 시
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

    // 카드 사용 로직
    private void UseCard(Card card)
    {
        int value = card.GetEffectValueByCategoryAndLevel();
        //Player.Instance.currentLightEnergy = Player.Instance.lightenergy;
        int lightEnergyCost = card.lightEnergy;

        if(Player.Instance.currentLightEnergy < lightEnergyCost)
        {
            Debug.Log($"{card.cardName} 카드를 사용할 수 없습니다. LightEnergy가 부족합니다. 필요 에너지: {lightEnergyCost}, 현재 에너지: {player.currentLightEnergy}");
            return;
        }
        else
        {
            Player.Instance.DecreaseLightEnergy(lightEnergyCost);
        }

        Debug.Log($"카드 사용: {card.cardName}, 효과: {value}");

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
            Debug.LogWarning($"카드의 이펙트가 없습니다.");
        }
    }

    public void ApplyAttack(Card card, int value)
    { 
        int shieldValue = EnemyControl.enemyControlInstance.currentEnemyShield;
        int remainingDamage = value - shieldValue;

        if (shieldValue > 0)
        {
            Debug.Log($"적의 쉴드 {shieldValue}를 제거");
            EnemyControl.enemyControlInstance.ReduceShield(value);
        }

        if (remainingDamage > 0)
        {
            Debug.Log($"적에게 {remainingDamage}의 공격을 가합니다.");
            EnemyControl.enemyControlInstance.EnemyTakeDamage(remainingDamage);
        }
    }

    public void ApplyPierceAttack(Card card, int value)
    {
        Debug.Log($"적에게 {value}의 관통 공격을 가합니다. (쉴드 무시)");
        EnemyControl.enemyControlInstance.EnemyTakeDamage(value);
    }

    private void ApplyShield(Card card, int value)
    {
        Debug.Log($"플레이어가 {value}의 쉴드를 획득합니다.");
        Player.Instance.AddShield(value);
    }

    private void ApplyHeal(Card card, int value)
    {
        Debug.Log($"플레이어가 {value}의 체력을 회복합니다.");
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
                // 덱에서 랜덤하게 한 장 추가
                int randomIndex = Random.Range(0, CardManager.CardManagerInstance.playerDeck.Count);
                int cardID = new List<int>(CardManager.CardManagerInstance.playerDeck.Keys)[randomIndex];
                Card drawnCard = CardManager.CardManagerInstance.CreateCardByID(cardID);

                // HandDeck에 추가
                CardManager.CardManagerInstance.handDeck.Add(drawnCard);
                Debug.Log($"{drawnCard.cardName} 카드를 뽑았습니다.");

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
        Debug.Log($"플레이어가 {value}의 추가 턴을 획득합니다.");
        //TurnManager.Instance.AddTurns(value);
    }

    private void RepositionHandDeck()
    {
        // 부채꼴로 재배치
        float angleOffset = 15f;
        float startAngle = -angleOffset * (handDeck.Count - 1) / 2;
        float cardSpacing = 150f; // 카드 간 간격

        for (int i = 0; i < handDeck.Count; i++)
        {
            Card card = handDeck[i];

            // 남아있는 UI 카드 객체 찾기
            CardUI[] cardUIs = handDeckUIParent.GetComponentsInChildren<CardUI>();
            foreach (CardUI cardUI in cardUIs)
            {
                if (cardUI.cardData == card)
                {
                    // 카드 위치 및 회전 설정
                    float angle = startAngle + i * angleOffset;
                    Vector3 position = handDeckCenterPosition + new Vector3(i * cardSpacing - (cardSpacing * (handDeck.Count - 1) / 2), 0, 0);
                    cardUI.transform.localPosition = position;
                    cardUI.transform.rotation = Quaternion.Euler(0, 0, angle);

                    break;
                }
            }
        }
    }

    // 적군 드롭 위치 확인
    private bool IsDroppedOnEnemyArea(Vector2 position)
    {
        return position.y > Screen.height / 2;
    }
}
