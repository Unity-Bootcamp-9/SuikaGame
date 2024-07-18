using UnityEngine;

public class MergeFruit : MonoBehaviour
{
    public enum Type { Blueberry, Strawberry, Durian }
    private Type fruitType;

    private ScoreManager gameManager;
    private bool isMerged = false; // ������ �̹� ���������� ����

    void Start()
    {
        gameManager = FindObjectOfType<ScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isMerged && collision.gameObject.CompareTag(tag)) // �÷��� Ȯ��
        {
            MergeFruit otherFruit = collision.gameObject.GetComponent<MergeFruit>();

            if (otherFruit != null && otherFruit.fruitType == fruitType && !otherFruit.isMerged)
            {
                // ���� ��ġ�� ����
                MergeFruits(collision.gameObject);
                gameManager.OnFruitMerged(fruitType);
            }
        }
    }

    private void MergeFruits(GameObject otherFruit)
    {
        isMerged = true; // ���� ������ ���������� ǥ��
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

        InstantiateFruit(newFruitType, mergePosition);
    }

    private GameObject InstantiateFruit(Type newFruitType, Vector3 position)
    {
        GameObject fruitPrefab = null;
        switch (newFruitType)
        {
            case Type.Blueberry:
                fruitPrefab = Resources.Load<GameObject>("Prefabs/Blueberry");
                break;
            case Type.Strawberry:
                fruitPrefab = Resources.Load<GameObject>("Prefabs/Strawberry");
                break;
            case Type.Durian:
                fruitPrefab = Resources.Load<GameObject>("Prefabs/Durian");
                break;
        }
        return Instantiate(fruitPrefab, position, Quaternion.identity);
    }
}