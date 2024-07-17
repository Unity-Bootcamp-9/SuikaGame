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
        if (ifThrown) // �������� ������ ���� ���� ���� ���� ����
        {
            // �����ϰ� ���� ����
            int randomIndex = Random.Range(0, fruitSpawnList.Count); // ���� ��Ͽ��� �����ϰ� �ε��� ����
            Instantiate(fruitSpawnList[randomIndex], transform.position, transform.rotation); // ���� ���� ��ġ ����
            ifThrown = false;
        }

        // ������ ���������� �Ǵ�
        //if ( ���������� �������� ��� �Ǻ��� �� �ִ��� ã�ƺ���)
    }
}