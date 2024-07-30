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
        BestScore,
        Item1Text,
        Item2Text
    }

    // ��ư
    enum Buttons
    {
    }

    enum Images
    {
        NextFruitImage,
        // Item slot
        Item1,
        Item2,
        // Passive slot
        Revive
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

/*        GetImage((int)Images.Item1).gameObject.BindEvent(OnClickItemButton);
        GetImage((int)Images.Item2).gameObject.BindEvent(OnClickItemButton);
*/
        Managers.FruitRandomSpawnManager.OnChangeRandomEvent += UpdateNextFruitImage;
        Managers.FruitRandomSpawnManager.Init();

        Managers.ScoreManager.OnScoreUpdated += UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated += UpdateComboUI;
        Managers.ScoreManager.OnComboEnded += HideComboUI;
        Managers.ItemManager.OnRevivalUseEvent += UpdateRevivalUI;

        GetText((int)Texts.ScorePlusText).gameObject.SetActive(false);
        GetObject((int)GameObjects.ComboUI).gameObject.SetActive(false);

        // ������ ������������ ����
        scoreDataList = Managers.Data.score;
        scoreDataList.Sort();

        if (scoreDataList != null && scoreDataList.Count > 0)
        {
            GetText((int)Texts.BestScore).text = $"{scoreDataList[0].score}";
        }

        return true;
    }

    private void OnDisable()
    {

        Managers.FruitRandomSpawnManager.OnChangeRandomEvent -= UpdateNextFruitImage;

        Managers.ScoreManager.OnScoreUpdated -= UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated -= UpdateComboUI;
        Managers.ScoreManager.OnComboEnded -= HideComboUI;

        Managers.ItemManager.OnRevivalUseEvent -= UpdateRevivalUI;

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

        if (scoreDataList != null && scoreDataList.Count > 0)
        {
            float finalBestScore = score >= scoreDataList[0].score ? score : scoreDataList[0].score;
            GetText((int)Texts.BestScore).text = $"{finalBestScore}";
        }
        else
        {
            GetText((int)Texts.BestScore).text = $"{score}";
        }

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

    public void UpdateItemSlotUI(int slotIndex, bool isActive)
    {
        switch (slotIndex)
        {
            case 0:
                GetImage((int)Images.Revive).gameObject.SetActive(isActive);
                break;
            case 1:
                GetImage((int)Images.Item1).gameObject.SetActive(isActive);
                break;
            case 2:
                GetImage((int)Images.Item2).gameObject.SetActive(isActive);
                break;
        }
    }

    public void UpdateRevivalUI(bool isRevival)
    {
        GetImage((int)Images.Revive).gameObject.SetActive(isRevival);
    }
}   