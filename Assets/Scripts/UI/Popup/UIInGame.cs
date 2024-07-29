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
        Score,
        ScorePlusText,
        ComboCount,
        ComboMultiText,
        BestScore
    }

    // ��ư
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

    List<ScoreData> scoreDataList;


    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));

        BindObject(typeof(GameObjects));


        Managers.FruitRandomSpawnManager.OnChangeRandomEvent += UpdateNextFruitImage;
        Managers.FruitRandomSpawnManager.Init();

        Managers.ScoreManager.OnScoreUpdated += UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated += UpdateComboUI;
        Managers.ScoreManager.OnComboEnded += HideComboUI;

        GetText((int)Texts.ScorePlusText).gameObject.SetActive(false);
        GetObject((int)GameObjects.ComboUI).gameObject.SetActive(false);

        // ������ ������������ ����
        scoreDataList = Managers.Data.score;
        scoreDataList.Sort();

        GetText((int)Texts.BestScore).text = $"{scoreDataList[0].score}";

        return true;
    }

    private void OnDisable()
    {

        Managers.FruitRandomSpawnManager.OnChangeRandomEvent -= UpdateNextFruitImage;

        Managers.ScoreManager.OnScoreUpdated -= UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated -= UpdateComboUI;
        Managers.ScoreManager.OnComboEnded -= HideComboUI;
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
        GetText((int)Texts.Score).text = $"{score}";
        float finalBestScore = score >= scoreDataList[0].score ? score : scoreDataList[0].score;

        GetText((int)Texts.BestScore).text = $"{finalBestScore}";

        GetText((int)Texts.ScorePlusText).gameObject.SetActive(true);
        GetText((int)Texts.ScorePlusText).text = $"+{scorePlus}";
    }

    private void UpdateComboUI(int comboCount, float scoreMultiplier)
    {
        GetText((int)Texts.ComboMultiText).gameObject.SetActive(true);
        GetText((int)Texts.ComboMultiText).text = $"+{string.Format("{0:f1}", scoreMultiplier)}";

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