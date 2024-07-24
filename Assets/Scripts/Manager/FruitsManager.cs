using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsManager
{
    public GameObject InstantiateFruit(FruitsData fruitsData, Vector3 position, bool isMerged = false)
    {
        GameObject fruitPrefab = Resources.Load<GameObject>(fruitsData.path);
        if (fruitPrefab != null)
        {
            GameObject fruitInstance = Object.Instantiate(fruitPrefab, position, Quaternion.identity);
            if (isMerged)
            {
                fruitInstance.GetComponent<ThrowFruit>().enabled = false;
                Collider[] colliders = fruitInstance.GetComponents<Collider>();
                fruitInstance.GetComponent<Rigidbody>().useGravity = true;
                fruitInstance.GetComponent<CheckGameOver>().TopggleInBowl();

                foreach (Collider collider in colliders)
                {
                    collider.enabled = true;
                }

            }
            fruitInstance.GetComponent<MergeFruit>().fruitData = fruitsData;
            //fruitInstance.transform.localPosition = Vector3.zero; // 위치 초기화
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"프리팹 없음: {fruitsData.path}");
            return null;
        }
    }
}
