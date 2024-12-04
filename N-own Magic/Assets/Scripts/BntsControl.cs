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
            Mcanvas.SetActive(false);  // ĵ������ Ȱ��ȭ
            canvas.SetActive(true);
        }
    }

    public void OnBeforeButtonClick()
    {
        btnAudio.PlayOneShot(btnClip);
        // ���� ������Ʈ ��Ȱ��ȭ
        manuals[currentIndex].SetActive(false);

        // ���� ������Ʈ �ε��� ��� (��ȯ ���)
        currentIndex = (currentIndex - 1 + manuals.Length) % manuals.Length;

        // ���ο� ������Ʈ Ȱ��ȭ
        manuals[currentIndex].SetActive(true);
    }
}
