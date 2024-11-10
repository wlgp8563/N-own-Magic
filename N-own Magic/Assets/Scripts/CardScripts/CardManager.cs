using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    private Player player;

    public Dictionary<int, Card> playerDeck = new Dictionary<int, Card>();
    public List<Card> handDeck = new List<Card>(); // ���� ȭ�鿡 ���̴� ��
    public List<Card> selectedCardsForFusion = new List<Card>(); //ī�� �ռ� �� ������ ī��� ����Ʈ ��

    //public GameObject fuseShop;
    //public Transform fuseDeck;
    public Button fuseButton;
    public Transform handDeckUIParent; // ī����� ��ġ�� UI��ġ
    public GameObject cardUIPrefab; // ī�� ������
    public Transform fusionShopUIParent;
    public Button deckButton; // ���� �����ִ� ��ư(����â ���� ����)
    //public GameObject deckUI;  //�� UI â
    //public Button submitButton; // ī�� ���� ��ư
    public List<Card> cardDeck = new List<Card>(); // ��ü ī�� ��
    
    

    //public GameObject handDeckUI; // �ڵ� �� UI

    //public int initialHandSize = 4; // ���� ���� �� ���� ī�� ��
    //public float cardSpacing = 50f; // ī�� ����
    //public float fanAngle = 10f; // ��ä�� ����

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeDeck();
        //DrawHandDeck();
        DisplayHandDeckUI();
        //SetupFusionShopUI();
        //fuseButton.onClick.AddListener(FuseCards);

        /*if (SceneManager.GetActiveScene().name == "InGame")
        { 
            deckButton.onClick.AddListener(ShowCardDeckUI); // �� ��ư Ŭ��

            fusionButton.onClick.AddListener(TryFuseCards);
        }
        else if(SceneManager.GetActiveScene().name == "CardGame")
        {
            submitButton.onClick.AddListener(ReturnHandCardsToDeck); // ���� ��ư Ŭ�� �̺�Ʈ

            DrawInitialHand();
            ArrangeCardsInFanShape();
        }*/
        
    }

    private void InitializeDeck()
    {
        AddNewCardToDeck(1, 2);
        AddNewCardToDeck(4, 2);
        AddNewCardToDeck(10, 2);
        AddNewCardToDeck(7, 1);
        AddNewCardToDeck(13, 1);
        AddNewCardToDeck(16, 1);
    }

    private void AddNewCardToDeck(int cardID, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card newCard = CreateCardByID(cardID);
            if (playerDeck.ContainsKey(cardID))
            {
                playerDeck[cardID].count += 1;
            }
            else
            {
                playerDeck[cardID] = newCard;
            }
        }
    }

    private Card CreateCardByID(int cardID)
    {
        switch (cardID)
        {
            case 1: return new Card("Lighting Level1", null, 0, cardID, 1, CardCategory.Attack);
            case 2: return new Card("Lighting Level2", null, 0, cardID, 2, CardCategory.Attack);
            case 3: return new Card("Lighting Level3", null, 0, cardID, 3, CardCategory.Attack);
            case 4: return new Card("Pierce Light Level1", null, 0, cardID, 1, CardCategory.Pierce);
            case 5: return new Card("Pierce Light Level2", null, 0, cardID, 2, CardCategory.Pierce);
            case 6: return new Card("Pierce Light Level3", null, 0, cardID, 3, CardCategory.Pierce);
            case 7: return new Card("Shield Level1", null, 0, cardID, 1, CardCategory.Shield);
            case 8: return new Card("Shield Level2", null, 0, cardID, 2, CardCategory.Shield);
            case 9: return new Card("Shield Level3", null, 0, cardID, 3, CardCategory.Shield);
            case 10: return new Card("Heal Level1", null, 0, cardID, 1, CardCategory.Heal);
            case 11: return new Card("Heal Level2", null, 0, cardID, 2, CardCategory.Heal);
            case 12: return new Card("Heal Level3", null, 1, cardID, 3, CardCategory.Heal);
            case 13: return new Card("Add Card Level1", null, 1, cardID, 1, CardCategory.AddCard);
            case 14: return new Card("Add Card Level2", null, 2, cardID, 2, CardCategory.AddCard);
            case 15: return new Card("Add Card Level3", null, 3, cardID, 3, CardCategory.AddCard);
            case 16: return new Card("Add Turn Level1", null, 1, cardID, 1, CardCategory.AddTurn);
            case 17: return new Card("Add Turn Level2", null, 2, cardID, 2, CardCategory.AddTurn);
            case 18: return new Card("Add Turn Level3", null, 3, cardID, 3, CardCategory.AddTurn);
            default: return null;
        }
    }

    private void DisplayHandDeckUI()
    {
        // ���� UI ����
        foreach (Transform child in handDeckUIParent)
        {
            Destroy(child.gameObject);
        }

        // ��ä�� ��ġ ���� ���� ����
        float handDeckRadius = 300f; // ��ä���� ������
        float startAngle = -30f; // ���� ����
        float endAngle = 30f; // ���� ����
        int cardCount = handDeck.Count;
        float angleStep = (cardCount > 1) ? (endAngle - startAngle) / (cardCount - 1) : 0;

        for (int i = 0; i < cardCount; i++)
        {
            // ī�� ����
            Card card = handDeck[i];
            GameObject cardUI = Instantiate(cardUIPrefab, handDeckUIParent);
            CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
            cardUIComponent.SetCardData(card);

            // RectTransform�� �̿��� ��ġ �� ȸ�� ����
            RectTransform cardRect = cardUI.GetComponent<RectTransform>();

            // ���� ī���� ��ġ ����
            float angle = startAngle + (angleStep * i);

            // ��ġ ��� (��ä�� ��ġ)
            float posX = handDeckRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            float posY = -handDeckRadius * Mathf.Cos(angle * Mathf.Deg2Rad); // ��ä�� �Ʒ��� ��ġ

            // ī�� ��ġ �� ȸ�� ����
            cardRect.anchoredPosition = new Vector2(posX, posY);
            cardRect.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public Card UpgradeCard(Card card)
    {
        int nextLevelId = card.cardID + 1;
        if (playerDeck.ContainsKey(nextLevelId))
        {
            return new Card(
                playerDeck[nextLevelId].cardName,
                playerDeck[nextLevelId].cardImage,
                playerDeck[nextLevelId].lightEnergy,
                playerDeck[nextLevelId].cardID,
                playerDeck[nextLevelId].level,
                playerDeck[nextLevelId].category);
        }
        return null;
    }

    public void AddCardToDeck(Card card)
    {
        if (!playerDeck.ContainsKey(card.cardID))
        {
            //playerDeck[card.cardID] = new List<Card>();
        }
        //playerDeck[card.cardID].Add(card);
    }

    public void RemoveCardFromDeck(Card card)
    {
        if (playerDeck.ContainsKey(card.cardID))
        {
            //playerDeck[card.cardID].Remove(card);
            if (playerDeck[card.cardID].count == 0)
            {
                playerDeck.Remove(card.cardID);
            }
        }
    }
}
