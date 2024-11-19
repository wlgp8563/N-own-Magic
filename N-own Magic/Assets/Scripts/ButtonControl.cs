using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public static ButtonControl btncontrolInstance;
    
    public Button[] buttons;

    public GameObject[] gameobjects;

    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private void Awake()
    {
        if (btncontrolInstance == null)
        {
            btncontrolInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(Shop1Exit);
        buttons[1].onClick.AddListener(Shop2Exit);
        buttons[2].onClick.AddListener(LetterExit);
        buttons[3].onClick.AddListener(Shop3Exit);
        buttons[4].onClick.AddListener(Shop4Enter);
        buttons[5].onClick.AddListener(Shop4Exit);
    }
    void Shop1Exit()
    {
        audioSource.Play();
        gameobjects[0].SetActive(false);
    }
    void Shop2Exit()
    {
        audioSource.Play();
        gameobjects[1].SetActive(false);
    }

    void LetterExit()
    {
        audioSource.Play();
        gameobjects[2].SetActive(false);
    }
    void Shop3Exit()
    {
        audioSource.Play();
        gameobjects[3].SetActive(false);
    }

    void Shop4Enter()
    {
        audioSource.Play();
        gameobjects[4].SetActive(true);
    }

    void Shop4Exit()
    {
        audioSource.Play();
        gameobjects[5].SetActive(false);
    }
}
