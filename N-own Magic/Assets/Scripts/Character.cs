using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int fusionCount = 1; // �ʱ� �ռ� ���� ����

    // �ռ� ���� ������ 1 �����ϴ� �Լ�
    public void DecreaseFusionCount()
    {
        if (fusionCount > 0)
        {
            fusionCount--;
        }
    }

    // �ռ� ���� ���� Ȯ�� �Լ�
    public bool CanFuse()
    {
        return fusionCount > 0;
    }
}
