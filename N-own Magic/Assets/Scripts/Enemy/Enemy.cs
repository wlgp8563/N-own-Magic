using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int enemyHp;
    public int attack;
    public int pierce;
    public int healSelf;
    public int shieldSelf;
    public int turnCount;
    public int enemyShield;
    public int giveExp;
    public int giveMoney;

    //public int rebornHp;
    public int uitiAttack;

}
