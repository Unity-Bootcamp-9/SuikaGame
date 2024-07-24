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
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Length));
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Length));
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
        if (currentFruit != null)
        {
            currentFruit.transform.SetParent(Camera.main.transform, false);
            currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // ��ġ �ʱ�ȭ
            currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���ο� ���� ���� ����
        nextFruit = Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], fruitsSpawnPosition);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera�� �ڽ����� ����
            nextFruit.transform.localPosition = new Vector3(0.005f, -0.6f, 1.6f); // ��ġ �ʱ�ȭ
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���ο� ���� �ε��� �߰�
        randomIndex.Push(UnityEngine.Random.Range(0, Managers.Data.fruits.Length));

        // ���� ���� �̹��� ������Ʈ
        OnChangeRandomEvent?.Invoke(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    // ��� ĸ�� �ݶ��̴� Ȱ��ȭ
    public void EnableAllColliders(GameObject fruit)
    {
        CapsuleCollider[] colliders = fruit.GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
