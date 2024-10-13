using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ShieldCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int shield;
    public int cost;             //카드 자체에 표기x, 상점에서만 표기됨
    public ShieldCard()
    {

    }

    public ShieldCard(int Id, string CardName, string Description, int Shield, int Cost)
    {
        id = Id;
        cardName = CardName;
        description = Description;
        shield = Shield;
        cost = Cost;
    }
}
