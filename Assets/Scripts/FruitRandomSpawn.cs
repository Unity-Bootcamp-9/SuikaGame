    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitRandomSpawn : MonoBehaviour
{
    // Managers.Data -> 작성하면 바로 사용 가능
    void FruitsRandomSpawn(object sender, SwipeData args)
    {
        if (Camera.main.transform.childCount == 1)
        {
            Debug.Log("던져짐");
            // 과일이 던져져 카메라 자식이 0일 때만 랜덤하게 과일 생성
            int randomIndex = Random.Range(0, Managers.Data.fruits.Count - 1); // 과일 목록에서 랜덤하게 인덱스 선택

            Instantiate(.fruits[randomIndex], transform.position, transform.rotation); // 생성 과일 위치 설정
        }
    }
}