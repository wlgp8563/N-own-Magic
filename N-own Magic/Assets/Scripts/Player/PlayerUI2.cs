using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI2 : MonoBehaviour
{
    public TMP_Text hpText;
    public Slider hpslider;
    public TMP_Text myTurn;
    public TMP_Text lightEnergy;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var playerState = Player.Instance;
        hpslider.maxValue = playerState.maxhp;
        hpslider.value = playerState.currenthp;
        hpText.text = $"{playerState.currenthp} / {playerState.maxhp}";
        myTurn.text = $"{playerState.playerturn}";
        lightEnergy.text = $"{playerState.lightenergy}";
    }
}
