using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFusionManager : MonoBehaviour
{
    public static CardFusionManager Instance;

    [Header("UI Elements")]
    public GameObject fusionShopUI; // 카드 합성 shop UI
    public GameObject cardUIPrefab; // 카드 UI 프리팹
    public Transform cardContentParent; // 카드 리스트 부모 (스크롤 가능한 영역)
    public Button fusionButton; // 합성하기 버튼

    private List<Card> selectedCards = new List<Card>(); // 합성할 카드들
    private Dictionary<int, Card> playerDeck => CardManager.Instance.playerDeck; // playerDeck 참조

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fusionButton.onClick.AddListener(FuseSelectedCards);
        DisplayCardsInFusionShop();
    }

    public void OpenFusionShop()
    {
        //fusionShopUI.SetActive(true);
        //DisplayCardsInFusionShop();
    }

    public void CloseFusionShop()
    {
        fusionShopUI.SetActive(false);
        selectedCards.Clear();
    }

    // 카드 리스트 UI 표시
    private void DisplayCardsInFusionShop()
    {
        // 기존 UI 카드 삭제
        foreach (Transform child in cardContentParent)
        {
            Destroy(child.gameObject);
        }

        // playerDeck에 있는 모든 카드 표시
        foreach (var entry in playerDeck)
        {
            int cardID = entry.Key;
            Card card = entry.Value;

            for (int i = 0; i < card.count; i++)
            {
                GameObject cardUI = Instantiate(cardUIPrefab, cardContentParent);

                SetSortingLayer(cardUI, 2);

                cardUI.GetComponent<CardUI>().SetCardData(card);
                Button cardButton = cardUI.GetComponent<Button>();

                cardButton.onClick.AddListener(() => SelectCardForFusion(card));
            }
        }
    }

    private void SetSortingLayer(GameObject cardUI, int sortingLayer)
    {
        // Canvas가 있는 경우
        Canvas canvas = cardUI.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = sortingLayer;
        }

        // SpriteRenderer가 있는 경우
        SpriteRenderer spriteRenderer = cardUI.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingLayer;
        }
    }

    // 합성할 카드 선택
    private void SelectCardForFusion(Card card)
    {
        // 같은 ID를 가진 카드 3장만 선택 가능
        if (selectedCards.Count < 3)
        {
            selectedCards.Add(card);
            Debug.Log($"{card.cardName} 선택됨. 현재 선택된 카드 수: {selectedCards.Count}");

            if (selectedCards.Count == 3)
            {
                // 선택된 카드 3장이 모두 같은 ID인지 확인
                if (selectedCards[0].cardID == selectedCards[1].cardID && selectedCards[1].cardID == selectedCards[2].cardID)
                {
                    fusionButton.interactable = true;
                }
                else
                {
                    Debug.Log("카드 ID가 다릅니다. 다시 선택하세요.");
                    selectedCards.Clear();
                    fusionButton.interactable = false;
                }
            }
        }
    }

    // 카드 합성 기능
    private void FuseSelectedCards()
    {
        if (selectedCards.Count != 3)
        {
            Debug.Log("카드 3장을 선택해야 합니다.");
            return;
        }

        int selectedCardID = selectedCards[0].cardID;
        int nextLevelCardID = GetNextLevelCardID(selectedCardID);
        Player.Instance.DecreaseFusion();

        if (nextLevelCardID == -1)
        {
            Debug.Log("더 높은 레벨의 카드가 존재하지 않습니다.");
            selectedCards.Clear();
            fusionButton.interactable = false;
            return;
        }

        // playerDeck에서 선택된 카드 제거
        foreach (Card card in selectedCards)
        {
            if (playerDeck.ContainsKey(card.cardID))
            {
                playerDeck[card.cardID].count -= 1;
                if (playerDeck[card.cardID].count == 0)
                {
                    playerDeck.Remove(card.cardID);
                }
            }
        }

        // 새로운 합성된 카드 추가
        Card newCard = CardManager.Instance.CreateCardByID(nextLevelCardID);
        CardManager.Instance.AddNewCardToDeck(nextLevelCardID, 1);

        Debug.Log($"카드 합성 완료: {newCard.cardName} 획득");

        // UI 갱신
        selectedCards.Clear();
        fusionButton.interactable = false;
        DisplayCardsInFusionShop();
    }

    // 다음 레벨 카드 ID 가져오기
    private int GetNextLevelCardID(int currentCardID)
    {
        switch (currentCardID)
        {
            case 1: return 2;
            case 2: return 3;
            case 4: return 5;
            case 5: return 6;
            case 7: return 8;
            case 8: return 9;
            case 10: return 11;
            case 11: return 12;
            case 13: return 14;
            case 14: return 15;
            case 16: return 17;
            case 17: return 18;
            default: return -1; // 더 높은 레벨 카드 없음
        }
    }
}
