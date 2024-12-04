using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager selectManagerInstance;
    
    public Button button1; // ù ��° ��ư
    public Button button2; // �� ��° ��ư
    public Button goNextButton; // Ȱ��ȭ�� ��ư
    public GameObject buttonGroup;
    public GameObject nextGroup;

    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private bool isButton1Clicked = false;
    private bool isButton2Clicked = false;

    private void Awake()
    {
        if (selectManagerInstance == null)
        {
            selectManagerInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //goNextButton.interactable = false;

        // ù ��°�� �� ��° ��ư Ŭ�� ���� �̺�Ʈ �����ʸ� �߰��մϴ�.
        button1.onClick.AddListener(() => OnButton1Clicked(ref isButton1Clicked, "CardGame"));
        button2.onClick.AddListener(() => OnButton2Clicked(ref isButton2Clicked, "CardGame"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
    }

    public void OnButton1Clicked(ref bool buttonClicked, string sceneName)
    {
        isButton1Clicked = true; // ù ��° ��ư Ŭ�� ǥ��
        button1.interactable = false;
        CheckBothButtonsClicked();
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("CardGame");
    }

    public void OnButton2Clicked(ref bool buttonClicked, string sceneName)
    {
        isButton2Clicked = true; // �� ��° ��ư Ŭ�� ǥ��
        button2.interactable = false;
        CheckBothButtonsClicked();
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);
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
        audioSource.volume = 0.55f;
        audioSource.PlayOneShot(buttonClickSound);
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
        //audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("InGame2");

        //buttonGroup.SetActive(false);
        //nextGroup.SetActive(true);
    }
}
