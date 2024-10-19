using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  // Player's deck
    public List<Card> hand = new List<Card>();  // Cards in player's hand
    public int maxHandSize = 7;                 // Maximum cards a player can hold

    public Sprite attackCardSprite;
    public Sprite pierceAttackCardSprite;
    public Sprite shieldCardSprite;



    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DrawCards(4);  // Draw 3 cards at the start of the game
    }

    void InitializeDeck()
    {
        deck.Add(new Card
        {
            cardName = "Lighting",
            cardType = CardType.Attack,
            description = "������ 4�� ���ظ� ������.",
            image = attackCardSprite,  // Assign your Sprite in the Inspector or dynamically
            lightEnergyCost = 0,
            cardID = 1,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Piercing Light",
            cardType = CardType.PierceAttack,
            description = "���� ���带 ������ 3�� ���뵥������ ������.",
            image = pierceAttackCardSprite,
            lightEnergyCost = 0,
            cardID = 2,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Holy Shield",
            cardType = CardType.Shield,
            description = "3 ũ���� ���带 ��´�.",
            image = shieldCardSprite,
            lightEnergyCost = 0,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Heal",
            cardType = CardType.Shield,
            description = "4��ŭ�� Hp�� ȸ���Ѵ�.",
            image = shieldCardSprite,
            lightEnergyCost = 0,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Add Card",
            cardType = CardType.Shield,
            description = "ī�� 1���� �߰� ȹ���Ѵ�.",
            image = shieldCardSprite,
            lightEnergyCost = 1,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Add Turn",
            cardType = CardType.Shield,
            description = "1ȸ�� ���� �߰��Ѵ�.",
            image = shieldCardSprite,
            lightEnergyCost = 2,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Holy Shield",
            cardType = CardType.Shield,
            description = "3 ũ���� ���带 ��´�.",
            image = shieldCardSprite,
            lightEnergyCost = 1,
            cardID = 3,
            level = 1
        });
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void DrawCards(int draw)
    {

    }






    /*[SerializeField]
    public Slider hpSlider;
    public TMP_Text hpNum;
    public Slider levelSlider;
    public TMP_Text levelNum;
    public TMP_Text playerTurn;    //�÷��̾� �� ��
    public TMP_Text playerHand;    //�÷��̾� �տ� ���� �� �ִ� ī�� ��
    public int fusionCount;   // �ʱ� �ռ� ���� ����
    public TMP_Text lightGage;     //�ʱ� �� ������ ��




    // �ռ� ���� ������ 1 �����ϴ� �Լ�
    public void DecreaseFusionCount()
    {
        if (fusionCount > 0)
        {
            fusionCount--;
        }
    }

    // �ռ� ���� ���� Ȯ�� �Լ�
    public bool CanFuse()
    {
        return fusionCount > 0;
    }*/
}
