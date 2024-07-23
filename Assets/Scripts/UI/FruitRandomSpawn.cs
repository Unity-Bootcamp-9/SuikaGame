using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스와이프 관련 이벤트 제거 후 ThrowFruit에서 FruitRandomSpawnManager 호출 방식으로 변경
public class FruitRandomSpawnManager : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    [SerializeField] Transform fruitsSpawnPosition;
    [SerializeField] Transform nextFruitsPosition;

    private Stack<int> randomIndex = new Stack<int>();
    private GameObject currentFruit;
    private GameObject nextFruit;

    public delegate void OnChangeRandom(string fruitName);
    public event OnChangeRandom OnChangeRandomEvent;

    private bool isSwipe = false;

    private void Start()
    {
        MakeRandomIndex();
        SpawnFruits();

        // 스와이프 이벤트 구독
        _swipeEventAsset.eventRaised += OnSwipeEvent;
    }

    private void OnDestroy()
    {
        // 스와이프 이벤트 구독 해제
        _swipeEventAsset.eventRaised -= OnSwipeEvent;
    }

    void MakeRandomIndex()
    {
        randomIndex.Clear();
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Length));
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Length));
    }

    void SpawnFruits()
    {
        if (currentFruit != null)
        {
            // 현재 과일이 던져지면 분리
            currentFruit.transform.SetParent(null, true);
        }

        // 다음 과일을 현재 과일로 설정
        currentFruit = nextFruit;
        if (currentFruit != null)
        {
            currentFruit.transform.SetParent(Camera.main.transform, false);
            currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // 위치 초기화
            currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // 새로운 다음 과일 생성
        nextFruit = Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], nextFruitsPosition.position);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera의 자식으로 설정
            nextFruit.transform.localPosition = new Vector3(0.005f, -0.6f, 1.6f); // 위치 초기화
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // 새로운 랜덤 인덱스 추가
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Length));

        // 다음 과일 이미지 업데이트
        OnChangeRandomEvent?.Invoke(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    // 모든 캡슐 콜라이더 활성화
    private void EnableAllColliders(GameObject fruit)
    {
        CapsuleCollider[] colliders = fruit.GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider collider in colliders)
        {
            collider.enabled = true;
        }
    }

    // 스와이프 이벤트 핸들러
    public void OnSwipeEvent(object sender, SwipeData args)
    {
        if (!isSwipe)
        {
            // 스와이프 이벤트 발생 시 과일 업데이트
            SpawnFruits();
            EnableAllColliders(currentFruit);
            isSwipe = true;
        }
    }

    void Update()
    {
        // 스와이프가 완료된 후 다시 던질 수 있도록 상태 초기화
        if (isSwipe)
        {
            isSwipe = false;            
        }
    }
}
