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
    public ScoreData[] score { get; private set; }

    private string filePath => Path.Combine(Application.persistentDataPath, "score");

    // NOTE : 데이터테이블 추가해야함.
    // NOTE : 느슨한 식별자의 경우 List를, 엄격한 식별자의 경우 Dictionary 사용.
    // NOTE : Id를 열거형으로 만들어두면 오류낼 일이 적음
    //private Dictionary<int, TestData> _testDataTable;
    //private List<TestData2> _testData2Table;

    public void Init()
    {
        fruits = ParseToList<FruitGameData>("fruits").fruits;
        score = LoadScores().score;
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

    public void SaveScores(GameScoreData gameScoreData)
    {
        var json = JsonUtility.ToJson(gameScoreData, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"점수 저장 : {json}");
    }

    public GameScoreData LoadScores()
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            Debug.Log($"점수 로드 : {jsonData}");
            return JsonUtility.FromJson<GameScoreData>(jsonData);
        }
        return new GameScoreData { score = new ScoreData[0] };
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