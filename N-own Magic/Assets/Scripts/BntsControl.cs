using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BntsControl : MonoBehaviour
{
    public Button[] buttons;

    public GameObject[] manuals;
    public GameObject canvas;
    public GameObject Mcanvas;
    private int currentIndex = 0;

    public AudioSource btnAudio;
    public AudioClip btnClip;

    // Start is called before the first frame update
    void Start()
    {
        buttons[0].onClick.AddListener(InGameBnt);
        buttons[1].onClick.AddListener(InManual);
        buttons[2].onClick.AddListener(GameExit);
        //buttons[3].onClick.AddListener(NextPage);

        foreach (GameObject obj in manuals)
        {
            obj.SetActive(false);
        }
        if(manuals.Length >0)
        {
            manuals[0].SetActive(true);
        }
    }

    public void InGameBnt()
    {
        btnAudio.PlayOneShot(btnClip);
        SceneManager.LoadScene("Letter");
    }

    public void InManual()
    {
        btnAudio.PlayOneShot(btnClip);
        Mcanvas.SetActive(true);
        canvas.SetActive(false);
    }

    public void GameExit()
    {
        btnAudio.PlayOneShot(btnClip);
        Application.Quit();
    }

    public void OnNextButtonClick()
    {
        btnAudio.PlayOneShot(btnClip);
        manuals[currentIndex].SetActive(false);

        currentIndex = (currentIndex + 1) % manuals.Length;

        manuals[currentIndex].SetActive(true);

        if (currentIndex == manuals.Length - 1)
        {
            Mcanvas.SetActive(false);  // 캔버스를 활성화
            canvas.SetActive(true);
        }
    }

    public void OnBeforeButtonClick()
    {
        btnAudio.PlayOneShot(btnClip);
        // 현재 오브젝트 비활성화
        manuals[currentIndex].SetActive(false);

        // 이전 오브젝트 인덱스 계산 (순환 방식)
        currentIndex = (currentIndex - 1 + manuals.Length) % manuals.Length;

        // 새로운 오브젝트 활성화
        manuals[currentIndex].SetActive(true);
    }
}
