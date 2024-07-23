using System;

[Serializable]
public class FruitsData
{
    public string name;
    public int level;
    public string path;
}

[Serializable]
public class FruitGameData
{
    public FruitsData[] fruits;
}
