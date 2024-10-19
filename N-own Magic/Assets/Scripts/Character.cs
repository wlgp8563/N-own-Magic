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
            description = "적에게 4의 피해를 입힌다.",
            image = attackCardSprite,  // Assign your Sprite in the Inspector or dynamically
            lightEnergyCost = 0,
            cardID = 1,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Piercing Light",
            cardType = CardType.PierceAttack,
            description = "적의 쉴드를 무시한 3의 관통데미지를 입힌다.",
            image = pierceAttackCardSprite,
            lightEnergyCost = 0,
            cardID = 2,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Holy Shield",
            cardType = CardType.Shield,
            description = "3 크기의 쉴드를 얻는다.",
            image = shieldCardSprite,
            lightEnergyCost = 0,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Heal",
            cardType = CardType.Shield,
            description = "4만큼의 Hp를 회복한다.",
            image = shieldCardSprite,
            lightEnergyCost = 0,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Add Card",
            cardType = CardType.Shield,
            description = "카드 1장을 추가 획득한다.",
            image = shieldCardSprite,
            lightEnergyCost = 1,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Add Turn",
            cardType = CardType.Shield,
            description = "1회의 턴을 추가한다.",
            image = shieldCardSprite,
            lightEnergyCost = 2,
            cardID = 3,
            level = 1
        });

        deck.Add(new Card
        {
            cardName = "Holy Shield",
            cardType = CardType.Shield,
            description = "3 크기의 쉴드를 얻는다.",
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
    public TMP_Text playerTurn;    //플레이어 턴 수
    public TMP_Text playerHand;    //플레이어 손에 있을 수 있는 카드 수
    public int fusionCount;   // 초기 합성 가능 갯수
    public TMP_Text lightGage;     //초기 빛 에너지 수




    // 합성 가능 갯수를 1 차감하는 함수
    public void DecreaseFusionCount()
    {
        if (fusionCount > 0)
        {
            fusionCount--;
        }
    }

    // 합성 가능 여부 확인 함수
    public bool CanFuse()
    {
        return fusionCount > 0;
    }*/
}
