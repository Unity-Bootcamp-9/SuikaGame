using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ���� �̺�Ʈ ���� �� ThrowFruit���� FruitRandomSpawnManager ȣ�� ������� ����
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
            // ���� ������ �������� �и�
            currentFruit.transform.SetParent(null, true);
        }

        // ������Ÿ�� ���� currentFruit�� nextFruit �ϳ��� ��ġ��
        // ���ʿ� �ΰ��� ���� �ʿ䰡 ������

        // ���� ������ ���� ���Ϸ� ����
        currentFruit = nextFruit;

        // ���ο� ���� ���� ����
        nextFruit = Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], fruitsSpawnPosition);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera�� �ڽ����� ����
            nextFruit.transform.localPosition = new Vector3(0.007f, -0.25f, 0.67f);
        }

        // ���ο� ���� �ε��� �߰�
        randomIndex.Push(UnityEngine.Random.Range(0, maxRange));

        // ���� ���� �̹��� ������Ʈ
        OnChangeRandomEvent?.Invoke(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    // ��� ĸ�� �ݶ��̴� Ȱ��ȭ
    public void EnableAllColliders(GameObject fruit)
    {
        Collider[] colliders = fruit.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
        }
    }
}