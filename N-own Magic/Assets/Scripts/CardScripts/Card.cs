using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card
{
    public string cardName;
    public Sprite cardImage;
    public string description;
    public int lightEnergy;
    public string cardID;
    public int level;
    public CardCategory category;

    // ī�� ī�װ� enum
    public enum CardCategory { Attack, Pierce, Shield, Heal, AddCard, AddTurn }

    // ī�� �⺻ ������
    public Card(string name, Sprite image, int energy, string id, int lvl, CardCategory cat)
    {
        cardName = name;
        cardImage = image;
        lightEnergy = energy;
        cardID = id;
        level = lvl;
        category = cat;
        UpdateDescription(); // ���� �� ī�� ���� ������Ʈ
    }

    // ī�� ���� �� �޼ҵ�
    public void LevelUp()
    {
        if (level < 3)  // �ִ� ���� 5���� ����
        {
            level++;
            cardName = $"{cardName.Split(' ')[0]} Level{level}";
            cardImage = GetUpdatedImage(level);
            lightEnergy = GetUpdatedLightEnergy(level);
            UpdateDescription();  // ������ �� ���� ������Ʈ
        }
    }

    // ī�� ���� ������Ʈ �޼ҵ�
    public void UpdateDescription()
    {
        int value = GetEffectValueByCategoryAndLevel();
        switch (category)
        {
            case CardCategory.Attack:
                description = $"{value}�� �� ������ ���Ѵ�.";
                break;
            case CardCategory.Pierce:
                description = $"{value}�� ���带 ������ ���� ������ ���Ѵ�.";
                break;
            case CardCategory.Shield:
                description = $"{value}�� ���带 ������.";
                break;
            case CardCategory.Heal:
                description = $"{value}�� ü���� ȸ���Ѵ�.";
                break;
            case CardCategory.AddCard:
                description = $"{value}�� ī�带 �߰��Ѵ�.";
                break;
            case CardCategory.AddTurn:
                description = $"{value}�� ���� �߰��Ѵ�.";
                break;
        }
    }

    // ī�װ��� ������ ���� ȿ�� �� ��ȯ
    private int GetEffectValueByCategoryAndLevel()
    {
        switch (category)
        {
            case CardCategory.Attack:
                return level * 10; // ���ݷ��� ������ 10�� ����
            case CardCategory.Pierce:
                return level * 8;  // ���� ������ ������ 8�� ����
            case CardCategory.Shield:
                return level * 5;  // ����� ������ 5�� ����
            case CardCategory.Heal:
                return level * 7;  // ü�� ȸ���� ������ 7�� ����
            case CardCategory.AddCard:
                return level;      // �߰� ī��� ������ 1�徿 ����
            case CardCategory.AddTurn:
                return level;      // �߰� �ϵ� ������ 1�� ����
            default:
                return 0;
        }
    }

    // ī�� ������ ���� �̹��� ���� (������ �ٸ� �̹��� ����)
    private Sprite GetUpdatedImage(int newLevel)
    {
        // �� ������ �´� �̹����� Resources �������� �ҷ���
        return Resources.Load<Sprite>($"CardImages/{cardName.Split(' ')[0]}_Level{newLevel}");
    }

    // ī�� ������ ���� �� ������ ���� (�� �������� �ٸ��� ���� ����)
    private int GetUpdatedLightEnergy(int newLevel)
    {
        switch (category)
        {
            case CardCategory.Attack:
                return lightEnergy + (newLevel * 5); // ��: ���� ī��� ������ 5 ����
            case CardCategory.Pierce:
                return lightEnergy + (newLevel * 6); // ��: ���� ������ ������ 6 ����
            case CardCategory.Shield:
                return lightEnergy + (newLevel * 3); // ��: ����� ������ 3 ����
            case CardCategory.Heal:
                return lightEnergy + (newLevel * 4); // ��: ���� ������ 4 ����
            case CardCategory.AddCard:
                return lightEnergy + (newLevel * 2); // ��: ī�� �߰��� ������ 2 ����
            case CardCategory.AddTurn:
                return lightEnergy + (newLevel * 2); // ��: �� �߰��� ������ 2 ����
            default:
                return lightEnergy;
        }
    }
}
