using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFusionManager : MonoBehaviour
{
    //public GameObject shopUI; // 상점 UI
    //public Transform shopCardParent; // 상점에서 카드가 배치될 부모 UI

    public Player player;
    public CardManager cardManager;

    public Transform fuseDeckUIParent;
    public GameObject cardUIPrefab;
    public Button fuseButton;

    private List<Card> selectedCards = new List<Card>(); // 합성할 카드 리스트

    void Start()
    {
        fuseButton.onClick.AddListener(FuseCards);
        DisplayFuseShopUI();
    }

    public void DisplayFuseShopUI()
    {
        foreach (Transform child in fuseDeckUIParent)
        {
            Destroy(child.gameObject);
        }

        // 플레이어 덱의 모든 카드를 UI로 표시
        foreach (var card in cardManager.playerDeck.Values)
        {
            /*for (int i = 0; i < card.Count; i++)
            {
                GameObject cardUI = Instantiate(cardUIPrefab, fuseDeckUIParent);
                CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
                cardUIComponent.SetCardData(card[i]);

                // 카드 클릭 시 선택/해제
                Button cardButton = cardUI.GetComponent<Button>();
                int index = i; // 인덱스 캡쳐
                cardButton.onClick.AddListener(() => SelectCard(card[i]));
            }*/
        }
    }

    private void FuseCards()
    {
        if (selectedCards.Count == 3)
        {
            int cardId = selectedCards[0].cardID;
            // 카드 3장의 id와 레벨이 모두 동일해야 합성 가능
            if (selectedCards.TrueForAll(c => c.cardID == cardId) && selectedCards.TrueForAll(c => c.level == selectedCards[0].level))
            {
                if (player.canFuseCard > 0)
                {
                    // 합성 가능 시 합성 시도
                    Card fusedCard = cardManager.UpgradeCard(selectedCards[0]);

                    // 선택된 카드 삭제 및 덱에서 제거
                    foreach (Card card in selectedCards)
                    {
                        cardManager.RemoveCardFromDeck(card);
                    }

                    // 업그레이드된 카드 추가
                    cardManager.AddCardToDeck(fusedCard);

                    // 합성 가능 횟수 감소
                    player.canFuseCard--;

                    // 합성 완료 후 선택 카드 초기화
                    selectedCards.Clear();

                    // UI 갱신
                    DisplayFuseShopUI();
                }
            }
        }
    }

    private void SelectCard(Card card)
    {
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
        }
        else
        {
            if (selectedCards.Count < 3)
            {
                selectedCards.Add(card);
            }
        }
    }

    // 상점 UI 활성화 및 cardDeck 표시
    /*public void EnterShop()
    {
        shopUI.SetActive(true);
        PopulateShopUI();
    }

    // 상점 UI에 cardDeck 표시
    void PopulateShopUI()
    {
        foreach (Transform child in shopCardParent)
        {
            Destroy(child.gameObject); // 기존 상점 카드 제거
        }

        foreach (Card card in FindObjectOfType<CardManager>().cardDeck)
        {
            GameObject newCard = Instantiate(FindObjectOfType<CardManager>().cardUIPrefab, shopCardParent);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card);
            newCard.GetComponent<Button>().onClick.AddListener(() => SelectCardForFusion(card));
        }
    }

    // 합성할 카드 선택
    void SelectCardForFusion(Card card)
    {
        if (selectedCards.Count < 3)
        {
            selectedCards.Add(card);
            if (selectedCards.Count == 3)
            {
                TryFusion();
            }
        }
    }

    // 3장의 동일한 레벨 카드가 선택되면 합성 시도
    void TryFusion()
    {
        if (selectedCards.Count == 3 && selectedCards[0].level == selectedCards[1].level && selectedCards[1].level == selectedCards[2].level)
        {
            Card baseCard = selectedCards[0];
            baseCard.LevelUp(); // 카드 레벨업
            selectedCards.Clear();
        }
    }*/
}
