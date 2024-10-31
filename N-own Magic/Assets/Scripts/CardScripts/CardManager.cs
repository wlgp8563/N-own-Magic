using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    public GameObject fuseShop;
    public Transform handCardArea; // 카드들이 배치될 UI위치
    public GameObject cardPrefab; // 카드 프리팹s
    public Button deckButton; // 덱을 보여주는 버튼(선택창 씬에 있음)
    public GameObject deckUI;  //덱 UI 창
    public Button submitButton; // 카드 제출 버튼
    public List<Card> cardDeck = new List<Card>(); // 전체 카드 덱
    public List<Card> handDeck = new List<Card>(); // 현재 화면에 보이는 덱
    public List<Card> selectedCards = new List<Card>(); //카드 합성 시 선택한 카드들 리스트 덱
    public Button fusionButton;

    public GameObject handDeckUI; // 핸드 덱 UI

    public int initialHandSize = 4; // 게임 시작 시 뽑을 카드 수
    public float cardSpacing = 50f; // 카드 간격
    public float fanAngle = 10f; // 부채꼴 각도

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
            deckButton.onClick.AddListener(ShowCardDeckUI); // 덱 버튼 클릭

            fusionButton.onClick.AddListener(TryFuseCards);
        }
        else if(SceneManager.GetActiveScene().name == "CardGame")
        {
            submitButton.onClick.AddListener(ReturnHandCardsToDeck); // 제출 버튼 클릭 이벤트

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
            Debug.Log("합성 가능 횟수가 부족합니다"); //여기는 텍스트 상자 뜨는 걸로 수정
            return;
        }
        
        if(selectedCards.Count == 3 && CanFuseCards(selectedCards))
        {
            TryFuseCards(selectedCards);
            Player.Instance.DecreaseFusion();
        }
        else
        {
            Debug.Log("합성 가능 조건이 아님");
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
        //카드 합성 성공 메시지 추가
    }

    // 덱 UI 표시
    void ShowCardDeckUI()
    {
        deckUI.SetActive(true); // 덱 UI 활성화
        PopulateDeckUI();
    }

    // 덱 UI에 cardDeck 표시
    void PopulateDeckUI()
    {
        // 덱 UI에 기존 카드 제거
        foreach (Transform child in deckUI.transform)
        {
            Destroy(child.gameObject);
        }

        // cardDeck의 모든 카드를 덱 UI에 생성
        foreach (Card card in cardDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, deckUI.transform);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card); // 카드 데이터를 UI에 적용
        }
    }

    // 카드 덱에서 카드를 뽑아 핸드 덱에 추가
    void DrawInitialHand()
    {
        handDeck.Clear();
        for (int i = 0; i < initialHandSize; i++)
        {
            int randomIndex = Random.Range(0, cardDeck.Count); // 랜덤하게 카드 선택
            handDeck.Add(cardDeck[randomIndex]);
            cardDeck.RemoveAt(randomIndex); // 중복 방지를 위해 덱에서 제거
        }

        // UI에 카드 생성
        foreach (Card card in handDeck)
        {
            GameObject newCard = Instantiate(cardPrefab, handDeckUI.transform);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card); // 카드 데이터 설정
        }
    }

    // 카드를 부채꼴 모양으로 배치
    void ArrangeCardsInFanShape()
    {
        int cardCount = handDeck.Count;
        float middleIndex = (cardCount - 1) / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            Transform cardTransform = handDeckUI.transform.GetChild(i);
            float angle = (i - middleIndex) * fanAngle;
            Vector3 position = new Vector3((i - middleIndex) * cardSpacing, 0f, 0f);

            // 카드 회전 및 배치
            cardTransform.localPosition = position;
            cardTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    // 카드를 제출하고 handDeck에서 cardDeck으로 반환
    void ReturnHandCardsToDeck()
    {
        foreach (Card card in handDeck)
        {
            cardDeck.Add(card); // handDeck에서 cardDeck으로 카드 반환
        }

        handDeck.Clear();

        // 기존 핸드 UI의 카드 제거
        foreach (Transform child in handDeckUI.transform)
        {
            Destroy(child.gameObject);
        }

        DrawInitialHand(); // 새로운 핸드를 뽑음
        ArrangeCardsInFanShape(); // 다시 부채꼴로 배치
    }

    void EndGame()
    {
        // handDeck 초기화 및 모든 카드를 cardDeck으로 반환
        foreach (Card card in handDeck)
        {
            cardDeck.Add(card);
        }
        handDeck.Clear();

        // 핸드 UI 초기화
        foreach (Transform child in handDeckUI.transform)
        {
            Destroy(child.gameObject);
        }

        // 게임 상태 초기화 후 다시 카드 뽑기
        DrawInitialHand();
        ArrangeCardsInFanShape();
    }*/
}
