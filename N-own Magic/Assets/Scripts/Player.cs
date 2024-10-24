using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName = "Player";
    public int health = 100;
    public int cardDrawCount = 4;
    public LightEnergyManager energyManager;
    public TurnManager turnManager;

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
        health -= damage;
        Debug.Log($"{playerName} took {damage} damage. Remaining Health: {health}");
        if (health <= 0)
        {
            Debug.Log($"{playerName} has been defeated!");
            // ���� ���� ���� �߰�
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"{playerName} healed {amount} health. Current Health: {health}");
    }
}
