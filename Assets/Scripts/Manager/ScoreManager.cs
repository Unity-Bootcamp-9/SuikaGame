using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class ScoreManager
{
    private int comboCount = 1;
    private float comboTimer = 0f;
    private float comboDuration = 3f;
    private float comboMulti = 1.1f;
    private float score = 0;

    private Coroutine comboCoroutine;

    public event Action<float, float> OnScoreUpdated;
    public event Action<int, float> OnComboUpdated;
    public event Action OnComboEnded;

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
                comboMulti += 0.1f;
            }
        }
        else
        {
            comboCount = 1;
            comboMulti = 1.1f;
        }
        comboTimer = comboDuration;
        Debug.Log($"점수 멀티플 : {comboMulti}");

        if (comboCoroutine != null)
        {
            Managers.Instance.StopCoroutine(comboCoroutine);
        }

        comboCoroutine = Managers.Instance.StartCoroutine(ComboTimer());

        OnComboUpdated?.Invoke(comboCount, comboMulti);
    }

    private void UpdateScore(FruitsData fruitData)
    {
        int points = fruitData.level; // level을 점수로 사용
        float scorePlus = Mathf.CeilToInt(points * comboMulti);
        score += scorePlus;
        Debug.Log($"점수 : {score}");

        OnScoreUpdated?.Invoke(score, scorePlus);
    }

    private IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(comboDuration);
        comboCount = 0;
        comboMulti = 1.1f;
        comboTimer = 0f;

        OnComboEnded?.Invoke();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        comboMulti = 1.1f;
        comboTimer = 0f;

        //if ( 리셋 여부 확인)
        OnComboUpdated?.Invoke(comboCount, comboMulti);
    }
}
