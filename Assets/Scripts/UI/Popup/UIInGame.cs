using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

        GetImage((int)Images.Item1).gameObject.BindEvent(() => OnClickItemButton(0));
        GetImage((int)Images.Item2).gameObject.BindEvent(() => OnClickItemButton(1));

        Managers.FruitRandomSpawnManager.OnChangeRandomEvent += UpdateNextFruitImage;
        Managers.FruitRandomSpawnManager.Init();

        Managers.ScoreManager.OnScoreUpdated += UpdateScoreUI;
        Managers.ScoreManager.OnComboUpdated += UpdateComboUI;
        Managers.ScoreManager.OnComboEnded += HideComboUI;

        Managers.ItemManager.OnItemSlotChangeEvent += UpdateItemSlotUI;
        Managers.ItemManager.OnRevivalToggleEvent += UpdateReviveUI;

        Managers.GameManager.OnGameOverEvent += DisableItemButtonsOnGameOver;

        GetText((int)Texts.ScorePlusText).gameObject.SetActive(false);
        GetObject((int)GameObjects.ComboUI).gameObject.SetActive(false);

        GetImage((int)Images.Item1).gameObject.SetActive(false);
        GetImage((int)Images.Item2).gameObject.SetActive(false);

        GetText((int)Texts.Item1Text).gameObject.SetActive(false);
        GetText((int)Texts.Item2Text).gameObject.SetActive(false);

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

        Managers.ItemManager.OnItemSlotChangeEvent -= UpdateItemSlotUI;
        Managers.ItemManager.OnRevivalToggleEvent -= UpdateReviveUI;

        Managers.GameManager.OnGameOverEvent -= DisableItemButtonsOnGameOver;
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
        GetText((int)Texts.ComboMultiText).text = $"x{string.Format("{0:f1}", scoreMultiplier)}";

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

    private Dictionary<Define.Item, string> itemNames = new Dictionary<Define.Item, string>
    {
        { Define.Item.LevelUp, "���� ��" },
        { Define.Item.Delete, "����" },
        // �߰����� �������� ���⿡ ����
    };

    public void UpdateItemSlotUI(int slotIndex)
    {
        // ���� ������ ������
        int selectedItem = Managers.ItemManager.slot[slotIndex];

        if (selectedItem > -1)
        {
            // �̹��� ��������Ʈ ���� �� Ȱ��ȭ
            if (slotIndex == 0)
            {
                GetImage((int)Images.Item1).gameObject.SetActive(true);
                GetText((int)Texts.Item1Text).gameObject.SetActive(true);
                Sprite sprite = Managers.Resource.Load<Sprite>($"Images/Items/{((Define.Item)selectedItem)}");
                GetImage((int)Images.Item1).sprite = sprite;
                GetText((int)Texts.Item1Text).text = $"{itemNames[(Define.Item)selectedItem]}";
            }
            else if (slotIndex == 1)
            {
                GetImage((int)Images.Item2).gameObject.SetActive(true);
                GetText((int)Texts.Item2Text).gameObject.SetActive(true);
                Sprite sprite = Managers.Resource.Load<Sprite>($"Images/Items/{((Define.Item)selectedItem)}");
                GetImage((int)Images.Item2).sprite = sprite;
                GetText((int)Texts.Item2Text).text = $"{itemNames[(Define.Item)selectedItem]}";
            }
        }
        else
        {
            // �̹��� ��Ȱ��ȭ
            if (slotIndex == 0)
            {
                GetImage((int)Images.Item1).gameObject.SetActive(false);
                GetText((int)Texts.Item1Text).text = "";
            }
            else if (slotIndex == 1)
            {
                GetImage((int)Images.Item2).gameObject.SetActive(false);
                GetText((int)Texts.Item2Text).text = "";
            }
        }
    }

    private void DisableItemButtonsOnGameOver()
    {
        GetImage((int)Images.Item1).GetComponent<UIEventHandler>().enabled = false;
        GetImage((int)Images.Item2).GetComponent<UIEventHandler>().enabled = false;
    }

    public void UpdateReviveUI(bool isRevival)
    {
        Debug.Log(isRevival);
        string imageName = isRevival ? "Revive" : "Revive2";
        Sprite sprite = Managers.Resource.Load<Sprite>($"Images/Items/{imageName}");
        GetImage((int)Images.Revive).sprite = sprite;
    }

    private void OnClickItemButton(int index)
    {
        Managers.ItemManager.ItemUse(index);

    }
}   