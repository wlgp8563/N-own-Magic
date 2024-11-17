using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeletedBoss : MonoBehaviour
{
    public List<GameObject> characters; // ĳ���� ������ ����Ʈ

    // Ư�� �ε����� ĳ���� ���� �� <�ο�> ������ ��ȯ
    public void OnCharacterButtonClicked(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characters.Count)
        {
            // ������ ĳ���͸� CharacterManager�� ����
            BossManager.bossManagerInstance.SelectCharacter(characters[characterIndex]);
            SceneManager.LoadScene("CardGame"); // <�ο�> �� �̸��� ����Ƽ���� ����ϴ� ���� �̸����� ����
        }
    }
}
