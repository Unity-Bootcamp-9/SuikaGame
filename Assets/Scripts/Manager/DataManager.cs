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
    public List<FruitsData> fruits { get; private set; }
    // NOTE : ���������̺� �߰��ؾ���.
    // NOTE : ������ �ĺ����� ��� List��, ������ �ĺ����� ��� Dictionary ���.
    // NOTE : Id�� ���������� �����θ� ������ ���� ����
    //private Dictionary<int, TestData> _testDataTable;
    //private List<TestData2> _testData2Table;

    public void Init()
    {
        fruits = ParseToList<FruitsData>("FruitsData");

        foreach (FruitsData fruits in fruits)
        {
            Debug.Log(fruits.name);
        }

        //_testDataTable = ParseToDict<int, TestData>("Da ta/TestData", data => data.Id);
        //_testData2Table = ParseToList<TestData2>("Data/TestData2");
    }

    public List<T> ParseToList<T>([NotNull] string path)
    {
        using (var reader = new StringReader(Resources.Load<TextAsset>($"CSV/{path}").text))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
            }
        }
    }

    public Dictionary<Key, Item> ParseToDict<Key, Item>([NotNull] string path, Func<Item, Key> keySelector)
    {
        using (var reader = new StringReader(Resources.Load<TextAsset>($"CSV/{path}").text))
        {
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Item>().ToDictionary(keySelector);
            }
        }
    }
}