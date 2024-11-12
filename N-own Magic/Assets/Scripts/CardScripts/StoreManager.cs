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
            // ���� ���� UI ������Ʈ (��: �׵θ� ����)
        }
        else
        {
            selectedCards.Add(card);
            // ���� UI ������Ʈ (��: �׵θ� �߰�)
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
                Debug.Log("�ִ� �����Դϴ�.");
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

            Debug.Log($"ī�尡 ���� {newLevel}���� �ռ��Ǿ����ϴ�!");
        }
    }*/

    void ClearSelectedCards()
    {
        selectedCards.Clear();
        fusionButton.SetActive(false);
        // ���� ���� UI ������Ʈ (��: ��� �׵θ� ����)
    }
}
