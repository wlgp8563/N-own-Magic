using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    //public static PlayerDeckManager playerDeckManager;

    [Header("UI Elements")]
    public GameObject playerDeckUI; // 카드 합성 shop UI
    public GameObject cardUIPrefab; // 카드 UI 프리팹
    public Transform cardContentParent; // 카드 리스트 부모 (스크롤 가능한 영역)

    //private List<Card> selectedCards = new List<Card>(); // 합성할 카드들
    private Dictionary<int, Card> playerDeck => CardManager.CardManagerInstance.playerDeck; // playerDeck 참조

    /*private void Awake()
    {
        if (playerDeckManager == null)
        {
            playerDeckManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    // 카드 리스트 UI 표시
    public void DisplayCardsInFusionShop()
    {
        // 기존 UI 카드 삭제
        foreach (Transform child in cardContentParent)
        {
            Destroy(child.gameObject);
        }

        // playerDeck에 있는 모든 카드 표시
        foreach (var entry in playerDeck)
        {
            int cardID = entry.Key;
            Card card = entry.Value;

            for (int i = 0; i < card.count; i++)
            {
                Debug.Log("플레이어덱 보여주기");
                GameObject cardUI = Instantiate(cardUIPrefab, cardContentParent);

                SetSortingLayer(cardUI, 5);
                CardUI cardUIComponent = cardUI.GetComponent<CardUI>();
                cardUIComponent.GetComponent<CardUI>().SetCardData(card);
                //Button cardButton = cardUI.GetComponent<Button>();

                //cardButton.onClick.AddListener(() => SelectCardForFusion(card));
            }
        }
    }

    private void SetSortingLayer(GameObject cardUI, int sortingLayer)
    {
        // Canvas가 있는 경우
        Canvas canvas = cardUI.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = sortingLayer;
        }

        // SpriteRenderer가 있는 경우
        SpriteRenderer spriteRenderer = cardUI.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingLayer;
        }
    }
}
