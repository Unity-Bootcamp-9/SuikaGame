using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using UnityEngine;

public class DataManager
{
    public FruitsData[] fruits { get; private set; }
    public List<ScoreData> score { get; private set; }

    // NOTE : ���������̺� �߰��ؾ���.
    // NOTE : ������ �ĺ����� ��� List��, ������ �ĺ����� ��� Dictionary ���.
    // NOTE : Id�� ���������� �����θ� ������ ���� ����
    //private Dictionary<int, TestData> _testDataTable;
    //private List<TestData2> _testData2Table;

    public void Init()
    {
        fruits = ParseToList<FruitGameData>("fruits").fruits;
        score = ParseToList<GameScoreData>("score").score;
    }

    public T ParseToList<T>([NotNull] string path)
    {
        using (var reader = new StringReader(Resources.Load<TextAsset>($"Data/{path}").text))
        {
            string json = reader.ReadToEnd();
            T gameData = JsonUtility.FromJson<T>(json);

            return gameData;
        }
    }

    public void SaveData<T>(string path, T data)
    {
        /*string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path.Combine(Application.dataPath, "Resources", "Data", $"{path}"));
        Debug.Log($"���� ���� : {json}");*/

        string json = JsonUtility.ToJson(data, true);
        string fullPath = Path.Combine(Application.persistentDataPath, "Resources", "Data", path);

        // ���丮�� �������� ������ ����
        string directoryPath = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(fullPath, json);
        Debug.Log($"���� ���� : {json}");
    }

    //public Dictionary<Key, Item> ParseToDict<Key, Item>([NotNull] string path, Func<Item, Key> keySelector)
    //{
    //    using (var reader = new StringReader(Resources.Load<TextAsset>($"Data/{path}").text))
    //    {
    //        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    //        {
    //            return csv.GetRecords<Item>().ToDictionary(keySelector);
    //        }
    //    }
    //}
}