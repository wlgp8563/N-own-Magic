using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BntsControl : MonoBehaviour
{
    public Button[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(InGameBnt);
        buttons[1].onClick.AddListener(InManual);
        buttons[2].onClick.AddListener(GameExit);
    }

    public void InGameBnt()
    {
        SceneManager.LoadScene("InGame");
    }

    public void InManual()
    {
        //SceneManager.LoadScene("InGame");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
