using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class CardGameManager : MonoBehaviour
{
    public Transform handDeckUIParent;
    public GameObject cardUIPrefab;
    public Vector3 handDeckCenterPosition = Vector3.zero;
    public float cardSpacing = 100f;
    public int maxHandDeckNum = 4;

    private List<Card> handDeck = new List<Card>();
    private Dictionary<int, Card> playerDeck;
    private Player player; // Player 스크립트 참조
    private EnemyControl enemy; // EnemyController 스크립트 참조

    private void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<EnemyControl>();

        playerDeck = CardManager.Instance.playerDeck;
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
            // 플레이어 턴 차감 및 확인
            player.DecreaseTurn();

            // 턴이 0이 되면 적군 턴으로 전환
            if (player.IsTurnOver())
            {
                StartEnemyTurn();
            }
        }
        else
        {
            rectTransform.localPosition = handDeckCenterPosition;
        }
    }

    // 카드 사용 로직
    private void UseCard(Card card)
    {
        Debug.Log($"카드 사용: {card.cardName}");
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

    // 적군 턴 시작
    private void StartEnemyTurn()
    {
        Debug.Log("적군의 턴이 시작됩니다.");

        foreach (var enemy in FindObjectsOfType < EnemyControl>())
        {
            enemy.enemyData.StartTurn();
            enemy.enemyData.TakeTurn();
        }

        // 적군 턴 종료 후 플레이어 턴 초기화
        player.ResetTurn();
    }
}
