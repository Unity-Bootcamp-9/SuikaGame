using UnityEngine;

public class MergeFruit : MonoBehaviour
{
    private FruitsData fruitData;
    private ScoreManager scoreManager;
    private bool isMerged = false; // 과일이 이미 합쳐졌는지 여부

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (!isMerged && collision.gameObject.CompareTag(tag)) // 플래그 확인
        {
            MergeFruit otherFruit = collision.gameObject.GetComponent<MergeFruit>();

            if (otherFruit != null && otherFruit.fruitData == fruitData && !otherFruit.isMerged)
            {
                // 과일 합치기 로직
                MergeFruits(collision.gameObject);
                scoreManager.OnFruitMerged(fruitData);
            }
        }*/
    }

    private void MergeFruits(GameObject otherFruit)
    {
        /*isMerged = true; // 현재 과일이 합쳐졌음을 표시
        MergeFruit otherFruitScript = otherFruit.GetComponent<MergeFruit>();
        if (otherFruitScript != null)
        {
            otherFruitScript.isMerged = true; // 다른 과일도 합쳐졌음을 표시
        }

        Debug.Log($"과일 합쳐짐: {fruitType}");

        Vector3 mergePosition = transform.position; // 합쳐질 위치
        Destroy(otherFruit);
        Destroy(gameObject);

        Type newFruitType = Type.Blueberry; // 기본값

        switch (fruitType)
        {
            case Type.Blueberry:
                newFruitType = Type.Strawberry;
                break;
            case Type.Strawberry:
                newFruitType = Type.Durian;
                break;
        }

        InstantiateFruit(newFruitType, mergePosition);*/
    }

    /*private GameObject InstantiateFruit(FruitsData nextFruit, Vector3 position)
    {
        for ()
    }*/
}