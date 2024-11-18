using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager1 : MonoBehaviour
{
    public Button button1; // 첫 번째 버튼
    public Button button2; // 두 번째 버튼
    public Button goNextButton; // 활성화될 버튼
    public GameObject buttonGroup;
    public GameObject nextGroup;
    public GameObject fuseShop;

    public Button optionButton;
    public GameObject disappearGroup;
    public GameObject appearGroup;

    private bool isButton1Clicked = false;
    private bool isButton2Clicked = false;

    void Start()
    {

        // 첫 번째와 두 번째 버튼 클릭 시의 이벤트 리스너를 추가합니다.
        button1.onClick.AddListener(() => OnButtonClicked(ref isButton1Clicked, "CardGame"));
        button2.onClick.AddListener(() => OnButtonClicked(ref isButton2Clicked, "CardGame"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
        optionButton.onClick.AddListener(OpenOption);
    }

    void OnButtonClicked(ref bool buttonClicked, string sceneName)
    {
        // 해당 버튼이 클릭된 것으로 설정합니다.
        buttonClicked = true;
        SceneManager.LoadScene(sceneName);

        // 두 버튼이 모두 눌렸을 때 세 번째 버튼을 활성화합니다.
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

    void OpenOption()
    {
        fuseShop.SetActive(true);
        disappearGroup.SetActive(false);
        appearGroup.SetActive(true);
    }
}
