using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public GameObject fuseShop;
    public Transform handCardArea; // ī����� ��ġ�� UI��ġ
    public GameObject cardPrefab; // ī�� ������s
    public Button deckButton; // ���� �����ִ� ��ư(����â ���� ����)
    public GameObject deckUI;  //�� UI â
    public Button submitButton; // ī�� ���� ��ư
    public List<Card> cardDeck = new List<Card>(); // ��ü ī�� ��
    public List<Card> handDeck = new List<Card>(); // ���� ȭ�鿡 ���̴� ��
    public List<Card> selectedCards = new List<Card>(); //ī�� �ռ� �� ������ ī��� ����Ʈ ��
    public Button fusionButton;

    public GameObject handDeckUI; // �ڵ� �� UI

    public int initialHandSize = 4; // ���� ���� �� ���� ī�� ��
    public float cardSpacing = 50f; // ī�� ����
    public float fanAngle = 10f; // ��ä�� ����

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeDeck();

        /*if (SceneManager.GetActiveScene().name == "InGame")
        { 
            deckButton.onClick.AddListener(ShowCardDeckUI); // �� ��ư Ŭ��

            fusionButton.onClick.AddListener(TryFuseCards);
        }
        else if(SceneManager.GetActiveScene().name == "CardGame")
        {
            submitButton.onClick.AddListener(ReturnHandCardsToDeck); // ���� ��ư Ŭ�� �̺�Ʈ

            DrawInitialHand();
            ArrangeCardsInFanShape();
        }*/
        
    }

    private void InitializeDeck()
    {
        cardDeck.Add(new Card("Lighting Level1", null, 0, "attack01", 1, CardCategory.Attack));
        cardDeck.Add(new Card("Lighting Level1", null, 0, "attack02", 1, CardCategory.Attack));
        cardDeck.Add(new Card("Pierce Light Level1", null, 0, "pierce01", 1, CardCategory.Pierce));
        cardDeck.Add(new Card("Heal Level1", null, 0, "heal01", 1, CardCategory.Heal));
        cardDeck.Add(new Card("Heal Level1", null, 0, "heal02", 1, CardCategory.Heal));
        cardDeck.Add(new Card("Shield Level1", null, 1, "shield01", 1, CardCategory.Shield));
        cardDeck.Add(new Card("Add Card Level1", null, 1, "addcard01", 1, CardCategory.AddCard));
        cardDeck.Add(new Card("Add Turn Level1", null, 1, "addturn01", 1, CardCategory.AddTurn));
    }

    public void OpenFusionShop()
    {
        fusionUI.gameObject.SetActive(true);
        DisplayDeckInFusionUI();
    }

    private void DisplayDeckInFusionUI()
    {
        foreach (Transform child in fusionInventoryArea)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in playerDeck)
        {
            GameObject cardObj = Instantiate(cardPrefab, fusionInventoryArea);
            cardObj.GetComponent<CardDisplay>().Setup(card);
        }
    }

    public void FuseSelectedCards(List<Card> selectedCards)
    {
        if (selectedCards.Count == 3 &&
            selectedCards[0].category == selectedCards[1].category &&
            selectedCards[0].category == selectedCards[2].category &&
            selectedCards[0].level == selectedCards[1].level &&
            selectedCards[0].level == selectedCards[2].level &&
            PlayerState.Instance.UseFusionAttempt())
        {
            var fusedCard = new Card($"{selectedCards[0].category} Level{selectedCards[0].level + 1}",
                                      null, 0, "newID", selectedCards[0].level + 1, selectedCards[0].category);
            playerDeck.RemoveAll(card => selectedCards.Contains(card));
            playerDeck.Add(fusedCard);
        }
    }

    public void DrawInitialHand()
    {
        handDeck.Clear();
        for (int i = 0; i < Player.Instance.maxHandSize; i++)
        {
            int randomIndex = Random.Range(0, playerDeck.Count);
            handDeck.Add(playerDeck[randomIndex]);
        }
        DisplayHand();
    }

    private void DisplayHand()
    {
        foreach (Transform child in handArea)
        {
            Destroy(child.gameObject);
        }

        float cardSpacing = 0.3f;
        float handWidth = (handDeck.Count - 1) * cardSpacing;

        for (int i = 0; i < handDeck.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, handArea);
            cardObj.GetComponent<CardDisplay>().Setup(handDeck[i]);
            cardObj.transform.localPosition = new Vector3(i * cardSpacing - handWidth / 2, 0, 0);
            cardObj.transform.localRotation = Quaternion.Euler(0, 0, -10 + 5 * i);
        }
    }






    /*public void DisplayDectInFusionShop()
    {
        foreach(Transform child in fuseShop.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(var card in cardDeck)
        {
            GameObject cardUI = Instantiate(cardPrefab, fuseShop.transform);
            CardUI cardUIScript = cardUI.GetComponent<CardUI>();
            cardUIScript.SetCardData(card);

            Button cardButton = cardUI.GetComponent<Button>();
            cardButton.onClick.AddListener(() => OnCardSelected(card, cardUI));
        }
    }

    private void OnCardSelected(Card card, GameObject cardUI)
    {
        if (selectedCards.Contains(card))
        {
            selectedCards.Remove(card);
            cardUI.GetComponent<Image>().color = Color.gray;
        }
        else if(selectedCards.Count < 3)
        {
            selectedCards.Add(card);
            cardUI.GetComponent<Image>().color = Color.white;
        }
    }

    private void TryFuseCards()
    {
        if(Player.Instance.canFuseCard <= 0)
        {
            Debug.Log("�ռ� ���� Ƚ���� �����մϴ�"); //����� �ؽ�Ʈ ���� �ߴ� �ɷ� ����
            return;
        }
        
        if(selectedCards.Count == 3 && CanFuseCards(selectedCards))
        {
            TryFuseCards(selectedCards);
            Player.Instance.DecreaseFusion();
        }
        else
        {
            Debug.Log("�ռ� ���� ������ �ƴ�");
        }
    }

    private bool CanFuseCards(List<Card> selectCards)
    {
        return selectedCards[0].level == selectedCards[1].level &&
               selectedCards[1].level == selectedCards[2].level &&
               selectedCards[0].category == selectedCards[1].category &&
               selectedCards[1].category == selectedCards[2].category;
    }

    private void FuseCards(List<Card> cardsToFuse)
    {
        int newLevel = cardsToFuse[0].level + 1;
        CardCategory category = cardsToFuse[0].category;

        Card newCard = new Card(category, newLevel);
        cardDeck.Add(newCard);

        foreach (var card in cardsToFuse)
        {
            cardDeck.Remove(card);
        }

        selectedCards.Clear();
        DisplayDectInFusionShop();
        //ī�� �ռ� ���� �޽��� �߰�
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
    }*/
}
