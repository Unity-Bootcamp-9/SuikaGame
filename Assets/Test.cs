using CsvHelper;
using System.Globalization;
using System.IO;
using UnityEngine;

public class Foo
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 우리 텍스트 파일의 경로는
        // Assets/Resources/Data.csv

        TextAsset csvFile = Resources.Load<TextAsset>("Data");
        using (var reader = new StringReader(csvFile.text))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Foo>();

            foreach (var record in records)
            {
                Debug.Log($"{record.id}, {record.name}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
