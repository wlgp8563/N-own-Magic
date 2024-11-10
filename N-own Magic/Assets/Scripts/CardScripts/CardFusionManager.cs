using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFusionManager : MonoBehaviour
{
    //public GameObject shopUI; // ���� UI
    //public Transform shopCardParent; // �������� ī�尡 ��ġ�� �θ� UI

    public Player player;
    public CardManager cardManager;

    public Transform fuseDeckUIParent;
    public GameObject cardUIPrefab;
    public Button fuseButton;

    private List<Card> selectedCards = new List<Card>(); // �ռ��� ī�� ����Ʈ

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

        // �÷��̾� ���� ��� ī�带 UI�� ǥ��
        foreach (var card in cardManager.playerDeck.Values)
        {
            /*for (int i = 0; i < card.Count; i++)
            {
                GameObject cardUI = Instantiate(cardUIPrefab, fuseDeckUIParent);
                CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
                cardUIComponent.SetCardData(card[i]);

                // ī�� Ŭ�� �� ����/����
                Button cardButton = cardUI.GetComponent<Button>();
                int index = i; // �ε��� ĸ��
                cardButton.onClick.AddListener(() => SelectCard(card[i]));
            }*/
        }
    }

    private void FuseCards()
    {
        if (selectedCards.Count == 3)
        {
            int cardId = selectedCards[0].cardID;
            // ī�� 3���� id�� ������ ��� �����ؾ� �ռ� ����
            if (selectedCards.TrueForAll(c => c.cardID == cardId) && selectedCards.TrueForAll(c => c.level == selectedCards[0].level))
            {
                if (player.canFuseCard > 0)
                {
                    // �ռ� ���� �� �ռ� �õ�
                    Card fusedCard = cardManager.UpgradeCard(selectedCards[0]);

                    // ���õ� ī�� ���� �� ������ ����
                    foreach (Card card in selectedCards)
                    {
                        cardManager.RemoveCardFromDeck(card);
                    }

                    // ���׷��̵�� ī�� �߰�
                    cardManager.AddCardToDeck(fusedCard);

                    // �ռ� ���� Ƚ�� ����
                    player.canFuseCard--;

                    // �ռ� �Ϸ� �� ���� ī�� �ʱ�ȭ
                    selectedCards.Clear();

                    // UI ����
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

    // ���� UI Ȱ��ȭ �� cardDeck ǥ��
    /*public void EnterShop()
    {
        shopUI.SetActive(true);
        PopulateShopUI();
    }

    // ���� UI�� cardDeck ǥ��
    void PopulateShopUI()
    {
        foreach (Transform child in shopCardParent)
        {
            Destroy(child.gameObject); // ���� ���� ī�� ����
        }

        foreach (Card card in FindObjectOfType<CardManager>().cardDeck)
        {
            GameObject newCard = Instantiate(FindObjectOfType<CardManager>().cardUIPrefab, shopCardParent);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card);
            newCard.GetComponent<Button>().onClick.AddListener(() => SelectCardForFusion(card));
        }
    }

    // �ռ��� ī�� ����
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

    // 3���� ������ ���� ī�尡 ���õǸ� �ռ� �õ�
    void TryFusion()
    {
        if (selectedCards.Count == 3 && selectedCards[0].level == selectedCards[1].level && selectedCards[1].level == selectedCards[2].level)
        {
            Card baseCard = selectedCards[0];
            baseCard.LevelUp(); // ī�� ������
            selectedCards.Clear();
        }
    }*/
}
