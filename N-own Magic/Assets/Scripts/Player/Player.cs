using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int haveMoney = 0;

    public int currentTurn;
    public int currentShield;
    public int currentLightEnergy;

    //private LightEnergyManager energyManager;
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
        currentTurn = playerturn;
        currentShield = 0;
        currentLightEnergy = lightenergy;
    }

    public void AddExp(int amount)
    {
        if(amount >= nexttoexp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        playerlevel++;
        exp = 0;
        switch(playerlevel)
        {
            case 2:
                nexttoexp += 10;
                maxhp += 12;
                currenthp = maxhp;
                EnemyControl.enemyControlInstance.LevelUp2Reward();
                break;
            case 3:
                nexttoexp += 15;
                maxhp += 18;
                currenthp = maxhp;
                break;
            case 4:
                nexttoexp += 20;
                maxhp += 26;
                currenthp = maxhp;
                break;
            case 5:
                nexttoexp += 30;
                maxhp += 36;
                currenthp = maxhp;
                break;
            case 6:
                nexttoexp += 40;
                maxhp += 48;
                currenthp = maxhp;
                break;
        }
    }

    public void LevelUp2Reward()
    {
        haveMoney += 7;
        handdecknum += 1;
        /*if(currentSceneName == InGame)
        {
            level.text = $"Level.1 -> Level.2";
            reward.text = $"+ 7 coin" +
                $"뽑을 수 있는 카드 1 증가";
            StartCoroutine(SetLevelUP());
        }
        /*level.text = $"Level.1 -> Level.2";
        reward.text = $"+ 7 coin" +
            $"뽑을 수 있는 카드 1 증가";
        StartCoroutine(SetLevelUP());*/
    }

    public void LevelUP6Reward()
    {
        haveMoney += 32;
        lightenergy += 1;
        //level.text = $"Level.5 -> Level.6";
        //reward.text = $"+ 32 coin" +
            //$"플레이어 빛 에너지 1 증가";
    }

    /*private IEnumerator SetLevelUP()
    {
        levelUpM.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        levelUpM.SetActive(false);
    }*/

    public bool DecreaseFusion()
    {
        if(canFuseCard > 0)
        {
            canFuseCard--;
            return true;
        }
        return false;
    }

    public void IncreaseFusion()
    {
        canFuseCard++;
    }

    public void PlayerTakeDamage(int damage)
    {
        currenthp -= damage;
        //Debug.Log($"{playerName} took {damage} damage. Remaining Health: {health}");
        if (currenthp <= 0)
        {
            //Debug.Log($"{playerName} has been defeated!");
            // 게임 오버 로직 추가
        }
    }

    public void PlayerShieldAttack(int amount)
    {
        currentShield -= amount;
        if(currentShield <= 0)
        {
            currentShield = 0;
        }
        /*if(currentShield <= 0)
        {
            playerUI2.playerShields.SetActive(false);
        }*/
    }

    public void Heal(int amount)
    {
        if(currenthp == maxhp)
        {
            currenthp = maxhp;
        }
        else
        {
            currenthp += amount;
            if(currenthp >= maxhp)
            {
                currenthp = maxhp;
            }
        }

        //Debug.Log($"{playerName} healed {amount} health. Current Health: {health}");
    }
    

    public void AddShield(int amount)
    {
        //playerUI2.playerShields.SetActive(true);
        currentShield += amount;
    }

    public void DecreaseTurn()
    {
        if(currentTurn > 0)
        {
            currentTurn--;
        }

        if(currentTurn == 0)
        {
            TurnManager.TurnManagerInstance.EndPlayerTurn();
            //turnManager.EndPlayerTurn();
            ResetTurn();
        }
    }

    public void IncreaseTurn(int amount)
    {
        currentTurn += amount;
    }

    public void ResetTurn()
    {
        currentTurn = playerturn;
        //playerturn = defalutplayerturn;
    }

    public void DecreaseLightEnergy(int amount)
    {
        currentLightEnergy -= amount;
    }
}
