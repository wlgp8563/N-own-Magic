using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public GameObject selectedCharacterPrefab;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // �� ��ȯ �� �������� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���õ� ĳ���� ������ ����
    public void SelectCharacter(GameObject characterPrefab)
    {
        selectedCharacterPrefab = characterPrefab;
    }
}
