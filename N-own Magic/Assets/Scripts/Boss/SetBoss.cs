using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoss : MonoBehaviour
{
    public GameObject bossPrefab;             // 스폰할 적 프리팹
    //public Vector3 enemySpawnPosition = new Vector3(0f, 0.0f, -3.0f);
    //public Transform bossSpawnPoint;          // 적이 스폰될 위치
    private GameObject _currentCharacter;
    public Canvas canvas;

    private void Start()
    {
        SpawnSelectedCharacter();
    }

    private void SpawnSelectedCharacter()
    {
        if (BossManager.bossManagerInstance.selectedCharacterPrefab != null)
        {
            // 선택된 캐릭터를 씬에 스폰
            _currentCharacter = Instantiate(BossManager.bossManagerInstance.selectedCharacterPrefab, canvas.transform);
            _currentCharacter.transform.localPosition = new Vector3(25f, 200f, 0f);
            _currentCharacter.transform.SetAsLastSibling();

            ChangeSortingLayer(_currentCharacter, "Prefab");
        }
    }

    private void ChangeSortingLayer(GameObject obj, string layerName)
    {
        // SpriteRenderer가 있는 경우에만 Sorting Layer 변경
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = layerName;
        }

        // UI 요소의 경우
        CanvasRenderer canvasRenderer = obj.GetComponent<CanvasRenderer>();
        if (canvasRenderer != null)
        {
            // UI 요소의 Sorting Layer를 변경하는 방법
            obj.GetComponent<RectTransform>().SetAsLastSibling(); // UI를 Canvas의 최상단에 배치
        }
    }
}
