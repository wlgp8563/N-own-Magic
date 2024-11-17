using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Button[] shopBtns;
    public GameObject[] shops;
    public GameObject[] soldouts;

    public GameObject[] purchaseImage;
    public TMP_Text[] purchase;
    
    // Start is called before the first frame update
    void Start()
    {
        shopBtns[0].onClick.AddListener(LgihtLevel1Add);
        shopBtns[1].onClick.AddListener(HealLevel2Add);
        shopBtns[2].onClick.AddListener(AddCardLevel1Add);
        shopBtns[3].onClick.AddListener(AddTurnLevel1Add);
        shopBtns[4].onClick.AddListener(PierceLevel2Add);
        shopBtns[5].onClick.AddListener(LightLevel3Add);
        shopBtns[6].onClick.AddListener(ShieldLevel3Add);
        shopBtns[7].onClick.AddListener(PierceLevel3Add);
        shopBtns[8].onClick.AddListener(AddCardLevel2Add);
        shopBtns[9].onClick.AddListener(HealLevel3Add);
    }

    public void LgihtLevel1Add()
    {
        if(Player.Instance.haveMoney >= 6)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(1);
            CardManager.CardManagerInstance.AddNewCardToDeck(1, 1);
            Player.Instance.haveMoney -= 6;
            shops[0].SetActive(false);
            soldouts[0].SetActive(true);
            StartCoroutine(PurchaseText1(addCard));
        }
    }
    public void HealLevel2Add()
    {
        if (Player.Instance.haveMoney >= 15)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(11);
            CardManager.CardManagerInstance.AddNewCardToDeck(11, 1);
            Player.Instance.haveMoney -= 15;
            shops[1].SetActive(false);
            soldouts[1].SetActive(true);
            StartCoroutine(PurchaseText1(addCard));
        }
    }

    public void AddCardLevel1Add()
    {
        if (Player.Instance.haveMoney >= 10)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(13);
            CardManager.CardManagerInstance.AddNewCardToDeck(13, 1);
            Player.Instance.haveMoney -= 10;
            shops[2].SetActive(false);
            soldouts[2].SetActive(true);
            StartCoroutine(PurchaseText1(addCard));
        }
    }

    public void AddTurnLevel1Add()
    {
        if (Player.Instance.haveMoney >= 10)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(16);
            CardManager.CardManagerInstance.AddNewCardToDeck(16, 1);
            Player.Instance.haveMoney -= 10;
            shops[3].SetActive(false);
            soldouts[3].SetActive(true);
            StartCoroutine(PurchaseText1(addCard));
        }
    }
    public void PierceLevel2Add()
    {
        if (Player.Instance.haveMoney >= 17)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(5);
            CardManager.CardManagerInstance.AddNewCardToDeck(5, 1);
            Player.Instance.haveMoney -= 17;
            shops[4].SetActive(false);
            soldouts[4].SetActive(true);
            StartCoroutine(PurchaseText1(addCard));
        }
    }
    public void LightLevel3Add()
    {
        if (Player.Instance.haveMoney >= 20)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(3);
            CardManager.CardManagerInstance.AddNewCardToDeck(3, 1);
            Player.Instance.haveMoney -= 20;
            shops[5].SetActive(false);
            soldouts[5].SetActive(true);
            StartCoroutine(PurchaseText2(addCard));
        }
    }
    public void ShieldLevel3Add()
    {
        if (Player.Instance.haveMoney >= 25)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(9);
            CardManager.CardManagerInstance.AddNewCardToDeck(9, 1);
            Player.Instance.haveMoney -= 25;
            shops[6].SetActive(false);
            soldouts[6].SetActive(true);
            StartCoroutine(PurchaseText2(addCard));
        }
    }
    public void PierceLevel3Add()
    {
        if (Player.Instance.haveMoney >= 30)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(6);
            CardManager.CardManagerInstance.AddNewCardToDeck(6, 1);
            Player.Instance.haveMoney -= 30;
            shops[7].SetActive(false);
            soldouts[7].SetActive(true);
            StartCoroutine(PurchaseText2(addCard));
        }
    }
    public void AddCardLevel2Add()
    {
        if (Player.Instance.haveMoney >= 18)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(14);
            CardManager.CardManagerInstance.AddNewCardToDeck(14, 1);
            Player.Instance.haveMoney -= 18;
            shops[8].SetActive(false);
            soldouts[8].SetActive(true);
            StartCoroutine(PurchaseText2(addCard));
        }
    }
    public void HealLevel3Add()
    {
        if (Player.Instance.haveMoney >= 32)
        {
            //카드 추가 + 돈 줄어들기 + 버튼 변경
            Card addCard = CardManager.CardManagerInstance.CreateCardByID(12);
            CardManager.CardManagerInstance.AddNewCardToDeck(12, 1);
            Player.Instance.haveMoney -= 32;
            shops[9].SetActive(false);
            soldouts[9].SetActive(true);
            StartCoroutine(PurchaseText2(addCard));
        }
    }
    private IEnumerator PurchaseText1(Card addCard)
    {
        purchase[0].text = $"'{addCard.cardName}'이(가) 플레이어 덱에 추가되었습니다.";
        purchaseImage[0].SetActive(true);

        yield return new WaitForSeconds(1.0f);

        purchaseImage[0].SetActive(false);
    }

    private IEnumerator PurchaseText2(Card addCard)
    {
        purchase[1].text = $"'{addCard.cardName}'이(가) 플레이어 덱에 추가되었습니다.";
        purchaseImage[1].SetActive(true);

        yield return new WaitForSeconds(1.0f);

        purchaseImage[1].SetActive(false);
    }
}
