using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<AttackCard> attackcardlist = new List<AttackCard>();

    void Awake()
    {
        attackcardlist.Add(new AttackCard(0, "����Ʈ��", "�⺻ �� �������� ������ 3�������� �ش�", 3, 5));
    }
}
