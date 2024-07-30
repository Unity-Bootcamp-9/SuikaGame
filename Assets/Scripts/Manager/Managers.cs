using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get; private set; }

    private static DataManager dataManager = new();
    private static UIManager uiManager = new();
    private static ResourceManager resourceManager = new();
    private static FruitRandomSpawnManager fruitRandomSpawnManager = new();
    private static ScoreManager scoreManager = new();
    private static FruitsManager fruitsManager = new();
    private static GameManager gameManager = new();
    private static SoundManager soundManager = new();
    private static ItemManager itemManager = new();

    public static DataManager Data { get { Init(); return dataManager; } }
    public static UIManager UI { get { Init(); return uiManager; } }
    public static ResourceManager Resource { get { Init(); return resourceManager; } }
    public static FruitRandomSpawnManager FruitRandomSpawnManager { get { Init(); return fruitRandomSpawnManager; } }
    public static ScoreManager ScoreManager { get { Init(); return scoreManager; } }
    public static FruitsManager FruitsManager { get { Init(); return fruitsManager; } }
    public static GameManager GameManager { get { Init(); return gameManager; } }
    public static SoundManager SoundManager { get { Init(); return soundManager; } }
    public static ItemManager ItemManager { get { Init(); return itemManager; } }

    void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            Instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);

            dataManager.Init();
            soundManager.Init();
            itemManager.Init();

            Application.targetFrameRate = 60;
        }
    }
}
