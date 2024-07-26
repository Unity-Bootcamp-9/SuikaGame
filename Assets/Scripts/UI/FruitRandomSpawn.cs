using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스와이프 관련 이벤트 제거 후 ThrowFruit에서 FruitRandomSpawnManager 호출 방식으로 변경
public class FruitRandomSpawnManager
{
    Vector3 fruitsSpawnPosition = new Vector3(0.22f, -3f, -0.2f);

    private Stack<int> randomIndex = new Stack<int>();
    private GameObject currentFruit;
    private GameObject nextFruit;
    private int maxRange = 6;

    public delegate void OnChangeRandom(string fruitName);
    public event OnChangeRandom OnChangeRandomEvent;

    public void Init()
    {
        MakeRandomIndex();
        SpawnFruits();
    }

    void MakeRandomIndex()
    {
        randomIndex.Clear();
        randomIndex.Push(UnityEngine.Random.Range(0, maxRange));
        randomIndex.Push(UnityEngine.Random.Range(0, maxRange));
    }

    public void SpawnFruits()
    {
        if (currentFruit != null)
        {
            // 현재 과일이 던져지면 분리
            currentFruit.transform.SetParent(null, true);
        }

        // 프로토타입 이후 currentFruit랑 nextFruit 하나로 합치기
        // 애초에 두개로 나눌 필요가 없어짐

        // 다음 과일을 현재 과일로 설정
        currentFruit = nextFruit;

        // 새로운 다음 과일 생성
        nextFruit = Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], fruitsSpawnPosition);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera의 자식으로 설정
            nextFruit.transform.localPosition = new Vector3(0.007f, -0.25f, 0.67f);
        }

        // 새로운 랜덤 인덱스 추가
        randomIndex.Push(UnityEngine.Random.Range(0, maxRange));

        // 다음 과일 이미지 업데이트
        OnChangeRandomEvent?.Invoke(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    // 모든 캡슐 콜라이더 활성화
    public void EnableAllColliders(GameObject fruit)
    {
        Collider[] colliders = fruit.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
        }
    }
}
