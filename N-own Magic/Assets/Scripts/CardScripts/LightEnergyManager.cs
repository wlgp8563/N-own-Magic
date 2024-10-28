using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightEnergyManager : MonoBehaviour
{
    public int maxLightEnergy = 3;
    public int currentLightEnergy;
    public TMP_Text energyText;

    void Start()
    {
        currentLightEnergy = maxLightEnergy;
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
            Debug.Log("�������� �����մϴ�!");
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
    }
}