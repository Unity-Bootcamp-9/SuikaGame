    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitRandomSpawn : MonoBehaviour
{
    // Managers.Data -> �ۼ��ϸ� �ٷ� ��� ����
    void FruitsRandomSpawn(object sender, SwipeData args)
    {
        if (Camera.main.transform.childCount == 1)
        {
            Debug.Log("������");
            // ������ ������ ī�޶� �ڽ��� 0�� ���� �����ϰ� ���� ����
            int randomIndex = Random.Range(0, Managers.Data.fruits.Count - 1); // ���� ��Ͽ��� �����ϰ� �ε��� ����

            Instantiate(.fruits[randomIndex], transform.position, transform.rotation); // ���� ���� ��ġ ����
        }
    }
}