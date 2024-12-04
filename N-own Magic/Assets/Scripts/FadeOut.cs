using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup ������Ʈ ����
    public float fadeDuration = 1f; // ���̵� ���� �ð�

    private void Start()
    {
        StartFadeOut();
    }

    public void StartFadeOut()
    {
        StartCoroutine(CFadeOut());
    }

    private IEnumerator CFadeOut()
    {
        float startAlpha = canvasGroup.alpha; // ���� alpha ��
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f; // ������ �����ϰ� ����
        canvasGroup.interactable = false; // ��ȣ�ۿ� ��Ȱ��ȭ
        canvasGroup.blocksRaycasts = false; // Ŭ�� ����
    }
}
