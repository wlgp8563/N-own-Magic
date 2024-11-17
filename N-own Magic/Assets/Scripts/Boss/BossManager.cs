using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager bossManagerInstance;
    public GameObject selectedCharacterPrefab;

    private void Awake()
    {
        // �̱��� ���� ����
        if (bossManagerInstance == null)
        {
            bossManagerInstance = this;
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
