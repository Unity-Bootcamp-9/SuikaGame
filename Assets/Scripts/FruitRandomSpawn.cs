    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitRandomSpawn : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    DataManager _dataManager;

    private void OnEnable()
    {
        _swipeEventAsset.eventRaised += FruitsRandomSpawn;
    }

    private void OnDisable()
    {
        _swipeEventAsset.eventRaised -= FruitsRandomSpawn;
    }

    void FruitsRandomSpawn(object sender, SwipeData args)
    {
        if (Camera.main.transform.childCount == 1)
        {
            Debug.Log("������");
            // ������ ������ ī�޶� �ڽ��� 0�� ���� �����ϰ� ���� ����
            int randomIndex = Random.Range(0, _dataManager.fruits.Count - 1); // ���� ��Ͽ��� �����ϰ� �ε��� ����
            Instantiate(_dataManager.fruits[randomIndex], transform.position, transform.rotation); // ���� ���� ��ġ ����
        }
    }
}