using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour
{
    public void OnFightButtonClicked()
    {
        SceneManager.LoadScene("CardGameScene"); // <ī�����> ������ �̵�
    }
}
