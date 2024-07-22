using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Count));
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Count));
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
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], nextFruitsPosition.position);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera의 자식으로 설정
            nextFruit.transform.localPosition = new Vector3(0.005f, -0.6f, 1.6f); // 위치 초기화
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // 새로운 랜덤 인덱스 추가
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Count));

        // 다음 과일 이미지 업데이트
        OnChangeRandomEvent?.Invoke(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    GameObject InstantiateFruit(FruitsData fruitsData, Vector3 position)
    {
        GameObject fruitPrefab = Resources.Load<GameObject>(fruitsData.path);
        if (fruitPrefab != null)
        {
            GameObject fruitInstance = Instantiate(fruitPrefab, position, Quaternion.identity);
            fruitInstance.transform.localPosition = Vector3.zero; // 위치 초기화
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"프리팹 없음: {fruitsData.path}");
            return null;
        }
    }

    // 스와이프 이벤트 핸들러
    public void OnSwipeEvent(object sender, SwipeData args)
    {
        if (!isSwipe)
        {
            // 스와이프 이벤트 발생 시 과일 업데이트
            SpawnFruits();
            currentFruit.GetComponent<CapsuleCollider>().enabled = true;
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
