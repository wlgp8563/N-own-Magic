using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCategoryManager : MonoBehaviour
{
    public List<Card> attackCards = new List<Card>();
    public List<Card> pierceCards = new List<Card>();
    public List<Card> shieldCards = new List<Card>();
    public List<Card> healCards = new List<Card>();
    public List<Card> changeCards = new List<Card>();
    public List<Card> addCards = new List<Card>();
    public Character character; // 캐릭터 정보 연결

    private Dictionary<CardCategory, List<Card>> cardCategories;

    private void Start()
    {
        cardCategories = new Dictionary<CardCategory, List<Card>>()
        {
            { CardCategory.Attack, attackCards },
            { CardCategory.PierceAttack, pierceCards },
            { CardCategory.Shield,  shieldCards},
            { CardCategory.Heal, healCards },
            { CardCategory.ChangeCard, changeCards },
            { CardCategory.AddTurn, addCards }
            // 나머지 카테고리들도 동일하게 추가
        };

        AddBasicCards();
    }

    private void AddBasicCards()
    {
        // 기본 카드 2장씩 카테고리에 추가
        foreach (var category in cardCategories.Keys)
        {
            for (int i = 0; i < 2; i++)
            {
                Card newCard = ScriptableObject.CreateInstance<Card>();
                newCard.category = category;
                newCard.cardName = $"{category.ToString()} Basic Card";
                newCard.cardID = System.Guid.NewGuid().ToString(); // 유니크한 카드 ID 설정
                cardCategories[category].Add(newCard);
            }
        }
    }

    // 합성 시도 함수
    public void TryToFuseCard(CardCategory category)
    {
        // 캐릭터의 합성 가능 갯수를 확인
        if (!character.CanFuse())
        {
            Debug.Log("합성할 수 없습니다. 합성 가능 갯수가 부족합니다.");
            return;
        }

        List<Card> cardsInCategory = cardCategories[category];
        int basicCardCount = cardsInCategory.FindAll(card => card.level == 1).Count;

        if (basicCardCount >= 3)
        {
            FuseCards(category);
        }
    }

    // 카드 합성 함수
    private void FuseCards(CardCategory category)
    {
        List<Card> cardsInCategory = cardCategories[category];

        // 기본 카드 3장 제거
        for (int i = 0; i < 3; i++)
        {
            Card cardToRemove = cardsInCategory.Find(card => card.level == 1);
            cardsInCategory.Remove(cardToRemove);
        }

        // 상위 레벨 카드 추가
        Card newCard = ScriptableObject.CreateInstance<Card>();
        newCard.category = category;
        newCard.cardName = $"{category.ToString()} Upgraded Card";
        newCard.cardID = System.Guid.NewGuid().ToString(); // 유니크한 카드 ID 설정
        newCard.level = 2;

        cardsInCategory.Add(newCard);

        // 캐릭터의 합성 가능 갯수를 1 차감
        character.DecreaseFusionCount();

        Debug.Log($"{category.ToString()} 카드가 상위 카드로 합성되었습니다! 남은 합성 가능 갯수: {character.fusionCount}");
    }
}
