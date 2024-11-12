using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFusionManager : MonoBehaviour
{
    public static CardFusionManager Instance;

    [Header("UI Elements")]
    public GameObject fusionShopUI; // ī�� �ռ� shop UI
    public GameObject cardUIPrefab; // ī�� UI ������
    public Transform cardContentParent; // ī�� ����Ʈ �θ� (��ũ�� ������ ����)
    public Button fusionButton; // �ռ��ϱ� ��ư

    private List<Card> selectedCards = new List<Card>(); // �ռ��� ī���
    private Dictionary<int, Card> playerDeck => CardManager.Instance.playerDeck; // playerDeck ����

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

    // ī�� ����Ʈ UI ǥ��
    private void DisplayCardsInFusionShop()
    {
        // ���� UI ī�� ����
        foreach (Transform child in cardContentParent)
        {
            Destroy(child.gameObject);
        }

        // playerDeck�� �ִ� ��� ī�� ǥ��
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
        // Canvas�� �ִ� ���
        Canvas canvas = cardUI.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = sortingLayer;
        }

        // SpriteRenderer�� �ִ� ���
        SpriteRenderer spriteRenderer = cardUI.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingLayer;
        }
    }

    // �ռ��� ī�� ����
    private void SelectCardForFusion(Card card)
    {
        // ���� ID�� ���� ī�� 3�常 ���� ����
        if (selectedCards.Count < 3)
        {
            selectedCards.Add(card);
            Debug.Log($"{card.cardName} ���õ�. ���� ���õ� ī�� ��: {selectedCards.Count}");

            if (selectedCards.Count == 3)
            {
                // ���õ� ī�� 3���� ��� ���� ID���� Ȯ��
                if (selectedCards[0].cardID == selectedCards[1].cardID && selectedCards[1].cardID == selectedCards[2].cardID)
                {
                    fusionButton.interactable = true;
                }
                else
                {
                    Debug.Log("ī�� ID�� �ٸ��ϴ�. �ٽ� �����ϼ���.");
                    selectedCards.Clear();
                    fusionButton.interactable = false;
                }
            }
        }
    }

    // ī�� �ռ� ���
    private void FuseSelectedCards()
    {
        if (selectedCards.Count != 3)
        {
            Debug.Log("ī�� 3���� �����ؾ� �մϴ�.");
            return;
        }

        int selectedCardID = selectedCards[0].cardID;
        int nextLevelCardID = GetNextLevelCardID(selectedCardID);
        Player.Instance.DecreaseFusion();

        if (nextLevelCardID == -1)
        {
            Debug.Log("�� ���� ������ ī�尡 �������� �ʽ��ϴ�.");
            selectedCards.Clear();
            fusionButton.interactable = false;
            return;
        }

        // playerDeck���� ���õ� ī�� ����
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

        // ���ο� �ռ��� ī�� �߰�
        Card newCard = CardManager.Instance.CreateCardByID(nextLevelCardID);
        CardManager.Instance.AddNewCardToDeck(nextLevelCardID, 1);

        Debug.Log($"ī�� �ռ� �Ϸ�: {newCard.cardName} ȹ��");

        // UI ����
        selectedCards.Clear();
        fusionButton.interactable = false;
        DisplayCardsInFusionShop();
    }

    // ���� ���� ī�� ID ��������
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
            default: return -1; // �� ���� ���� ī�� ����
        }
    }
}
