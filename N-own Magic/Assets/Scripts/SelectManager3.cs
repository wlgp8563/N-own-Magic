using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager3 : MonoBehaviour
{
    public static SelectManager3 selectManagerInstance3;

    public Button button1; // ù ��° ��ư
    public Button button2; // �� ��° ��ư
    public Button goNextButton; // Ȱ��ȭ�� ��ư
    //public GameObject buttonGroup;
    //public GameObject nextGroup;
    public GameObject healShop;
    public GameObject shop;

    public Button optionButton;
    public GameObject disappearGroup;
    public GameObject appearGroup;

    private bool isButton1Clicked = false;
    private bool isButton2Clicked = false;

    public AudioClip healClip;
    public AudioClip btnClip;
    public AudioSource healSource;

    private void Awake()
    {
        if (selectManagerInstance3 == null)
        {
            selectManagerInstance3 = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        // ù ��°�� �� ��° ��ư Ŭ�� ���� �̺�Ʈ �����ʸ� �߰��մϴ�.
        button1.onClick.AddListener(OpenShop);
        button2.onClick.AddListener(() => OnButtonClicked(ref isButton2Clicked, "CardGame"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
        optionButton.onClick.AddListener(OpenOption);
    }

    void OpenShop()
    {
        healSource.PlayOneShot(btnClip);
        shop.SetActive(true);
    }

    void OnButtonClicked(ref bool buttonClicked, string sceneName)
    {
        // �ش� ��ư�� Ŭ���� ������ �����մϴ�.
        buttonClicked = true;
        SceneManager.LoadScene(sceneName);

        disappearGroup.SetActive(false);
        appearGroup.SetActive(true);

        // �� ��ư�� ��� ������ �� �� ��° ��ư�� Ȱ��ȭ�մϴ�.
        if (isButton1Clicked && isButton2Clicked)
        {
            goNextButton.interactable = true;
        }
    }

    void ActivateNextGroup()
    {
        SceneManager.LoadScene("CardGame");
        //buttonGroup.SetActive(false);
        //nextGroup.SetActive(true);
    }

    void OpenOption()
    {
        Player.Instance.maxhp += 35;
        Player.Instance.currenthp = Player.Instance.maxhp;
        healSource.PlayOneShot(healClip);
        healShop.SetActive(true);
        //disappearGroup.SetActive(false);
        //appearGroup.SetActive(true);
    }
}
