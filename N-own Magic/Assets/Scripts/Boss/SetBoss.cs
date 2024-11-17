using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoss : MonoBehaviour
{
    public GameObject bossPrefab;             // ������ �� ������
    //public Vector3 enemySpawnPosition = new Vector3(0f, 0.0f, -3.0f);
    //public Transform bossSpawnPoint;          // ���� ������ ��ġ
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
            // ���õ� ĳ���͸� ���� ����
            _currentCharacter = Instantiate(BossManager.bossManagerInstance.selectedCharacterPrefab, canvas.transform);
            _currentCharacter.transform.localPosition = new Vector3(25f, 200f, 0f);
            _currentCharacter.transform.SetAsLastSibling();

            ChangeSortingLayer(_currentCharacter, "Prefab");
        }
    }

    private void ChangeSortingLayer(GameObject obj, string layerName)
    {
        // SpriteRenderer�� �ִ� ��쿡�� Sorting Layer ����
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = layerName;
        }

        // UI ����� ���
        CanvasRenderer canvasRenderer = obj.GetComponent<CanvasRenderer>();
        if (canvasRenderer != null)
        {
            // UI ����� Sorting Layer�� �����ϴ� ���
            obj.GetComponent<RectTransform>().SetAsLastSibling(); // UI�� Canvas�� �ֻ�ܿ� ��ġ
        }
    }
}
