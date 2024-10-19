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

    public List<Card> cardDeck = new List<Card>(); // ��� ī�带 �����ϴ� ī�嵦

    public Character character; // ĳ���� ���� ����

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
        // �⺻ ī�� 2�徿 ī�װ��� �߰�
        foreach (var category in cardCategories.Keys)
        {
            for (int i = 0; i < 2; i++)
            {
                Card newCard = ScriptableObject.CreateInstance<Card>();
                newCard.category = category;
                newCard.cardName = GenerateCardName(category, 1); // ī�� �̸� ����
                newCard.cardName = $"{category.ToString()} Basic Card";
                newCard.cardID = System.Guid.NewGuid().ToString(); // ����ũ�� ī�� ID ����
                AssignCategoryStats(newCard, 1); // �ʱ� ���� ����
                cardCategories[category].Add(newCard);
                cardDeck.Add(newCard); // ī�嵦�� �߰�
            }
        }
    }

    private void AssignCategoryStats(Card card, int level)
    {
        switch (card.category)
        {
            case CardCategory.Attack:
                card.damage = level * 3; // �������� ������ ����
                break;
            case CardCategory.PierceAttack:
                card.pierceDamage = level * 2; // �������� ���뵥���� ����
                break;
            case CardCategory.Heal:
                card.healing = level * 3; // �������� ���� ����
                break;
            case CardCategory.Shield:
                card.defend = level * 2; // �������� ���差 ����
                break;
            case CardCategory.ChangeCard:
                card.addCard = level + 1; // ī�� ��ȯ ī�� �� �� ���׷��̵�
                break;
            case CardCategory.AddTurn:
                card.addTurn = level + 1; // �� ���� ī�� �� �� ���׷��̵�
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

    // �ռ� �õ� �Լ�
    public void TryToFuseCard(CardCategory category)
    {
        // ĳ������ �ռ� ���� ������ Ȯ��
        if (!character.CanFuse())
        {
            //���� �׷� �ؽ�Ʈ�� ǥ��
            Debug.Log("�ռ��� �� �����ϴ�. �ռ� ���� ������ �����մϴ�.");
            return;
        }

        List<Card> cardsInCategory = cardCategories[category];
        int currentLevel = cardsInCategory[0].level; // ī�װ� �� ù ��° ī���� ������ Ȯ��

        // ���� ������ ���� ī�� ���� Ȯ��
        int cardCount = cardsInCategory.FindAll(card => card.level == currentLevel).Count;

        if (cardCount >= 3)
        {
            // �ռ� ����: ���� ������ ���׷��̵�
            if (currentLevel < 5)
            {
                FuseCards(category, currentLevel);
            }
            else if(currentLevel == 5)
            {
                Debug.Log("�̹� �ְ� ���� ī���Դϴ�.");
            }
        }
    }

    // ī�� �ռ� �Լ�
    private void FuseCards(CardCategory category, int currentLevel)
    {
        List<Card> cardsInCategory = cardCategories[category];

        // ���� ������ ī�� 3���� ����
        for (int i = 0; i < 3; i++)
        {
            Card cardToRemove = cardsInCategory.Find(card => card.level == currentLevel);
            cardsInCategory.Remove(cardToRemove);
        }

        // ���� ���� ī�� ����
        Card newCard = ScriptableObject.CreateInstance<Card>();
        newCard.category = category;
        newCard.cardName = GenerateCardName(category, currentLevel + 1); // ���ο� ������ ī�� �̸� ����
        newCard.cardID = System.Guid.NewGuid().ToString(); // ����ũ�� ī�� ID ����
        newCard.level = currentLevel + 1;
        AssignCategoryStats(newCard, newCard.level); // ���ο� ���� ����

        cardsInCategory.Add(newCard);
        character.DecreaseFusionCount(); // �ռ� �� ĳ������ �ռ� ���� Ƚ�� ����

        Debug.Log($"{category.ToString()} ī�尡 Lv.{currentLevel + 1}�� �ռ��Ǿ����ϴ�! ���� �ռ� ���� ����: {character.fusionCount}");
    }*/
}
