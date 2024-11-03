using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public Button button1; // 첫 번째 버튼
    public Button button2; // 두 번째 버튼
    public Button goNextButton; // 활성화될 버튼
    public GameObject buttonGroup;
    public GameObject nextGroup;

    private bool isButton1Clicked = false;
    private bool isButton2Clicked = false;

    void Start()
    {
        goNextButton.interactable = false;

        // 첫 번째와 두 번째 버튼 클릭 시의 이벤트 리스너를 추가합니다.
        button1.onClick.AddListener(() => OnButtonClicked(ref isButton1Clicked, "CardGame1"));
        button2.onClick.AddListener(() => OnButtonClicked(ref isButton2Clicked, "CardGame2"));
        goNextButton.onClick.AddListener(ActivateNextGroup);
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
}
