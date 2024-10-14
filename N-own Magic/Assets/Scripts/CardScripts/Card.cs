using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardCategory
{
    Attack,                     //�⺻������(���尡 ���� �� ���� ����)
    PierceAttack,               //���������(���尡 �־ ���带 �νô°� �ƴ϶� hp�� ���� ����)
    Shield,
    Heal,
    ChangeCard,                 //�� ī�带 ���������ν� ī�� 1�� ���� �߰�(�� ���� ���� �� �ִ� �ִ� ī�� ���� ���� ����)
    AddTurn                     //�� �߰� ī��
}

[CreateAssetMenu(fileName = "New Card", menuName = "TCG/Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;           // ī�� ���� �߰�
    public Sprite cardImage;             // ī�� �̹��� �߰�
    public CardCategory category;        //ī�� ������ �� 6���� ī�װ��� ������ ����
    public int level = 1;                //ī�� ���� ���� ó�� �⺻������ �־����� ī��� ����1 �ִ� 5���� ����
    public string cardID;                // ī�� ���� ID �߰�(->ī�尡 ���������� �������� ���� ID ���� �����ϱ� ���� �뵵)
    public string cardGage;              // ī�� ��������(0~5)

    public int damage;
    public int pierceDamage;
    public int defend;
    public int healing;
    public int addCard;
    public int addTurn;
}
