using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager bossManagerInstance;
    public GameObject selectedCharacterPrefab;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (bossManagerInstance == null)
        {
            bossManagerInstance = this;
            //DontDestroyOnLoad(gameObject); // 씬 전환 시 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 선택된 캐릭터 프리팹 저장
    public void SelectCharacter(GameObject characterPrefab)
    {
        selectedCharacterPrefab = characterPrefab;
    }
}
