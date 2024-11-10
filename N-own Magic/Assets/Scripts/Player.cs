using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int maxhp = 25;
    public int currenthp;
    public int playerlevel = 1;
    public int exp = 0;
    public int nexttoexp = 15;
    public int canFuseCard = 2;
    public int lightenergy = 2;
    public int playerturn = 3;
    public int handdecknum = 4;

    public LightEnergyManager energyManager;
    public TurnManager turnManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        currenthp = maxhp;
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if(exp >= nexttoexp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerlevel++;
        exp = 0;
        switch(playerlevel)
        {
            case 2:
                nexttoexp += 10;
                maxhp += 10;
                currenthp = maxhp;
                break;
            case 3:
                nexttoexp += 15;
                maxhp += 15;
                currenthp = maxhp;
                break;
            case 4:
                nexttoexp += 20;
                maxhp += 20;
                currenthp = maxhp;
                break;
            case 5:
                nexttoexp += 30;
                maxhp += 30;
                currenthp = maxhp;
                break;
            case 6:
                nexttoexp += 40;
                maxhp += 40;
                currenthp = maxhp;
                break;
        }
    }

    public bool DecreaseFusion()
    {
        if(canFuseCard > 0)
        {
            canFuseCard--;
            return true;
        }
        return false;
    }

    /*public void IncreaseFusion()
    {
        canFuseCard++;
    }*/

    public void UseCard(Card card)
    {
        if (energyManager.UseEnergy(card.lightEnergy))
        {
            //ApplyCardEffect(card);
            // ī�� ��� �� ���� ��ȯ�ϰų� �ʿ��� ���� �߰�
            // ��: cardManager.ReturnCardsToDeck(new List<Card> { card });
            turnManager.OnCardUsed();
        }
        else
        {
            Debug.Log("�������� �����Ͽ� ī�带 ����� �� �����ϴ�.");
        }
    }

    /*void ApplyCardEffect(Card card)
    {
        switch (card.cardType)
        {
            case Card.CardType.AddCard:
                // �߰� ī�� �̱�
                // cardManager.DrawCards(1);
                break;
            case Card.CardType.AddTurn:
                // �� �߰� ����
                // turnManager.StartPlayerTurn(); // �Ǵ� �ٸ� ������� ����
                break;
            case Card.CardType.Attack:
                // ������ ������ �ֱ�
                Enemy enemy = FindObjectOfType<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10); // ���� ������
                }
                break;
            case Card.CardType.Defense:
                // �� �߰�
                // ��: player.AddShield(card.shield);
                break;
            case Card.CardType.Heal:
                Heal(10); // ���� ����
                break;
        }
    }*/

    public void TakeDamage(int damage)
    {
        currenthp -= damage;
        //Debug.Log($"{playerName} took {damage} damage. Remaining Health: {health}");
        if (currenthp <= 0)
        {
            //Debug.Log($"{playerName} has been defeated!");
            // ���� ���� ���� �߰�
        }
    }

    public void Heal(int amount)
    {
        currenthp += amount;
        //Debug.Log($"{playerName} healed {amount} health. Current Health: {health}");
    }
}
