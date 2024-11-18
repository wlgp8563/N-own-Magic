using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedEnemy : MonoBehaviour
{
    public List<GameObject> characters; // 캐릭터 프리팹 리스트

    // 특정 인덱스의 캐릭터 선택 및 <CardGame> 씬으로 전환
    public void OnCharacterButtonClicked(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            EnemyManager.Instance.SelectCharacter(characters[characterIndex]);
            SceneManager.LoadScene("CardGame"); 
        }
    }
}
