using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardCategory { Attack, Pierce, Shield, Heal, AddCard, AddTurn }

public class Card
{
    public string cardName { get; set; }
    public Sprite cardImage { get; set; }
    public string description { get; set; }
    public int lightEnergy { get; set; }
    public int cardID { get; set; }
    public int level { get; set; }
    public CardCategory category { get; set; }
    public int count { get; set; }

    // ī�� �⺻ ������
    public Card(string name, Sprite image, int energy, int id, int lvl, CardCategory cat, int count = 1)
    {
        this.cardName = name;
        this.cardImage = LoadCardImage(cat, lvl);
        this.lightEnergy = energy;
        this.cardID = id;
        this.level = lvl;
        this.category = cat;
        this.count = count;
        UpdateDescription(); // ���� �� ī�� ���� ������Ʈ
    }

    private Sprite LoadCardImage(CardCategory category, int level)
    {
        return Resources.Load<Sprite>($"Cards/{category}_Level{level}");
    }

    // ī�� ���� �� �޼ҵ�
    public void LevelUp()
    {
        if (level < 3)  // �ִ� ���� 3���� ����
        {
            level++;
            cardName = $"{cardName.Split(' ')[0]} Level{level}";
            cardImage = LoadCardImage(category,level);
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
                description = $"{value}�� �� ������\n ���Ѵ�.";
                break;
            case CardCategory.Pierce:
                description = $"{value}�� ���带 ������\n ���� ������ ���Ѵ�.";
                break;
            case CardCategory.Shield:
                description = $"{value} ũ���� ���带\n ������.";
                break;
            case CardCategory.Heal:
                description = $"{value}��ŭ�� ü����\n ȸ���Ѵ�.";
                break;
            case CardCategory.AddCard:
                description = $"{value}��ŭ�� ī�带\n ������ ��������\n �߰��Ѵ�.";
                break;
            case CardCategory.AddTurn:
                description = $"{value}�� ����\n �ش� �Ͽ� �߰��Ѵ�.";
                break;
        }
    }

    // ī�װ��� ������ ���� ȿ�� �� ��ȯ
    private int GetEffectValueByCategoryAndLevel()
    {
        switch (category)
        {
            case CardCategory.Attack:
                return level * 8; // ���ݷ��� ������ 10�� ����
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

    // ī�� ������ ���� �� ������ ���� (�� �������� �ٸ��� ���� ����)
    private int GetUpdatedLightEnergy(int newLevel)
    {
        switch (category)
        {
            case CardCategory.Attack:
                return lightEnergy; 
            case CardCategory.Pierce:
                return lightEnergy; 
            case CardCategory.Shield:
                return lightEnergy; 
            case CardCategory.Heal:
                return lightEnergy + (newLevel); 
            case CardCategory.AddCard:
                return lightEnergy + (newLevel); 
            case CardCategory.AddTurn:
                return lightEnergy + (newLevel + 1);
            default:
                return lightEnergy;
        }
    }
}
