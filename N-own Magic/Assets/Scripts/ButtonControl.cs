using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button[] buttons;

    public GameObject[] gameobjects;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(Shop1Exit);
        buttons[1].onClick.AddListener(Shop2Exit);
        buttons[2].onClick.AddListener(LetterExit);
        buttons[3].onClick.AddListener(Shop3Exit);
    }
    void Shop1Exit()
    {
        gameobjects[0].SetActive(false);
    }
    void Shop2Exit()
    {
        gameobjects[1].SetActive(false);
    }

    void LetterExit()
    {
        gameobjects[2].SetActive(false);
    }
    void Shop3Exit()
    {
        gameobjects[3].SetActive(false);
    }
}
