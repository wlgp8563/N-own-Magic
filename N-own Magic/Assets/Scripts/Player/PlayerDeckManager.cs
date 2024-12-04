using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    //public static PlayerDeckManager playerDeckManager;

    [Header("UI Elements")]
    public GameObject playerDeckUI; // ī�� �ռ� shop UI
    public GameObject cardUIPrefab; // ī�� UI ������
    public Transform cardContentParent; // ī�� ����Ʈ �θ� (��ũ�� ������ ����)

    //private List<Card> selectedCards = new List<Card>(); // �ռ��� ī���
    private Dictionary<int, Card> playerDeck => CardManager.CardManagerInstance.playerDeck; // playerDeck ����

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

    // ī�� ����Ʈ UI ǥ��
    public void DisplayCardsInFusionShop()
    {
        // ���� UI ī�� ����
        foreach (Transform child in cardContentParent)
        {
            Destroy(child.gameObject);
        }

        // playerDeck�� �ִ� ��� ī�� ǥ��
        foreach (var entry in playerDeck)
        {
            int cardID = entry.Key;
            Card card = entry.Value;

            for (int i = 0; i < card.count; i++)
            {
                Debug.Log("�÷��̾ �����ֱ�");
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
        // Canvas�� �ִ� ���
        Canvas canvas = cardUI.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = sortingLayer;
        }

        // SpriteRenderer�� �ִ� ���
        SpriteRenderer spriteRenderer = cardUI.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortingLayer;
        }
    }
}
