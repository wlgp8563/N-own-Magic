using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PierceAttackCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int pierceDamage;
    public int cost;             //카드 자체에 표기x, 상점에서만 표기됨

    public PierceAttackCard()
    {

    }

    public PierceAttackCard(int Id, string CardName, string Description, int PierceDamage, int Cost)
    {
        id = Id;
        cardName = CardName;
        description = Description;
        pierceDamage = PierceDamage;
        cost = Cost;
    }
}
