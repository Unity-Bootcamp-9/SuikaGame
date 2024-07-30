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
                fruitInstance.GetComponent<CheckGameOver>().ToggleInBowl();

                foreach (Collider collider in colliders)
                {
                    collider.isTrigger = false;
                }
            }
            fruitInstance.GetComponent<MergeFruit>().fruitData = fruitsData;
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"ÇÁ¸®ÆÕ ¾øÀ½: {fruitsData.path}");
            return null;
        }
    }
/*
    public GameObject InstantiateFruit(Transform targetParent, FruitsData fruitsData, Vector3 position, bool isMerged = false)
    {
        GameObject createdFruit = InstantiateFruit(fruitsData, position, isMerged);
        createdFruit.transform.parent = targetParent;

        return createdFruit;
    }*/
}
