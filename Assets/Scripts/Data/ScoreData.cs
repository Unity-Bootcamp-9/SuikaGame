using System;

[Serializable]
public class ScoreData
{
    public int num;
    public string score;
}

[Serializable]
public class GameScoreData
{
    public ScoreData[] score;
}