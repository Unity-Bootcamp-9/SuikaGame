    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FruitRandomSpawn : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    // Managers.Data -> 작성하면 바로 사용 가능
    [SerializeField] Transform fruitsSpawnPosition;
    [SerializeField] Transform nextFruitsPosition;

    private Stack<int> randomIndex = new Stack<int>();
    private GameObject currentFruit;
    private GameObject nextFruit;
    private GameObject nextFruitImageObject;  // NextFruit 이미지를 표시할 오브젝트
    private Image nextFruitImage;

    private void Start()
    {
        MakeRandomIndex();
        SpawnFruits();

        // 스와이프 이벤트 구독
        _swipeEventAsset.eventRaised += FruitsRandomSpawn;
    }

    private void OnDestroy()
    {
        // 스와이프 이벤트 구독 해제
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
        // 현재 과일 생성 및 ThrowFruit 위치에 설정
        currentFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], GetWorldPositionFromScreen(fruitsSpawnPosition.position));
        if (currentFruit != null)
        {
            currentFruit.transform.SetParent(Camera.main.transform, false);
            currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // 위치 초기화
            currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // 다음 과일 생성 및 NextFruit 위치에 설정
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Peek()], GetWorldPositionFromScreen(nextFruitsPosition.position));
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(nextFruitsPosition, false);
            nextFruit.transform.localPosition = Vector3.zero;
        }

        // 다음 과일 이미지 생성
        SetNextFruitImage(Managers.Data.fruits[randomIndex.Peek()].name);

        // 새로운 랜덤 인덱스 추가
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
        currentFruit.transform.localPosition = new Vector3(0.02f, -0.37f, 0.9f); // 위치 초기화
        currentFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        // 새로운 nextFruit 생성
        nextFruit = InstantiateFruit(Managers.Data.fruits[randomIndex.Pop()], GetWorldPositionFromScreen(nextFruitsPosition.position));
        if (nextFruit != null)
        {
            nextFruit.transform.SetParent(nextFruitsPosition, false);
            nextFruit.transform.localPosition = Vector3.zero; // 위치 초기화 및 Z축 조정 (이미지보다 앞에)
            nextFruit.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        }

        // 다음 과일 이미지 업데이트
        SetNextFruitImage(Managers.Data.fruits[randomIndex.Peek()].name);

        // 새로운 랜덤 인덱스 추가
        randomIndex.Push(Random.Range(0, Managers.Data.fruits.Count));
    }

    void SetNextFruitImage(string fruitName)
    {
        // 기존 이미지 가 없으면 생성
        if (nextFruitImage == null)
        {
            nextFruitImageObject = new GameObject("NextFruitImage");
            nextFruitImageObject.transform.SetParent(nextFruitsPosition, false);

            // RectTransform 및 Image 컴포넌트 추가
            RectTransform rectTransform = nextFruitImageObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100); // 이미지 크기 설정
            rectTransform.localPosition = new Vector3(10, -5, 0); // 위치 초기화

            nextFruitImage = nextFruitImageObject.AddComponent<Image>();
        }

        // 이미지 업데이트
        Sprite sprite = Resources.Load<Sprite>($"Images/Fruits/{fruitName}");
        if (sprite != null)
        {
            nextFruitImage.sprite = sprite;
        }
        else
        {
            Debug.LogError($"스프라이트 없음: Images/Fruits/{fruitName}");
        }
    }


    GameObject InstantiateFruit(FruitsData fruitsData, Vector3 worldPosition)
    {
        GameObject fruitPrefab = Resources.Load<GameObject>(fruitsData.path);
        if (fruitPrefab != null)
        {
            GameObject fruitInstance = Instantiate(fruitPrefab, worldPosition, Quaternion.identity);
            fruitInstance.transform.localPosition = Vector3.zero; // 위치 초기화
            return fruitInstance;
        }
        else
        {
            Debug.LogError($"프리팹 없음: {fruitsData.path}");
            return null;
        }
    }

    Vector3 GetWorldPositionFromScreen(Vector3 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane + 1));
        worldPosition.z = 0; // Z축 위치 조정
        return worldPosition;
    }

    // 스와이프 이벤트 핸들러
    public void FruitsRandomSpawn(object sender, SwipeData args)
    {
        // 스와이프 이벤트 발생 시 과일 업데이트
        UpdateFruits();
    }    
}