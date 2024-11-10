using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public Button button1; // ù ��° ��ư
    public Button button2; // �� ��° ��ư
    public Button goNextButton; // Ȱ��ȭ�� ��ư
    public GameObject buttonGroup;
    public GameObject nextGroup;

    private bool isButton1Clicked = false;
    private bool isButton2Clicked = false;

    void Start()
    {
        goNextButton.interactable = false;

        // ù ��°�� �� ��° ��ư Ŭ�� ���� �̺�Ʈ �����ʸ� �߰��մϴ�.
        //button1.onClick.AddListener(() => OnButtonClicked(ref isButton1Clicked, "CardGame"));
        //button2.onClick.AddListener(() => OnButtonClicked(ref isButton2Clicked, "CardGame"));
        //goNextButton.onClick.AddListener(ActivateNextGroup);
    }

    public void OnButton1Clicked()
    {
        isButton1Clicked = true; // ù ��° ��ư Ŭ�� ǥ��
        CheckBothButtonsClicked();
        SceneManager.LoadScene("CardGame");
    }

    public void OnButton2Clicked()
    {
        isButton2Clicked = true; // �� ��° ��ư Ŭ�� ǥ��
        CheckBothButtonsClicked();
        SceneManager.LoadScene("CardGame");
    }

    private void CheckBothButtonsClicked()
    {
        // �� ��ư�� ��� Ŭ���� ��쿡�� Ȱ��ȭ�� ��ư�� Ȱ��ȭ
        if (isButton1Clicked == true && isButton2Clicked == true)
        {
            Debug.Log("play");
            goNextButton.interactable = true;
        }
    }

    public void GoNextButton()
    {
        ActivateNextGroup();
    }
    /*void OnButtonClicked(ref bool buttonClicked, string sceneName)
    {
        //CheckClick(ref buttonClicked);
        // �ش� ��ư�� Ŭ���� ������ �����մϴ�.
        //buttonClicked = true;
        if(buttonClicked == isButton1Clicked)
        {
            isButton1Clicked = true;
            CheckClick();

        }

        if(buttonClicked == isButton2Clicked)
        {
            isButton2Clicked = true;
            CheckClick();
        }

        SceneManager.LoadScene(sceneName);
        // �� ��ư�� ��� ������ �� �� ��° ��ư�� Ȱ��ȭ�մϴ�.
        /*if (isButton1Clicked ==true && isButton2Clicked== true)
        {
            Debug.Log("����");
            goNextButton.interactable = true;
            //Debug.Log(goNextButton);
        }
    }

    void CheckClick()
    {

        if (isButton1Clicked ==true && isButton2Clicked== true)
        {
            //Debug.Log("����");
            goNextButton.interactable = true;
            Debug.Log(goNextButton.interactable);
        }
        /*if (isButton1Clicked == true)
        {
            if(isButton2Clicked == true)
            {
                goNextButton.interactable = true;
                Debug.Log("����");
            }
        }

        if(isButton2Clicked == true)
        {
            if(isButton1Clicked == true)
            {
                goNextButton.interactable = true;
                Debug.Log("����");
            }
        }
    }*/

    void ActivateNextGroup()
    {
        buttonGroup.SetActive(false);
        nextGroup.SetActive(true);
    }
}