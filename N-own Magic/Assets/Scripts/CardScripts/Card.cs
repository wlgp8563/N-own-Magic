using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardCategory
{
    Attack,                     //기본빛공격(쉴드가 있을 시 쉴드 공격)
    PierceAttack,               //관통빛공격(쉴드가 있어도 쉴드를 부시는게 아니라 hp를 직접 깎음)
    Shield,
    Heal,
    ChangeCard,                 //이 카드를 제출함으로써 카드 1장 랜덤 추가(한 번에 가질 수 있는 최대 카드 수에 영향 받음)
    AddTurn                     //턴 추가 카드
}

[CreateAssetMenu(fileName = "New Card", menuName = "TCG/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;           // 카드 설명 추가
    public Sprite cardImage;             // 카드 이미지 추가
    public CardCategory category;        //카드 종류가 총 6개로 카테고리로 나눠서 관리
    public int level = 1;                //카드 레벨 존재 처음 기본적으로 주어지는 카드는 레벨1 최대 5까지 존재
    public string cardID;                // 카드 고유 ID 추가(->카드가 여러종류니 종류별로 고유 ID 만들어서 관리하기 위한 용도)
    public string cardGage;              // 카드 빛에너지(0~5)

    public int damage;
    public int pierceDamage;
    public int defend;
    public int healing;
    public int addCard;
    public int addTurn;
}
