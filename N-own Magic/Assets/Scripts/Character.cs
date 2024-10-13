using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int fusionCount = 1; // 초기 합성 가능 갯수

    // 합성 가능 갯수를 1 차감하는 함수
    public void DecreaseFusionCount()
    {
        if (fusionCount > 0)
        {
            fusionCount--;
        }
    }

    // 합성 가능 여부 확인 함수
    public bool CanFuse()
    {
        return fusionCount > 0;
    }
}
