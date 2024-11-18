using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedEnemy : MonoBehaviour
{
    public List<GameObject> characters; // ĳ���� ������ ����Ʈ

    // Ư�� �ε����� ĳ���� ���� �� <CardGame> ������ ��ȯ
    public void OnCharacterButtonClicked(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            EnemyManager.Instance.SelectCharacter(characters[characterIndex]);
            SceneManager.LoadScene("CardGame"); 
        }
    }
}
