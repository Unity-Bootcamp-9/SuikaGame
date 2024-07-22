using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitRandomSpawn : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    [SerializeField] Transform fruitsSpawnPosition;
    [SerializeField] Transform nextFruitsPosition;

    private Stack<int> randomIndex = new Stack<int>();
    private GameObject currentFruit;
    private GameObject nextFruit;
    private GameObject nextFruitImageObject;  // NextFruit �̹����� ǥ���� ������Ʈ
    private Image nextFruitImage;

    private bool isSwipe = false;

    private void Start()
    {
        MakeRandomIndex();
        SpawnFruits();

        // �������� �̺�Ʈ ����
        _swipeEventAsset.eventRaised += OnSwipeEvent;
    }

    private void OnDestroy()
    {
        // �������� �̺�Ʈ ���� ����
        _swipeEventAsset.eventRaised -= OnSwipeEvent;
    }

    void MakeRandomIndex()
    {
        randomIndex.Clear();
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
    }

    void SpawnFruits()
    {
        if (currentFruit != null)
        {
            // ���� ������ �������� �и�
            currentFruit.transform.SetParent(null, true);
        }

        // ���� ������ ���� ���Ϸ� ����
        currentFruit = nextFruit;
        if (currentFruit != null)
        {
            currentFruit.transform.SetParent(Camera.main.transform, false);
            currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // ��ġ �ʱ�ȭ
            currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���ο� ���� ���� ����
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], nextFruitsPosition.position);
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(Camera.main.transform, false); // MainCamera�� �ڽ����� ����
            nextFruit.transform.localPosition = new Vector3(0.005f, -0.6f, 1.6f); // ��ġ �ʱ�ȭ
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // ���ο� ���� �ε��� �߰�
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));

        // ���� ���� �̹��� ������Ʈ
        SetNextFruitImage(Managers.Data.fruits[randomIndex.Peek()].name);
    }

    void SetNextFruitImage(string fruitName)
    {
        // ���� �̹����� ������ ����
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

    GameObject InstantiateFruit(FruitsData fruitsData, Vector3 position)
    {
        GameObject fruitPrefab = Resources.Load<GameObject>(fruitsData.path);
        if (fruitPrefab != null)
        {
            GameObject fruitInstance = Instantiate(fruitPrefab, position, Quaternion.identity);
            fruitInstance.transform.localPosition = Vector3.zero; // ��ġ �ʱ�ȭ
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"������ ����: {fruitsData.path}");
            return null;
        }
    }

    // �������� �̺�Ʈ �ڵ鷯
    public void OnSwipeEvent(object sender, SwipeData args)
    {
        if (!isSwipe)
        {
            // �������� �̺�Ʈ �߻� �� ���� ������Ʈ
            SpawnFruits();
            currentFruit.GetComponent<CapsuleCollider>().enabled = true;
            isSwipe = true;
        }
    }

    void Update()
    {
        // ���������� �Ϸ�� �� �ٽ� ���� �� �ֵ��� ���� �ʱ�ȭ
        if (isSwipe)
        {
            isSwipe = false;            
        }
    }
}
