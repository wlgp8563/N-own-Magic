using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup 컴포넌트 참조
    public float fadeDuration = 1f; // 페이드 지속 시간

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
        float startAlpha = canvasGroup.alpha; // 현재 alpha 값
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f; // 완전히 투명하게 설정
        canvasGroup.interactable = false; // 상호작용 비활성화
        canvasGroup.blocksRaycasts = false; // 클릭 막기
    }
}
