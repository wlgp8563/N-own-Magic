using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeletedBoss : MonoBehaviour
{
    public List<GameObject> characters; // 캐릭터 프리팹 리스트

    // 특정 인덱스의 캐릭터 선택 및 <싸움> 씬으로 전환
    public void OnCharacterButtonClicked(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            // 선택한 캐릭터를 CharacterManager에 설정
            BossManager.bossManagerInstance.SelectCharacter(characters[characterIndex]);
            SceneManager.LoadScene("CardGame"); // <싸움> 씬 이름을 유니티에서 사용하는 실제 이름으로 설정
        }
    }
}
