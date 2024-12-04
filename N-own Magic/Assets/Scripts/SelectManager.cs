using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager selectManagerInstance;
    
    public Button button1; // 첫 번째 버튼
    public Button button2; // 두 번째 버튼
    public Button goNextButton; // 활성화될 버튼
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

        // 첫 번째와 두 번째 버튼 클릭 시의 이벤트 리스너를 추가합니다.
        button1.onClick.AddListener(() => OnButton1Clicked(ref isButton1Clicked, "CardGame"));
        button2.onClick.AddListener(() => OnButton2Clicked(ref isButton2Clicked, "CardGame"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
    }

    public void OnButton1Clicked(ref bool buttonClicked, string sceneName)
    {
        isButton1Clicked = true; // 첫 번째 버튼 클릭 표시
        button1.interactable = false;
        CheckBothButtonsClicked();
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("CardGame");
    }

    public void OnButton2Clicked(ref bool buttonClicked, string sceneName)
    {
        isButton2Clicked = true; // 두 번째 버튼 클릭 표시
        button2.interactable = false;
        CheckBothButtonsClicked();
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("CardGame");
    }

    private void CheckBothButtonsClicked()
    {
        // 두 버튼이 모두 클릭된 경우에만 활성화할 버튼을 활성화
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
        // 해당 버튼이 클릭된 것으로 설정합니다.
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
        // 두 버튼이 모두 눌렸을 때 세 번째 버튼을 활성화합니다.
        /*if (isButton1Clicked ==true && isButton2Clicked== true)
        {
            Debug.Log("실행");
            goNextButton.interactable = true;
            //Debug.Log(goNextButton);
        }
    }

    void CheckClick()
    {

        if (isButton1Clicked ==true && isButton2Clicked== true)
        {
            //Debug.Log("실행");
            goNextButton.interactable = true;
            Debug.Log(goNextButton.interactable);
        }
        /*if (isButton1Clicked == true)
        {
            if(isButton2Clicked == true)
            {
                goNextButton.interactable = true;
                Debug.Log("실행");
            }
        }

        if(isButton2Clicked == true)
        {
            if(isButton1Clicked == true)
            {
                goNextButton.interactable = true;
                Debug.Log("실행");
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
