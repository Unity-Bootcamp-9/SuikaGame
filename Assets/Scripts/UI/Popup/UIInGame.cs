using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInGame : UIPopup
{
    // �̺�Ʈ ����, �Ҵ��� ��� //
    // �ؽ�Ʈ
    enum Texts
    {
        ScoreText,
        ScorePlusText,
        ComboText,
        ComboMultiText
    }

    // ��ư
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
        // �̹��� ������Ʈ
        Sprite sprite = Resources.Load<Sprite>($"Images/Fruits/{fruitName}");
        if (sprite != null)
        {
            GetImage((int)Images.NextFruitImage).sprite = sprite;
        }
        else
        {
            Debug.Log($"��������Ʈ ���� : Images/Fruits/{fruitName}");
        }
    }

    private void UpdateScoreUI(float score, float scorePlus)
    {
        if (GetText((int)Texts.ScoreText) != null)
        {
            GetText((int)Texts.ScoreText).text = $"����: {score}";
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
            GetText((int)Texts.ComboText).text = $"{comboCount} ����";
        }

        if (GetText((int)Texts.ComboMultiText) != null)
        {
            GetText((int)Texts.ComboMultiText).text = $"+{scoreMultiplier}";
        }

        // �޺�Ÿ�̸� ����
        // �ڷ�ƾ���� ����
        // if (�ڷ�ƾ �ߺ� ���� ���� ���� ó��)
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