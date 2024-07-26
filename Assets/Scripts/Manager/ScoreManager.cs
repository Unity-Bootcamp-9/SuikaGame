using System;
using UnityEngine;
using System.Collections;

public class ScoreManager
{
    private int comboCount = 1;
    private float comboTimer = 0f;
    private float comboDuration = 3f;
    private float comboMulti = 1.1f;

    private float audioPitch = 1.0f;

    public int Score { get; private set; }
    public int BestScore { get; private set; }

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

        audioPitch += 0.1f;
        if (audioPitch >= 2.0)
        {
            audioPitch = 2.0f;
        }

        Managers.SoundManager.Play(Define.Sound.Effect, "Combo", 1.0f, audioPitch);

        if (comboCoroutine != null)
        {
            Managers.Instance.StopCoroutine(comboCoroutine);
        }

        comboCoroutine = Managers.Instance.StartCoroutine(ComboResetCoroutine());

        OnComboUpdated?.Invoke(comboCount, comboMulti);
    }

    private void UpdateScore(FruitsData fruitData)
    {
        int points = fruitData.level; // level을 점수로 사용
        int scorePlus = Mathf.CeilToInt(points * comboMulti);
        Score += scorePlus;
        Debug.Log($"점수 : {Score}");

        OnScoreUpdated?.Invoke(Score, scorePlus);
    }
    private IEnumerator ComboResetCoroutine()
    {
        yield return new WaitForSeconds(comboDuration);
        ResetCombo();
        OnComboEnded?.Invoke();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        comboMulti = 1.1f;
        comboTimer = 0f;
        audioPitch = 1.0f;
    }

    public void ResetAll()
    {
        ResetCombo();
        OnComboUpdated?.Invoke(comboCount, comboMulti);
        Score = 0;
        BestScore = 0;
    }
}
