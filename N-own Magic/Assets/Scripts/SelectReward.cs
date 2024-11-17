using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectReward : MonoBehaviour
{
    public Button[] btns;

    private void Start()
    {
        btns[0].onClick.AddListener(AddRandomLevel1Card);
        btns[1].onClick.AddListener(AddHp);
        btns[2].onClick.AddListener(AddCoin);
        //btns[0].onClick.AddListener(AddCoin);
        //btns[0].onClick.AddListener(AddCoin);
    }

    public void AddCoin()
    {
        Player.Instance.haveMoney += 5;
        SceneManager.LoadScene("InGame");
    }

    public void AddRandomLevel1Card()
    {
        List<int> level1CardIDs = new List<int> { 1, 4, 7, 10, 13, 16 };

        int randomIndex = Random.Range(0, level1CardIDs.Count);
        int randomCardID = level1CardIDs[randomIndex];

        Card randomCard = CardManager.CardManagerInstance.CreateCardByID(randomCardID);

        if (randomCard != null)
        {
            CardManager.CardManagerInstance.AddNewCardToDeck(randomCardID, 1);
            Debug.Log($"Level1 ī�� '{randomCard.cardName}'��(��) playerDeck�� �߰��Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("ī�� ������ �����߽��ϴ�.");
        }
        SceneManager.LoadScene("InGame");
    }

    public void AddHp()
    {
        Player.Instance.maxhp += 10;
        SceneManager.LoadScene("InGame");
    }
}
