using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ���� �̺�Ʈ ���� �� ThrowFruit���� FruitRandomSpawnManager ȣ�� ������� ����
public class FruitRandomSpawnManager
{
    Vector3 fruitsSpawnPosition = new Vector3(0.22f, -3f, -0.2f);

    private Queue<int> randomIndex = new Queue<int>();
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
        randomIndex.Enqueue(UnityEngine.Random.Range(0, maxRange));
        randomIndex.Enqueue(UnityEngine.Random.Range(0, maxRange));
    }

    public void SpawnFruits()
    {
        // ���ο� ���� ���� ����
        GameObject nextFruit = Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[randomIndex.Dequeue()], fruitsSpawnPosition);  
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera�� �ڽ����� ����
            nextFruit.transform.localPosition = new Vector3(0.007f, -0.25f, 0.67f);
        }

        // ���ο� ���� �ε��� �߰�
        randomIndex.Enqueue(UnityEngine.Random.Range(0, maxRange));

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
