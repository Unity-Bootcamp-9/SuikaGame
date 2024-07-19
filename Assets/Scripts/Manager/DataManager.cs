using CsvHelper;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager
{
    public List<FruitsData> FruitsData { get; private set; }

    public void Init()
    {
        FruitsData = LoadCsv<FruitsData>("FruitsData");
    }

    private List<T> LoadCsv<T>(string filename)
    {
        using (var reader = new StringReader(Resources.Load<TextAsset>($"CSV/{filename}").text))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {

            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }

    private Dictionary<Key, T> LoadCsv<T, Key>(string filename, string indexKey) 
    {
        Dictionary<Key, T> result = new();
        using (var reader = new StringReader(Resources.Load<TextAsset>($"CSV/{filename}").text))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<T>().ToList();
            
            foreach (var record in records)
            {
                var keyProperty = typeof(T).GetProperty(indexKey);
                if (keyProperty == null)
                {
                    continue;
                }

                var key = (Key)keyProperty.GetValue(record);
                var value = record;

                if (!result.ContainsKey(key))
                {
                    result.Add(key, value);
                }
                else
                {
                    Debug.Log("¾øÀ½");
                }
            }
        }

        return result;
    }
}
