using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<AttackCard> attackcardlist = new List<AttackCard>();

    void Awake()
    {
        attackcardlist.Add(new AttackCard(0, "라이트닝", "기본 빛 공격으로 적에게 3데미지를 준다", 3, 5));
    }
}
