using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public Image cardImage;
    public TMP_Text descriptionText;
    public TMP_Text cardIDText;
    public TMP_Text cardLightEnergy;

    private Card cardData;

    // ī�� �����͸� �����ϰ� UI ������Ʈ
    public void SetCardData(Card card)
    {
        cardData = card;
        cardNameText.text = card.cardName;
        cardImage.sprite = card.cardImage;
        descriptionText.text = card.description;
        cardIDText.text = card.cardID;
        cardLightEnergy.text = card.lightEnergy.ToString();
    }
}
