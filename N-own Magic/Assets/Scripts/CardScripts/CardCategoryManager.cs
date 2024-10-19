using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCategoryManager : MonoBehaviour
{
    /*public List<Card> attackCards = new List<Card>();
    public List<Card> pierceCards = new List<Card>();
    public List<Card> shieldCards = new List<Card>();
    public List<Card> healCards = new List<Card>();
    public List<Card> changeCards = new List<Card>();
    public List<Card> addCards = new List<Card>();

    public List<Card> cardDeck = new List<Card>(); // 모든 카드를 관리하는 카드덱

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
                newCard.cardName = GenerateCardName(category, 1); // 카드 이름 생성
                newCard.cardName = $"{category.ToString()} Basic Card";
                newCard.cardID = System.Guid.NewGuid().ToString(); // 유니크한 카드 ID 설정
                AssignCategoryStats(newCard, 1); // 초기 스탯 설정
                cardCategories[category].Add(newCard);
                cardDeck.Add(newCard); // 카드덱에 추가
            }
        }
    }

    private void AssignCategoryStats(Card card, int level)
    {
        switch (card.category)
        {
            case CardCategory.Attack:
                card.damage = level * 3; // 레벨별로 데미지 증가
                break;
            case CardCategory.PierceAttack:
                card.pierceDamage = level * 2; // 레벨별로 관통데미지 증가
                break;
            case CardCategory.Heal:
                card.healing = level * 3; // 레벨별로 힐량 증가
                break;
            case CardCategory.Shield:
                card.defend = level * 2; // 레벨별로 쉴드량 증가
                break;
            case CardCategory.ChangeCard:
                card.addCard = level + 1; // 카드 교환 카드 장 수 업그레이드
                break;
            case CardCategory.AddTurn:
                card.addTurn = level + 1; // 턴 증가 카드 턴 수 업그레이드
                break;
        }
    }

    private string GenerateCardName(CardCategory category, int level)
    {
        string baseName;
        switch (category)
        {
            case CardCategory.Attack:
                baseName = "Lighting";
                break;
            case CardCategory.PierceAttack:
                baseName = "Piercing";
                break;
            case CardCategory.Shield:
                baseName = "Light Shield";
                break;
            case CardCategory.Heal:
                baseName = "Healing";
                break;
            case CardCategory.ChangeCard:
                baseName = "Add Card";
                break;
            case CardCategory.AddTurn:
                baseName = "Add Turn";
                break;
            default:
                baseName = "Unknown Card";
                break;
        }

        return $"{baseName} Lv.{level}";
    }

    // 합성 시도 함수
    public void TryToFuseCard(CardCategory category)
    {
        // 캐릭터의 합성 가능 갯수를 확인
        if (!character.CanFuse())
        {
            //공지 그런 텍스트로 표현
            Debug.Log("합성할 수 없습니다. 합성 가능 갯수가 부족합니다.");
            return;
        }

        List<Card> cardsInCategory = cardCategories[category];
        int currentLevel = cardsInCategory[0].level; // 카테고리 내 첫 번째 카드의 레벨을 확인

        // 현재 레벨에 따른 카드 개수 확인
        int cardCount = cardsInCategory.FindAll(card => card.level == currentLevel).Count;

        if (cardCount >= 3)
        {
            // 합성 가능: 다음 레벨로 업그레이드
            if (currentLevel < 5)
            {
                FuseCards(category, currentLevel);
            }
            else if(currentLevel == 5)
            {
                Debug.Log("이미 최고 레벨 카드입니다.");
            }
        }
    }

    // 카드 합성 함수
    private void FuseCards(CardCategory category, int currentLevel)
    {
        List<Card> cardsInCategory = cardCategories[category];

        // 현재 레벨의 카드 3장을 제거
        for (int i = 0; i < 3; i++)
        {
            Card cardToRemove = cardsInCategory.Find(card => card.level == currentLevel);
            cardsInCategory.Remove(cardToRemove);
        }

        // 상위 레벨 카드 생성
        Card newCard = ScriptableObject.CreateInstance<Card>();
        newCard.category = category;
        newCard.cardName = GenerateCardName(category, currentLevel + 1); // 새로운 레벨의 카드 이름 생성
        newCard.cardID = System.Guid.NewGuid().ToString(); // 유니크한 카드 ID 설정
        newCard.level = currentLevel + 1;
        AssignCategoryStats(newCard, newCard.level); // 새로운 스탯 설정

        cardsInCategory.Add(newCard);
        character.DecreaseFusionCount(); // 합성 후 캐릭터의 합성 가능 횟수 차감

        Debug.Log($"{category.ToString()} 카드가 Lv.{currentLevel + 1}로 합성되었습니다! 남은 합성 가능 갯수: {character.fusionCount}");
    }*/
}
