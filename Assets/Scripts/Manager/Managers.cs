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

    public static DataManager Data { get { Init(); return dataManager; } }
    public static UIManager UI { get { Init(); return uiManager; } }
    public static ResourceManager Resource { get { Init(); return resourceManager; } }
    public static FruitRandomSpawnManager FruitRandomSpawnManager { get {  Init(); return fruitRandomSpawnManager; } }

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

            Application.targetFrameRate = 60;
        }
    }
}
