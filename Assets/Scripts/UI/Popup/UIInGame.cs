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
        Score,
        ScorePlusText,
        ComboCount,
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

    enum GameObjects
    { 
        ComboUI
    }

    private FruitRandomSpawnManager fruitRandomSpawnManager;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        BindObject(typeof(GameObjects));

        fruitRandomSpawnManager = FindObjectOfType<FruitRandomSpawnManager>();

        if (fruitRandomSpawnManager != null)
        {
            fruitRandomSpawnManager.OnChangeRandomEvent += UpdateNextFruitImage;
        }

        Managers.ScoreManager.OnScoreUpdated += UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated += UpdateComboUI;
        Managers.ScoreManager.OnComboEnded += HideComboUI;

        GetText((int)Texts.ScorePlusText).gameObject.SetActive(false);
        GetObject((int)GameObjects.ComboUI).gameObject.SetActive(false);

        return true;
    }

    private void OnDisable()
    {
        if (fruitRandomSpawnManager != null)
        {
            fruitRandomSpawnManager.OnChangeRandomEvent -= UpdateNextFruitImage;
        }

        Managers.ScoreManager.OnScoreUpdated -= UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated -= UpdateComboUI;
        Managers.ScoreManager.OnComboEnded -= HideComboUI;
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
        GetText((int)Texts.Score).text = $"{score}";

        GetText((int)Texts.ScorePlusText).gameObject.SetActive(true);
        GetText((int)Texts.ScorePlusText).text = $"+{scorePlus}";
    }

    private void UpdateComboUI(int comboCount, float scoreMultiplier)
    {
        GetText((int)Texts.ComboMultiText).gameObject.SetActive(true);
        GetText((int)Texts.ComboMultiText).text = $"+{scoreMultiplier}";

        GetObject((int)GameObjects.ComboUI).gameObject.SetActive(true);

        GetText((int)Texts.ComboCount).gameObject.SetActive(true);
        GetText((int)Texts.ComboCount).text = $"{comboCount}";
    }

    private void HideComboUI()
    {
        GetText((int)Texts.ScorePlusText).gameObject.SetActive(false);

        GetObject((int)GameObjects.ComboUI).gameObject.SetActive(false);
    }

    private IEnumerator ResetComboCoroutine()
    {
        yield return new WaitForSeconds(3f);
        HideComboUI();
    }
}   