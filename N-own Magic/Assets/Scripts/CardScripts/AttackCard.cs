using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AttackCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int damage;
    public int cost;             //카드 자체에 표기x, 상점에서만 표기됨

    public AttackCard()
    {

    }

    public AttackCard(int Id, string CardName, string Description, int Damage, int Cost)
    {
        id = Id;
        cardName = CardName;
        description = Description;
        damage = Damage;
        cost = Cost;
    }
}
