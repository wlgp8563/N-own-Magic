using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LetterManager : MonoBehaviour
{
    //public Button enter;
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    // Start is called before the first frame update
    void Start()
    {
        //enter.onClick.AddListener(EnterGame);
    }

    public void EnterGame()
    {
        Debug.Log("In");
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("InGame");
    }
}
