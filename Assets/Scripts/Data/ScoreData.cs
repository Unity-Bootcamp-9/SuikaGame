using System;
using System.Collections.Generic;

[Serializable]
public class ScoreData
{
    public int score;
}

[Serializable]
public class GameScoreData
{
    public List<ScoreData> score;
}