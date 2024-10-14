using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;       // 카드 이름 UI
    public Image cardImage;         // 카드 이미지 UI
    public TMP_Text cardDescriptionText;// 카드 설명 UI
    public TMP_Text cardLightGage;      // 카드 빛에너지 수 UI
    public TMP_Text cardID;             // 카드ID UI 

    // 카드 정보를 UI에 반영하는 함수
    public void SetCardUI(Card cardData)
    {
        cardNameText.text = cardData.cardName;
        cardImage.sprite = cardData.cardImage;
        cardDescriptionText.text = cardData.description;
        cardLightGage.text = cardData.cardGage;
        cardID.text = cardData.cardID;
    }
}
