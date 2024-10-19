using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType { Attack, PierceAttack, Shield, Heal, AddTurn, ChangeCard }

[System.Serializable]
public class Card
{
    public string cardName;
    public CardType cardType;
    public string description;
    public Sprite image;
    public int lightEnergyCost;
    public int cardID;
    public int level;
}
