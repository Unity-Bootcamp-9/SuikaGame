using UnityEngine;

public class MergeFruit : MonoBehaviour
{
    private FruitsData fruitData;
    private ScoreManager scoreManager;
    private bool isMerged = false; // ������ �̹� ���������� ����

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (!isMerged && collision.gameObject.CompareTag(tag)) // �÷��� Ȯ��
        {
            MergeFruit otherFruit = collision.gameObject.GetComponent<MergeFruit>();

            if (otherFruit != null && otherFruit.fruitData == fruitData && !otherFruit.isMerged)
            {
                // ���� ��ġ�� ����
                MergeFruits(collision.gameObject);
                scoreManager.OnFruitMerged(fruitData);
            }
        }*/
    }

    private void MergeFruits(GameObject otherFruit)
    {
        /*isMerged = true; // ���� ������ ���������� ǥ��
        MergeFruit otherFruitScript = otherFruit.GetComponent<MergeFruit>();
        if (otherFruitScript != null)
        {
            otherFruitScript.isMerged = true; // �ٸ� ���ϵ� ���������� ǥ��
        }

        Debug.Log($"���� ������: {fruitType}");

        Vector3 mergePosition = transform.position; // ������ ��ġ
        Destroy(otherFruit);
        Destroy(gameObject);

        Type newFruitType = Type.Blueberry; // �⺻��

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