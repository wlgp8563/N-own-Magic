using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager1 : MonoBehaviour
{
    public Button button1; // ù ��° ��ư
    public Button button2; // �� ��° ��ư
    public Button goNextButton; // Ȱ��ȭ�� ��ư
    public GameObject buttonGroup;
    public GameObject nextGroup;
    public GameObject fuseShop;

    public Button optionButton;
    public GameObject disappearGroup;
    public GameObject appearGroup;

    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private bool isButton1Clicked = false;
    private bool isButton2Clicked = false;

    void Start()
    {

        // ù ��°�� �� ��° ��ư Ŭ�� ���� �̺�Ʈ �����ʸ� �߰��մϴ�.
        button1.onClick.AddListener(() => OnButtonClicked(ref isButton1Clicked, "CardGame"));
        button2.onClick.AddListener(() => OnButtonClicked(ref isButton2Clicked, "CardGame"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
        optionButton.onClick.AddListener(OpenOption);
    }

    void OnButtonClicked(ref bool buttonClicked, string sceneName)
    {
        // �ش� ��ư�� Ŭ���� ������ �����մϴ�.
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);
        buttonClicked = true;
        SceneManager.LoadScene("CardGame");

        // �� ��ư�� ��� ������ �� �� ��° ��ư�� Ȱ��ȭ�մϴ�.
        if (isButton1Clicked && isButton2Clicked)
        {
            goNextButton.interactable = true;
        }
    }

    void ActivateNextGroup()
    {
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);

        SceneManager.LoadScene("InGame3");

        buttonGroup.SetActive(false);
        nextGroup.SetActive(true);
    }

    void OpenOption()
    {
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);

        fuseShop.SetActive(true);
        disappearGroup.SetActive(false);
        appearGroup.SetActive(true);
    }
}
