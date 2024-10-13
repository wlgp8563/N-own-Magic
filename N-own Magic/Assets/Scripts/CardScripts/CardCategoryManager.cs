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
            // ������ ī�װ��鵵 �����ϰ� �߰�
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
                newCard.cardName = $"{category.ToString()} Basic Card";
                newCard.cardID = System.Guid.NewGuid().ToString(); // ����ũ�� ī�� ID ����
                cardCategories[category].Add(newCard);
            }
        }
    }

    // �ռ� �õ� �Լ�
    public void TryToFuseCard(CardCategory category)
    {
        // ĳ������ �ռ� ���� ������ Ȯ��
        if (!character.CanFuse())
        {
            Debug.Log("�ռ��� �� �����ϴ�. �ռ� ���� ������ �����մϴ�.");
            return;
        }

        List<Card> cardsInCategory = cardCategories[category];
        int basicCardCount = cardsInCategory.FindAll(card => card.level == 1).Count;

        if (basicCardCount >= 3)
        {
            FuseCards(category);
        }
    }

    // ī�� �ռ� �Լ�
    private void FuseCards(CardCategory category)
    {
        List<Card> cardsInCategory = cardCategories[category];

        // �⺻ ī�� 3�� ����
        for (int i = 0; i < 3; i++)
        {
            Card cardToRemove = cardsInCategory.Find(card => card.level == 1);
            cardsInCategory.Remove(cardToRemove);
        }

        // ���� ���� ī�� �߰�
        Card newCard = ScriptableObject.CreateInstance<Card>();
        newCard.category = category;
        newCard.cardName = $"{category.ToString()} Upgraded Card";
        newCard.cardID = System.Guid.NewGuid().ToString(); // ����ũ�� ī�� ID ����
        newCard.level = 2;

        cardsInCategory.Add(newCard);

        // ĳ������ �ռ� ���� ������ 1 ����
        character.DecreaseFusionCount();

        Debug.Log($"{category.ToString()} ī�尡 ���� ī��� �ռ��Ǿ����ϴ�! ���� �ռ� ���� ����: {character.fusionCount}");
    }
}
