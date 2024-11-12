using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public void OnFightButtonClicked()
    {
        SceneManager.LoadScene("CardGameScene"); // <카드게임> 씬으로 이동
    }
}
