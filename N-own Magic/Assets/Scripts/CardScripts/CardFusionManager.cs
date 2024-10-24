using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFusionManager : MonoBehaviour
{
    public GameObject shopUI; // 상점 UI
    public Transform shopCardParent; // 상점에서 카드가 배치될 부모 UI

    private List<Card> selectedCards = new List<Card>(); // 합성할 카드 리스트

    // 상점 UI 활성화 및 cardDeck 표시
    public void EnterShop()
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
            GameObject newCard = Instantiate(FindObjectOfType<CardManager>().cardPrefab, shopCardParent);
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
    }
}
