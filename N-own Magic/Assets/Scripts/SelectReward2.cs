using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectReward2 : MonoBehaviour
{
    public Button[] btns;

    // Start is called before the first frame update
    void Start()
    {
        btns[0].onClick.AddListener(AddRandomLevel1Card);
        btns[1].onClick.AddListener(AddLightEnergy);
        btns[2].onClick.AddListener(AddFuse);
    }

    private void AddRandomLevel1Card()
    {
        List<int> level1CardIDs = new List<int> { 1, 4, 7, 10, 13, 16 };

        int randomIndex = Random.Range(0, level1CardIDs.Count);
        int randomCardID = level1CardIDs[randomIndex];

        Card randomCard = CardManager.CardManagerInstance.CreateCardByID(randomCardID);

        if (randomCard != null)
        {
            CardManager.CardManagerInstance.AddNewCardToDeck(randomCardID, 1);
            Debug.Log($"Level1 카드 '{randomCard.cardName}'이(가) playerDeck에 추가되었습니다.");
        }
        else
        {
            Debug.LogError("카드 생성에 실패했습니다.");
        }
        SceneManager.LoadScene("InGame");
    }

    private void AddLightEnergy()
    {
        Player.Instance.lightenergy++;
        SceneManager.LoadScene("InGame");
    }

    public void AddFuse()
    {
        Player.Instance.canFuseCard++;
        SceneManager.LoadScene("InGame");
    }
}
