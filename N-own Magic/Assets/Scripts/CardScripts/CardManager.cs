using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    private Player player;

    public Dictionary<int, Card> playerDeck = new Dictionary<int, Card>();
    public List<Card> handDeck = new List<Card>(); // 현재 화면에 보이는 덱
    public GameObject cardUIPrefab; // 카드 프리팹

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
    }

    private void InitializeDeck()
    {
        AddNewCardToDeck(1, 3);
        AddNewCardToDeck(4, 2);
        AddNewCardToDeck(7, 2);
        AddNewCardToDeck(10, 1);
        AddNewCardToDeck(13, 1);
        AddNewCardToDeck(16, 1);
    }

    public void AddNewCardToDeck(int cardID, int count)
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

    public Card CreateCardByID(int cardID)
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
}
