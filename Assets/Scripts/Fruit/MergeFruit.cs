using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MergeFruit : MonoBehaviour
{
    public FruitsData fruitData;
    private bool isMerged = false;

    void OnCollisionEnter(Collision collision)
    {
        bool isPlacedOnBowl = GetComponent<CheckGameOver>().InBowl;

        if (isPlacedOnBowl && !isMerged && collision.gameObject.CompareTag("Fruit"))
        {
            MergeFruit otherFruit = collision.gameObject.GetComponent<MergeFruit>();

            if (otherFruit != null && otherFruit.fruitData.level == fruitData.level && !otherFruit.isMerged)
            {
                MergeFruits(collision.gameObject);
            }
        }
    }

    private void MergeFruits(GameObject otherFruit)
    {
        Managers.ScoreManager.OnFruitMerged(fruitData);
        isMerged = true;
        MergeFruit otherFruitScript = otherFruit.GetComponent<MergeFruit>();
        if (otherFruitScript != null)
        {
            otherFruitScript.isMerged = true; // 다른 과일도 합쳐졌음을 표시
        }

        Vector3 mergePosition = gameObject.transform.position; // 합쳐질 위치
        Destroy(otherFruit);
        Destroy(gameObject);

        if (Managers.Data.fruits.Length > fruitData.level)
        {
            FruitsData newFruitData = Managers.Data.fruits[fruitData.level];

            if (newFruitData != null)
            {
                Managers.FruitsManager.InstantiateFruit(newFruitData, mergePosition, true);
            }
        }
    }
}