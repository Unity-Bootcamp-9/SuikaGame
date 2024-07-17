using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitRandomSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> fruitSpawnList;
    bool ifThrown = false;

    // Update is called once per frame
    void Update()
    {
        if (ifThrown) // 던져져서 과일이 없을 때만 다음 과일 생성
        {
            // 랜덤하게 과일 생성
            int randomIndex = Random.Range(0, fruitSpawnList.Count); // 과일 목록에서 랜덤하게 인덱스 선택
            Instantiate(fruitSpawnList[randomIndex], transform.position, transform.rotation); // 생성 과일 위치 설정
            ifThrown = false;
        }

        // 과일이 던져졌는지 판단
        //if ( 스와이프로 던져진걸 어떻게 판별할 수 있는지 찾아보기)
    }
}