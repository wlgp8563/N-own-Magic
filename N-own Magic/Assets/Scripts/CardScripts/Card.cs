using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardCategory { Attack, Pierce, Shield, Heal, AddCard, AddTurn }

public class Card
{
    public string cardName;
    public Sprite cardImage;
    public string description;
    public int lightEnergy;
    public string cardID;
    public int level;
    public CardCategory category;

    // 카드 카테고리 enum
    //public enum CardCategory { Attack, Pierce, Shield, Heal, AddCard, AddTurn }

    // 카드 기본 생성자
    public Card(string name, Sprite image, int energy, string id, int lvl, CardCategory cat)
    {
        cardName = name;
        cardImage = LoadCardImage(cat, lvl);
        lightEnergy = energy;
        cardID = id;
        level = lvl;
        category = cat;
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
                description = $"{value}의 빛 공격을 가한다.";
                break;
            case CardCategory.Pierce:
                description = $"{value}의 쉴드를 무시한 관통 공격을 가한다.";
                break;
            case CardCategory.Shield:
                description = $"{value} 크기의 쉴드를 가진다.";
                break;
            case CardCategory.Heal:
                description = $"{value}만큼의 체력을 회복한다.";
                break;
            case CardCategory.AddCard:
                description = $"{value}만큼의 카드를 덱에서 랜덤으로 추가한다.";
                break;
            case CardCategory.AddTurn:
                description = $"{value}의 턴을 해당 턴에 추가한다.";
                break;
        }
    }

    // 카테고리와 레벨에 따른 효과 값 반환
    private int GetEffectValueByCategoryAndLevel()
    {
        switch (category)
        {
            case CardCategory.Attack:
                return level * 10; // 공격력은 레벨당 10씩 증가
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

    // 카드 레벨에 따른 이미지 갱신 (레벨별 다른 이미지 설정)
    /*private Sprite GetUpdatedImage(int newLevel)
    {
        // 각 레벨에 맞는 이미지를 Resources 폴더에서 불러옴
        return Resources.Load<Sprite>($"CardImages/{cardName.Split(' ')[0]}_Level{newLevel}");
    }*/

    // 카드 레벨에 따른 빛 에너지 갱신 (빛 에너지도 다르게 설정 가능)
    private int GetUpdatedLightEnergy(int newLevel)
    {
        switch (category)
        {
            case CardCategory.Attack:
                return lightEnergy + (newLevel * 5); // 예: 공격 카드는 레벨당 5 증가
            case CardCategory.Pierce:
                return lightEnergy + (newLevel * 6); // 예: 관통 공격은 레벨당 6 증가
            case CardCategory.Shield:
                return lightEnergy + (newLevel * 3); // 예: 쉴드는 레벨당 3 증가
            case CardCategory.Heal:
                return lightEnergy + (newLevel * 4); // 예: 힐은 레벨당 4 증가
            case CardCategory.AddCard:
                return lightEnergy + (newLevel * 2); // 예: 카드 추가는 레벨당 2 증가
            case CardCategory.AddTurn:
                return lightEnergy + (newLevel * 2); // 예: 턴 추가는 레벨당 2 증가
            default:
                return lightEnergy;
        }
    }
}
