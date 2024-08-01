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

            if (otherFruit != null && otherFruit.GetComponent<CheckGameOver>().InBowl && !gameObject.GetComponent<CheckGameOver>().InBowl)
            {
                // �ڱ��ڽ��� true�� ����
                gameObject.GetComponent<CheckGameOver>().ToggleInBowl();
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
            otherFruitScript.isMerged = true; // �ٸ� ���ϵ� ���������� ǥ��
        }

        Vector3 mergePosition = gameObject.transform.position; // ������ ��ġ
        Destroy(otherFruit);
        Destroy(gameObject);

        if (Managers.Data.fruits.Length > fruitData.level)
        {
            FruitsData newFruitData = Managers.Data.fruits[fruitData.level];

            if (newFruitData != null)
            {
                GameObject mergedFruit = Managers.FruitsManager.InstantiateFruit(newFruitData, mergePosition, true);
                
                GameObject smokeParticle = Managers.Resource.Load<GameObject>("Particles/Cartoon FX Remaster/CFXR Prefabs/Misc/CFXR Magic Poof");
                GameObject createdSmokeParticle = Managers.Resource.Instantiate(smokeParticle);
                createdSmokeParticle.transform.position = mergedFruit.transform.position;
                createdSmokeParticle.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }
}