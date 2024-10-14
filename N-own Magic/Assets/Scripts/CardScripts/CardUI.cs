using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;       // ī�� �̸� UI
    public Image cardImage;         // ī�� �̹��� UI
    public TMP_Text cardDescriptionText;// ī�� ���� UI
    public TMP_Text cardLightGage;      // ī�� �������� �� UI
    public TMP_Text cardID;             // ī��ID UI 

    // ī�� ������ UI�� �ݿ��ϴ� �Լ�
    public void SetCardUI(Card cardData)
    {
        cardNameText.text = cardData.cardName;
        cardImage.sprite = cardData.cardImage;
        cardDescriptionText.text = cardData.description;
        cardLightGage.text = cardData.cardGage;
        cardID.text = cardData.cardID;
    }
}
