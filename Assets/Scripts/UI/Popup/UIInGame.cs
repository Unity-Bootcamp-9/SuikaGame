using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInGame : UIPopup
{
    // 이벤트 변경, 할당할 요소 //
    // 텍스트
    enum Texts
    {
        ScoreText,
        ScorePlusText,
        ComboText,
        ComboMultiText
    }

    // 버튼
    enum Buttons
    {
    }

    enum Images
    {
        NextFruitImage
    }

    private FruitRandomSpawnManager fruitRandomSpawnManager;
    private ScoreManager scoreManager;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        fruitRandomSpawnManager = FindObjectOfType<FruitRandomSpawnManager>();

        GetText((int)Texts.ScoreText);
        GetText((int)Texts.ScorePlusText);
        GetText((int)Texts.ComboText);
        GetText((int)Texts.ComboMultiText);

        if (fruitRandomSpawnManager != null)
        {
            fruitRandomSpawnManager.OnChangeRandomEvent += UpdateNextFruitImage;
        }

        if (scoreManager != null)
        {
            scoreManager.OnScoreUpdated += UpdateScoreUI;
            scoreManager.OnComboUpdated += UpdateComboUI;
        }

        return true;
    }

    private void OnDisable()
    {
        if (fruitRandomSpawnManager != null)
        {
            fruitRandomSpawnManager.OnChangeRandomEvent -= UpdateNextFruitImage;
        }

        if (scoreManager != null)
        {
            scoreManager.OnScoreUpdated -= UpdateScoreUI;
            scoreManager.OnComboUpdated -= UpdateComboUI;
        }
    }

    private void UpdateNextFruitImage(string fruitName)
    {
        // 이미지 업데이트
        Sprite sprite = Resources.Load<Sprite>($"Images/Fruits/{fruitName}");
        if (sprite != null)
        {
            GetImage((int)Images.NextFruitImage).sprite = sprite;
        }
        else
        {
            Debug.Log($"스프라이트 없음 : Images/Fruits/{fruitName}");
        }
    }

    private void UpdateScoreUI(float score, float scorePlus)
    {
        if (GetText((int)Texts.ScoreText) != null)
        {
            GetText((int)Texts.ScoreText).text = $"점수: {score}";
        }

        if (GetText((int)Texts.ScorePlusText) != null)
        {
            GetText((int)Texts.ScorePlusText).text = $"+{scorePlus}";
        }
    }

    private void UpdateComboUI(int comboCount, float scoreMultiplier)
    {
        if (GetText((int)Texts.ComboText) != null)
        {
            GetText((int)Texts.ComboText).text = $"{comboCount} 연쇄";
        }

        if (GetText((int)Texts.ComboMultiText) != null)
        {
            GetText((int)Texts.ComboMultiText).text = $"+{scoreMultiplier}";
        }

        // 콤보타이머 시작
        // 코루틴으로 구현
        // if (코루틴 중복 실행 방지 예외 처리)
        {
            StartCoroutine(ResetComboCoroutine());
        }
        Managers.ScoreManager.ResetCombo();
    }

    private IEnumerator ResetComboCoroutine()
    {
        yield return new WaitForSeconds(3f);
    }
}   