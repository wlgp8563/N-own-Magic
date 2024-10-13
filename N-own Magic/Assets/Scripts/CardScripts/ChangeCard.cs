using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ChangeCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int spendMana;         //����=���������� ī�忡 ǥ��(���� �߰� ī��� ����)
    public int rechargeCard;      //�ش�ī�带 ������ �����ϸ� �� �� ������Ǵ���(�ο� �� �տ� ������ ���� �� �ִ� �ִ� ī�� ���� ���� ����)
    public int cost;             //ī�� ��ü�� ǥ��x, ���������� ǥ���

    public ChangeCard()
    {

    }

    public ChangeCard(int Id, string CardName, string Description, int SpendMana, int RechargeCard, int Cost)
    {
        id = Id;
        cardName = CardName;
        description = Description;
        spendMana = SpendMana;
        rechargeCard = RechargeCard;
        cost = Cost;
    }
}
