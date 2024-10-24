using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFusionManager : MonoBehaviour
{
    public GameObject shopUI; // ���� UI
    public Transform shopCardParent; // �������� ī�尡 ��ġ�� �θ� UI

    private List<Card> selectedCards = new List<Card>(); // �ռ��� ī�� ����Ʈ

    // ���� UI Ȱ��ȭ �� cardDeck ǥ��
    public void EnterShop()
    {
        shopUI.SetActive(true);
        PopulateShopUI();
    }

    // ���� UI�� cardDeck ǥ��
    void PopulateShopUI()
    {
        foreach (Transform child in shopCardParent)
        {
            Destroy(child.gameObject); // ���� ���� ī�� ����
        }

        foreach (Card card in FindObjectOfType<CardManager>().cardDeck)
        {
            GameObject newCard = Instantiate(FindObjectOfType<CardManager>().cardPrefab, shopCardParent);
            CardUI cardUI = newCard.GetComponent<CardUI>();
            cardUI.SetCardData(card);
            newCard.GetComponent<Button>().onClick.AddListener(() => SelectCardForFusion(card));
        }
    }

    // �ռ��� ī�� ����
    void SelectCardForFusion(Card card)
    {
        if (selectedCards.Count < 3)
        {
            selectedCards.Add(card);
            if (selectedCards.Count == 3)
            {
                TryFusion();
            }
        }
    }

    // 3���� ������ ���� ī�尡 ���õǸ� �ռ� �õ�
    void TryFusion()
    {
        if (selectedCards.Count == 3 && selectedCards[0].level == selectedCards[1].level && selectedCards[1].level == selectedCards[2].level)
        {
            Card baseCard = selectedCards[0];
            baseCard.LevelUp(); // ī�� ������
            selectedCards.Clear();
        }
    }
}
