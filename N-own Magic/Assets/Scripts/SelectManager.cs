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
        button1.onClick.AddListener(() => OnButtonClicked(ref isButton1Clicked, "CardGame1"));
        button2.onClick.AddListener(() => OnButtonClicked(ref isButton2Clicked, "CardGame2"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
    }

    void OnButtonClicked(ref bool buttonClicked, string sceneName)
    {
        // �ش� ��ư�� Ŭ���� ������ �����մϴ�.
        buttonClicked = true;
        SceneManager.LoadScene(sceneName);

        // �� ��ư�� ��� ������ �� �� ��° ��ư�� Ȱ��ȭ�մϴ�.
        if (isButton1Clicked && isButton2Clicked)
        {
            goNextButton.interactable = true;
        }
    }

    void ActivateNextGroup()
    {
        buttonGroup.SetActive(false);
        nextGroup.SetActive(true);
    }
}
