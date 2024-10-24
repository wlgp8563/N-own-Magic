using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Transform cardParent; // ī����� ��ġ�� �θ� UI
    public GameObject cardPrefab; // ī�� ������
    public Button deckButton; // ���� �����ִ� ��ư
    public Button submitButton; // ī�� ���� ��ư
    public List<Card> cardDeck = new List<Card>(); // ��ü ī�� ��
    public List<Card> handDeck = new List<Card>(); // ���� ȭ�鿡 ���̴� ��

    public GameObject deckUI; // �� UI
    public GameObject handDeckUI; // �ڵ� �� UI

    public int initialHandSize = 4; // ���� ���� �� ���� ī�� ��
    public float cardSpacing = 50f; // ī�� ����
    public float fanAngle = 10f; // ��ä�� ����

    void Start()
    {
        deckButton.onClick.AddListener(ShowCardDeckUI); // �� ��ư Ŭ�� �̺�Ʈ
        submitButton.onClick.AddListener(ReturnHandCardsToDeck); // ���� ��ư Ŭ�� �̺�Ʈ

        DrawInitialHand();
        ArrangeCardsInFanShape();
    }

    // �� UI ǥ��
    void ShowCardDeckUI()
    {
        deckUI.SetActive(true); // �� UI Ȱ��ȭ
        PopulateDeckUI();
    }

    // �� UI�� cardDeck ǥ��
    void PopulateDeckUI()
    {
        // �� UI�� ���� ī�� ����
        foreach (Transform child in deckUI.transform)
        {
            Destroy(child.gameObject);
        }

        // cardDeck�� ��� ī�带 �� UI�� ����
        foreach (Card card in cardDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, deckUI.transform);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card); // ī�� �����͸� UI�� ����
        }
    }

    // ī�� ������ ī�带 �̾� �ڵ� ���� �߰�
    void DrawInitialHand()
    {
        handDeck.Clear();
        for (int i = 0; i < initialHandSize; i++)
        {
            int randomIndex = Random.Range(0, cardDeck.Count); // �����ϰ� ī�� ����
            handDeck.Add(cardDeck[randomIndex]);
            cardDeck.RemoveAt(randomIndex); // �ߺ� ������ ���� ������ ����
        }

        // UI�� ī�� ����
        foreach (Card card in handDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, handDeckUI.transform);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card); // ī�� ������ ����
        }
    }

    // ī�带 ��ä�� ������� ��ġ
    void ArrangeCardsInFanShape()
    {
        int cardCount = handDeck.Count;
        float middleIndex = (cardCount - 1) / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            Transform cardTransform = handDeckUI.transform.GetChild(i);
            float angle = (i - middleIndex) * fanAngle;
            Vector3 position = new Vector3((i - middleIndex) * cardSpacing, 0f, 0f);

            // ī�� ȸ�� �� ��ġ
            cardTransform.localPosition = position;
            cardTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    // ī�带 �����ϰ� handDeck���� cardDeck���� ��ȯ
    void ReturnHandCardsToDeck()
    {
        foreach (Card card in handDeck)
        {
            cardDeck.Add(card); // handDeck���� cardDeck���� ī�� ��ȯ
        }

        handDeck.Clear();

        // ���� �ڵ� UI�� ī�� ����
        foreach (Transform child in handDeckUI.transform)
        {
            Destroy(child.gameObject);
        }

        DrawInitialHand(); // ���ο� �ڵ带 ����
        ArrangeCardsInFanShape(); // �ٽ� ��ä�÷� ��ġ
    }

    void EndGame()
    {
        // handDeck �ʱ�ȭ �� ��� ī�带 cardDeck���� ��ȯ
        foreach (Card card in handDeck)
        {
            cardDeck.Add(card);
        }
        handDeck.Clear();

        // �ڵ� UI �ʱ�ȭ
        foreach (Transform child in handDeckUI.transform)
        {
            Destroy(child.gameObject);
        }

        // ���� ���� �ʱ�ȭ �� �ٽ� ī�� �̱�
        DrawInitialHand();
        ArrangeCardsInFanShape();
    }
}
