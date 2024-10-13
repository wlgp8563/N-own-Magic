using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AddTurnCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int spendMana;         //마나=빛에너지는 카드에 표기(마나 추가 카드는 없음)
    public int addTurn;      //해당카드를 덱에서 제출하면 몇 장 재생성되는지(싸울 때 손에 가지고 있을 수 있는 최대 카드 수에 영향 받음)
    public int cost;             //카드 자체에 표기x, 상점에서만 표기됨

    public AddTurnCard()
    {

    }

    public AddTurnCard(int Id, string CardName, string Description, int SpendMana, int AddTurn, int Cost)
    {
        id = Id;
        cardName = CardName;
        description = Description;
        spendMana = SpendMana;
        addTurn = AddTurn;
        cost = Cost;
    }
}
