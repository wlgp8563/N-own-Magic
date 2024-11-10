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
            // 카드 사용 후 덱에 반환하거나 필요한 로직 추가
            // 예: cardManager.ReturnCardsToDeck(new List<Card> { card });
            turnManager.OnCardUsed();
        }
        else
        {
            Debug.Log("에너지가 부족하여 카드를 사용할 수 없습니다.");
        }
    }

    /*void ApplyCardEffect(Card card)
    {
        switch (card.cardType)
        {
            case Card.CardType.AddCard:
                // 추가 카드 뽑기
                // cardManager.DrawCards(1);
                break;
            case Card.CardType.AddTurn:
                // 턴 추가 로직
                // turnManager.StartPlayerTurn(); // 또는 다른 방식으로 구현
                break;
            case Card.CardType.Attack:
                // 적에게 데미지 주기
                Enemy enemy = FindObjectOfType<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10); // 예시 데미지
                }
                break;
            case Card.CardType.Defense:
                // 방어막 추가
                // 예: player.AddShield(card.shield);
                break;
            case Card.CardType.Heal:
                Heal(10); // 예시 힐량
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
            // 게임 오버 로직 추가
        }
    }

    public void Heal(int amount)
    {
        currenthp += amount;
        //Debug.Log($"{playerName} healed {amount} health. Current Health: {health}");
    }
}
