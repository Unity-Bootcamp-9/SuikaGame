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

        // �������� �̺�Ʈ ����
        _swipeEventAsset.eventRaised += OnSwipeEvent;
    }

    private void OnDestroy()
    {
        // �������� �̺�Ʈ ���� ����
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
            // ���� ������ �������� �и�
            currentFruit.transform.SetParent(null, true);
        }

        // ���� ������ ���� ���Ϸ� ����
        currentFruit = nextFruit;
        if (currentFruit != null)
        {
            currentFruit.transform.SetParent(Camera.main.transform, false);
            currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // ��ġ �ʱ�ȭ
            currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���ο� ���� ���� ����
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], nextFruitsPosition.position);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera�� �ڽ����� ����
            nextFruit.transform.localPosition = new Vector3(0.005f, -0.6f, 1.6f); // ��ġ �ʱ�ȭ
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���ο� ���� �ε��� �߰�
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Count));

        // ���� ���� �̹��� ������Ʈ
        OnChangeRandomEvent?.Invoke(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    GameObject InstantiateFruit(FruitsData fruitsData, Vector3 position)
    {
        GameObject fruitPrefab = Resources.Load<GameObject>(fruitsData.path);
        if (fruitPrefab != null)
        {
            GameObject fruitInstance = Instantiate(fruitPrefab, position, Quaternion.identity);
            fruitInstance.transform.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"������ ����: {fruitsData.path}");
            return null;
        }
    }

    // �������� �̺�Ʈ �ڵ鷯
    public void OnSwipeEvent(object sender, SwipeData args)
    {
        if (!isSwipe)
        {
            // �������� �̺�Ʈ �߻� �� ���� ������Ʈ
            SpawnFruits();
            currentFruit.GetComponent<CapsuleCollider>().enabled = true;
            isSwipe = true;
        }
    }

    void Update()
    {
        // ���������� �Ϸ�� �� �ٽ� ���� �� �ֵ��� ���� �ʱ�ȭ
        if (isSwipe)
        {
            isSwipe = false;            
        }
    }
}
