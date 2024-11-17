using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text hpText;
    public Slider hpslider;
    public TMP_Text expText;
    public Slider expslider;
    public TMP_Text levelText;
    public TMP_Text canfusecardText;
    public TMP_Text myTurn;
    public TMP_Text lightEnergy;
    public TMP_Text canHandleDeck;
    public TMP_Text playerCoin;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }
    // Update is called once per frame
    private void UpdateUI()
    {
        var playerState = Player.Instance;
        hpslider.maxValue = playerState.maxhp;
        hpslider.value = playerState.currenthp;
        hpText.text = $"{playerState.currenthp} / {playerState.maxhp}";
        expslider.maxValue = playerState.nexttoexp;
        expslider.value = playerState.exp;
        expText.text = $"{playerState.exp} / {playerState.nexttoexp}";
        levelText.text = $"Lv. {playerState.playerlevel}";
        canfusecardText.text = $"{playerState.canFuseCard}";
        myTurn.text = $"{playerState.playerturn}";
        lightEnergy.text = $"{playerState.currentLightEnergy}";
        canHandleDeck.text = $"{playerState.handdecknum}";
        playerCoin.text = $"{playerState.haveMoney} coin";
    }
}
