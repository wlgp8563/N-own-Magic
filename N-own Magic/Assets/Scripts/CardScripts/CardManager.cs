using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Transform cardParent; // 카드들이 배치될 부모 UI
    public GameObject cardPrefab; // 카드 프리팹
    public Button deckButton; // 덱을 보여주는 버튼
    public Button submitButton; // 카드 제출 버튼
    public List<Card> cardDeck = new List<Card>(); // 전체 카드 덱
    public List<Card> handDeck = new List<Card>(); // 현재 화면에 보이는 덱

    public GameObject deckUI; // 덱 UI
    public GameObject handDeckUI; // 핸드 덱 UI

    public int initialHandSize = 4; // 게임 시작 시 뽑을 카드 수
    public float cardSpacing = 50f; // 카드 간격
    public float fanAngle = 10f; // 부채꼴 각도

    void Start()
    {
        deckButton.onClick.AddListener(ShowCardDeckUI); // 덱 버튼 클릭 이벤트
        submitButton.onClick.AddListener(ReturnHandCardsToDeck); // 제출 버튼 클릭 이벤트

        DrawInitialHand();
        ArrangeCardsInFanShape();
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
    }
}
