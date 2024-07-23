using System;
using UnityEngine;
using TMPro;

public class ScoreManager
{
    private int comboCount = 1;
    private float comboTimer = 0f;
    private float comboDuration = 3f;
    private float scoreMultiplier = 1.1f;
    private float score = 0;

    public event Action<float, float> OnScoreUpdated;
    public event Action<int, float> OnComboUpdated;

    public void OnFruitMerged(FruitsData fruitData)
    {
        UpdateCombo();
        UpdateScore(fruitData);
    }

    private void UpdateCombo()
    {
        if (comboTimer > 0)
        {
            comboCount++;

            if (comboCount % 2 == 0)
            {
                scoreMultiplier += 0.1f;
            }
        }
        else
        {
            comboCount = 1;
            scoreMultiplier = 1.1f;
        }
        comboTimer = comboDuration;

        OnComboUpdated?.Invoke(comboCount, scoreMultiplier);
    }

    private void UpdateScore(FruitsData fruitData)
    {
        int points = fruitData.level; // level을 점수로 사용
        float scorePlus = Mathf.CeilToInt(points * scoreMultiplier);
        score += scorePlus;

        OnScoreUpdated?.Invoke(score, scorePlus);
    }

    public void ResetCombo()
    {
        comboCount = 0;
        scoreMultiplier = 1.1f;
        comboTimer = 0f;

        //if ( 리셋 여부 확인)
        OnComboUpdated?.Invoke(comboCount, scoreMultiplier);
    }
}
