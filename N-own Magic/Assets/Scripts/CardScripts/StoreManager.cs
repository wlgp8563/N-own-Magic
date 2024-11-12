using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject storeUI;
    public GameObject fusionButton;
    public Button enterStoreButton;
    public Button exitStoreButton;
    public List<Card> selectedCards = new List<Card>();

    public Transform storeCardParent;
    public GameObject cardPrefab;

    void Start()
    {
        storeUI.SetActive(false);
        fusionButton.SetActive(false);
        enterStoreButton.onClick.AddListener(EnterStore);
        exitStoreButton.onClick.AddListener(ExitStore);
    }

    public void EnterStore()
    {
        storeUI.SetActive(true);
        DisplayDeckInStore();
    }

    public void ExitStore()
    {
        storeUI.SetActive(false);
        ClearSelectedCards();
    }

    void DisplayDeckInStore()
    {
        foreach (Transform child in storeCardParent)
        {
            Destroy(child.gameObject);
        }

        /*foreach (Card card in cardManager.playerDeck)
        {
            CreateStoreCardUI(card);
        }*/
    }

    void CreateStoreCardUI(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, storeCardParent);
        CardUI cardUI = newCard.GetComponent<CardUI>();
        //cardUI.SetCardUI(cardData);

        Button cardButton = newCard.GetComponent<Button>();
        cardButton.onClick.AddListener(() => SelectCard(cardData));
    }

    void SelectCard(Card card)
    {
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            // 선택 해제 UI 업데이트 (예: 테두리 제거)
        }
        else
        {
            selectedCards.Add(card);
            // 선택 UI 업데이트 (예: 테두리 추가)
        }

        if (selectedCards.Count == 3)
        {
            if (CanFuseSelectedCards())
            {
                fusionButton.SetActive(true);
            }
            else
            {
                fusionButton.SetActive(false);
            }
        }
        else
        {
            fusionButton.SetActive(false);
        }
    }

    bool CanFuseSelectedCards()
    {
        if (selectedCards.Count != 3)
            return false;

        Card firstCard = selectedCards[0];
        foreach (Card card in selectedCards)
        {
            if (card.category != firstCard.category || card.level != firstCard.level)
                return false;
        }
        return true;
    }

    /*public void FuseSelectedCards()
    {
        if (selectedCards.Count == 3 && CanFuseSelectedCards())
        {
            CardCategoryManager category = selectedCards[0].category;
            int newLevel = selectedCards[0].level + 1;

            if (newLevel > 5)
            {
                Debug.Log("최대 레벨입니다.");
                return;
            }

            Card newCard = ScriptableObject.CreateInstance<Card>();
            newCard.category = category;
            newCard.SetCardDataByLevel(newLevel);
            newCard.cardID = Random.Range(1000, 9999);

            cardManager.cardDeck.Add(newCard);
            foreach (Card card in selectedCards)
            {
                cardManager.cardDeck.Remove(card);
            }

            DisplayDeckInStore();
            ClearSelectedCards();

            Debug.Log($"카드가 레벨 {newLevel}으로 합성되었습니다!");
        }
    }*/

    void ClearSelectedCards()
    {
        selectedCards.Clear();
        fusionButton.SetActive(false);
        // 선택 해제 UI 업데이트 (예: 모든 테두리 제거)
    }
}
