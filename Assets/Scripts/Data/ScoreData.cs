using System;
using System.Collections.Generic;

[Serializable]
public class ScoreData : IComparable<ScoreData>
{
    public int score;

    public int CompareTo(ScoreData next)
    {
        return next.score - score;
    }
}

[Serializable]
public class GameScoreData
{
    public List<ScoreData> score;
}