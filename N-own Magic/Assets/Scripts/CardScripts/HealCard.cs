using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class HealCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int heal;
    public int cost;             //카드 자체에 표기x, 상점에서만 표기됨

    public HealCard()
    {

    }

    public HealCard(int Id, string CardName, string Description, int Heal, int Cost)
    {
        id = Id;
        cardName = CardName;
        description = Description;
        heal = Heal;
        cost = Cost;
    }
}
