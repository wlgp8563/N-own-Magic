using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class AddTurnCard : MonoBehaviour
{
    public int id;
    public string cardName;
    public string description;
    public int spendMana;         //����=���������� ī�忡 ǥ��(���� �߰� ī��� ����)
    public int addTurn;      //�ش�ī�带 ������ �����ϸ� �� �� ������Ǵ���(�ο� �� �տ� ������ ���� �� �ִ� �ִ� ī�� ���� ���� ����)
    public int cost;             //ī�� ��ü�� ǥ��x, ���������� ǥ���

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
