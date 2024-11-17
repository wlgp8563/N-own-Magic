using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightEnergyManager : MonoBehaviour
{
    /*public TMP_Text energyText;

    void Start()
    {
        Player.Instance.currentLightEnergy = Player.Instance.lightenergy;
        UpdateEnergyUI();
    }

    public bool UseEnergy(int amount)
    {
        if (currentLightEnergy >= amount)
        {
            currentLightEnergy -= amount;
            UpdateEnergyUI();
            return true;
        }
        else
        {
            Debug.Log("에너지가 부족합니다!");
            return false;
        }
    }

    public void AddEnergy(int amount)
    {
        currentLightEnergy += amount;
        if (currentLightEnergy > maxLightEnergy)
            currentLightEnergy = maxLightEnergy;
        UpdateEnergyUI();
    }

    void UpdateEnergyUI()
    {
        energyText.text = $"Energy: {currentLightEnergy}/{maxLightEnergy}";
    }

    public void RestoreEnergyAtTurnStart(int amount)
    {
        AddEnergy(amount);
    }*/
}
