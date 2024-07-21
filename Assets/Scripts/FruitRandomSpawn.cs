    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitRandomSpawn : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    // Managers.Data -> �ۼ��ϸ� �ٷ� ��� ����
    [SerializeField] Transform fruitsSpawnPosition;
    [SerializeField] Transform nextFruitsPosition;

    private Stack<int> randomIndex = new Stack<int>();
    private GameObject currentFruit;
    private GameObject nextFruit;
    private GameObject nextFruitImageObject;  // NextFruit �̹����� ǥ���� ������Ʈ
    private Image nextFruitImage;

    private void Start()
    {
        MakeRandomIndex();
        SpawnFruits();

        // �������� �̺�Ʈ ����
        _swipeEventAsset.eventRaised += FruitsRandomSpawn;
    }

    private void OnDestroy()
    {
        // �������� �̺�Ʈ ���� ����
        _swipeEventAsset.eventRaised -= FruitsRandomSpawn;
    }

    void MakeRandomIndex()
    {
        randomIndex.Clear();
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
    }

    void SpawnFruits()
    {
        // ���� ���� ���� �� ThrowFruit ��ġ�� ����
        currentFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], GetWorldPositionFromScreen(fruitsSpawnPosition.position));
        if (currentFruit != null)
        {
            currentFruit.transform.SetParent(Camera.main.transform, false);
            currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // ��ġ �ʱ�ȭ
            currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���� ���� ���� �� NextFruit ��ġ�� ����
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Peek()], GetWorldPositionFromScreen(nextFruitsPosition.position));
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(nextFruitsPosition, false);
            nextFruit.transform.localPosition = Vector3.zero;
        }

        // ���� ���� �̹��� ����
        SetNextFruitImage(Managers.Data.fruits[randomIndex.Peek()].name);

        // ���ο� ���� �ε��� �߰�
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
    }

    void UpdateFruits()
    {
        if (currentFruit != null)
        {
            Destroy(currentFruit);
        }

        currentFruit = nextFruit;
        currentFruit.transform.SetParent(Camera.main.transform, false);
        currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // ��ġ �ʱ�ȭ
        currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        // ���ο� nextFruit ����
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], GetWorldPositionFromScreen(nextFruitsPosition.position));
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(nextFruitsPosition, false);
            nextFruit.transform.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ �� Z�� ���� (�̹������� �տ�)
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���� ���� �̹��� ������Ʈ
        SetNextFruitImage(Managers.Data.fruits[randomIndex.Peek()].name);

        // ���ο� ���� �ε��� �߰�
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
    }

    void SetNextFruitImage(string fruitName)
    {
        // ���� �̹��� �� ������ ����
        if (nextFruitImage == null)
        {
            nextFruitImageObject = new GameObject("NextFruitImage");
            nextFruitImageObject.transform.SetParent(nextFruitsPosition, false);

            // RectTransform �� Image ������Ʈ �߰�
            RectTransform rectTransform = nextFruitImageObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100); // �̹��� ũ�� ����
            rectTransform.localPosition = new Vector3(10, -5, 0); // ��ġ �ʱ�ȭ

            nextFruitImage = nextFruitImageObject.AddComponent<Image>();
        }

        // �̹��� ������Ʈ
        Sprite sprite = Resources.Load<Sprite>($"Images/Fruits/{fruitName}");
        if (sprite != null)
        {
            nextFruitImage.sprite = sprite;
        }
        else
        {
            Debug.LogError($"��������Ʈ ����: Images/Fruits/{fruitName}");
        }
    }


    GameObject InstantiateFruit(FruitsData fruitsData, Vector3 worldPosition)
    {
        GameObject fruitPrefab = Resources.Load<GameObject>(fruitsData.path);
        if (fruitPrefab != null)
        {
            GameObject fruitInstance = Instantiate(fruitPrefab, worldPosition, Quaternion.identity);
            fruitInstance.transform.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"������ ����: {fruitsData.path}");
            return null;
        }
    }

    Vector3 GetWorldPositionFromScreen(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane + 1));
        worldPosition.z = 0; // Z�� ��ġ ����
        return worldPosition;
    }

    // �������� �̺�Ʈ �ڵ鷯
    public void FruitsRandomSpawn(object sender, SwipeData args)
    {
        // �������� �̺�Ʈ �߻� �� ���� ������Ʈ
        UpdateFruits();
    }    
}