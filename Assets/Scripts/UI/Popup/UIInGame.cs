using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIInGame : UIPopup
{
    // 이벤트 변경, 할당할 요소 //
    // 텍스트
    enum Texts
    {
        Score,
        ScorePlusText,
        ComboCount,
        ComboMultiText,
        BestScore,
        Item1Text,
        Item2Text,
        Timer
    }

    // 버튼
    enum Buttons
    {
        Back,
        Pause,
        Replace
    }

    enum Images
    {
        NextFruitImage,
        // Item slot
        Item1,
        Item2,
        Item1Background,
        Item2Background,
        // Passive slot
        Revive,
        // Pause Menu
        Pause,
        TimerImage
    }

    enum GameObjects
    { 
        ComboUI
    }

    List<ScoreData> scoreDataList;
    bool isPause;

    float timer = 180f;
    bool isTimerRunning = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetImage((int)Images.Item1).gameObject.BindEvent(() => OnClickItemButton(0));
        GetImage((int)Images.Item2).gameObject.BindEvent(() => OnClickItemButton(1));
        GetImage((int)Images.Pause).gameObject.BindEvent(() => OnClickPauseButton());

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


        if (Managers.GameManager.timeAttackMode == true)
        {
            GetImage((int)Images.TimerImage).gameObject.SetActive(true);
            GetText((int)Texts.Timer).gameObject.SetActive(true);
            isTimerRunning = true;  // 타이머 시작
        }
        else
        {
            GetImage((int)Images.TimerImage).gameObject.SetActive(false);
            GetText((int)Texts.Timer).gameObject.SetActive(false);
        }

        if (Managers.ScoreManager.LoadScore() == true)
        {
            // 점수를 내림차순으로 정렬
            scoreDataList = Managers.ScoreManager.scoreList;
            scoreDataList.Sort();

            if (scoreDataList != null && scoreDataList.Count > 0)
            {
                GetText((int)Texts.BestScore).text = $"{scoreDataList[0].score}";
            }
        }

        return true;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime; // 매 프레임마다 타이머 감소
            UpdateTimerUI();

            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false;
                Managers.GameManager.EnableGameOverDialog(); // 타이머가 0이 되면 게임 오버 호출
            }
        }
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

    private void UpdateTimerUI()
    {
        // 타이머 갱신 및 3분 카운트 다운

        // 3분 ~ 0분까지 시간 감소
        // 0분 도달 시 게임 오버 호출
        TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
        GetText((int)Texts.Timer).text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
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

    private Dictionary<Define.Item, string> itemNames = new Dictionary<Define.Item, string>
    {
        { Define.Item.LevelUp, "레벨 업" },
        { Define.Item.Delete, "제거" },
        // 추가적인 아이템을 여기에 매핑
    };

    public void UpdateItemSlotUI(int slotIndex)
    {
        // 현재 슬롯의 아이템
        int selectedItem = Managers.ItemManager.slot[slotIndex];

        if (selectedItem > -1)
        {
            // 이미지 스프라이트 변경 및 활성화
            if (slotIndex == 0)
            {
                GetImage((int)Images.Item1).gameObject.SetActive(true);
                GetText((int)Texts.Item1Text).gameObject.SetActive(true);
                GetImage((int)Images.Item1Background).sprite = Managers.Resource.Load<Sprite>($"Images/UI/ItemSlot");
                Sprite itemSprite = Managers.Resource.Load<Sprite>($"Images/Items/{((Define.Item)selectedItem)}");
                GetImage((int)Images.Item1).sprite = itemSprite;
                GetText((int)Texts.Item1Text).text = $"{itemNames[(Define.Item)selectedItem]}";
            }
            else if (slotIndex == 1)
            {
                GetImage((int)Images.Item2).gameObject.SetActive(true);
                GetText((int)Texts.Item2Text).gameObject.SetActive(true);
                GetImage((int)Images.Item2Background).sprite = Managers.Resource.Load<Sprite>($"Images/UI/ItemSlot");
                Sprite itemSprite = Managers.Resource.Load<Sprite>($"Images/Items/{((Define.Item)selectedItem)}");
                GetImage((int)Images.Item2).sprite = itemSprite;
                GetText((int)Texts.Item2Text).text = $"{itemNames[(Define.Item)selectedItem]}";
            }
        }
        else
        {
            GetImage((int)Images.Item1).GetComponent<UIEventHandler>().enabled = true;
            GetImage((int)Images.Item2).GetComponent<UIEventHandler>().enabled = true;
            // 이미지 비활성화
            if (slotIndex == 0)
            {
                GetImage((int)Images.Item1).gameObject.SetActive(false);
                GetImage((int)Images.Item1Background).sprite = Managers.Resource.Load<Sprite>($"Images/UI/ItemSlot");
                GetText((int)Texts.Item1Text).text = "";
            }
            else if (slotIndex == 1)
            {
                GetImage((int)Images.Item2).gameObject.SetActive(false);
                GetImage((int)Images.Item2Background).sprite = Managers.Resource.Load<Sprite>($"Images/UI/ItemSlot");
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
        Managers.ItemManager.UseItem(index);
        
        GetImage((int)Images.Item1).GetComponent<UIEventHandler>().enabled = false;
        GetImage((int)Images.Item2).GetComponent<UIEventHandler>().enabled = false;
        
        if (index == 0)
        {
            GetImage((int)Images.Item1Background).sprite = Managers.Resource.Load<Sprite>($"Images/UI/ItemSlotUsing");
        }
        else if (index == 1)
        {
            GetImage((int)Images.Item2Background).sprite = Managers.Resource.Load<Sprite>($"Images/UI/ItemSlotUsing");
        }
    }

    private void OnClickPauseButton()
    {
        Managers.UI.ShowPopupUI<UIPauseMenu>();
    }
}