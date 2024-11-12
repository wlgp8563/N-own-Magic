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

    // 카드 기본 생성자
    public Card(string name, Sprite image, int energy, int id, int lvl, CardCategory cat, int count = 1)
    {
        this.cardName = name;
        this.cardImage = LoadCardImage(cat, lvl);
        this.lightEnergy = energy;
        this.cardID = id;
        this.level = lvl;
        this.category = cat;
        this.count = count;
        UpdateDescription(); // 생성 시 카드 설명 업데이트
    }

    private Sprite LoadCardImage(CardCategory category, int level)
    {
        return Resources.Load<Sprite>($"Cards/{category}_Level{level}");
    }

    // 카드 레벨 업 메소드
    public void LevelUp()
    {
        if (level < 3)  // 최대 레벨 3까지 가능
        {
            level++;
            cardName = $"{cardName.Split(' ')[0]} Level{level}";
            cardImage = LoadCardImage(category,level);
            lightEnergy = GetUpdatedLightEnergy(level);
            UpdateDescription();  // 레벨업 시 설명 업데이트
        }
    }

    // 카드 설명 업데이트 메소드
    public void UpdateDescription()
    {
        int value = GetEffectValueByCategoryAndLevel();
        switch (category)
        {
            case CardCategory.Attack:
                description = $"{value}의 빛 공격을\n 가한다.";
                break;
            case CardCategory.Pierce:
                description = $"{value}의 쉴드를 무시한\n 관통 공격을 가한다.";
                break;
            case CardCategory.Shield:
                description = $"{value} 크기의 쉴드를\n 가진다.";
                break;
            case CardCategory.Heal:
                description = $"{value}만큼의 체력을\n 회복한다.";
                break;
            case CardCategory.AddCard:
                description = $"{value}만큼의 카드를\n 덱에서 랜덤으로\n 추가한다.";
                break;
            case CardCategory.AddTurn:
                description = $"{value}의 턴을\n 해당 턴에 추가한다.";
                break;
        }
    }

    // 카테고리와 레벨에 따른 효과 값 반환
    private int GetEffectValueByCategoryAndLevel()
    {
        switch (category)
        {
            case CardCategory.Attack:
                return level * 8; // 공격력은 레벨당 10씩 증가
            case CardCategory.Pierce:
                return level * 8;  // 관통 공격은 레벨당 8씩 증가
            case CardCategory.Shield:
                return level * 5;  // 쉴드는 레벨당 5씩 증가
            case CardCategory.Heal:
                return level * 7;  // 체력 회복은 레벨당 7씩 증가
            case CardCategory.AddCard:
                return level;      // 추가 카드는 레벨당 1장씩 증가
            case CardCategory.AddTurn:
                return level;      // 추가 턴도 레벨당 1씩 증가
            default:
                return 0;
        }
    }

    // 카드 레벨에 따른 빛 에너지 갱신 (빛 에너지도 다르게 설정 가능)
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
