using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreBoard : UIPopup
{
    enum Texts
    {
        TitleText, // 게임 오버
        CurrentText, // 현재 점수
        Number, // 오름 차순 번호
        //UserName, // 사용자 이름 -> 로컬 환경에서는 필요 없어서 우선 주석 처리함
        Score, // 내림 차순으로 정렬된 역대 점수
        MainButtonText, // 메인 버튼 텍스트
        RestartButtonText // 재시작 버튼 텍스트
    }

    enum Buttons
    {
        MainButton,
        RestartButton
    }
   
    enum GameObjects
    {
        Content
    }

    string _title;
    string _currentScore;
    string _mainButton;
    string _restartButton;
    bool _isGameOver;
    GameObject _scoreLine;
   

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.MainButton).gameObject.BindEvent(OnClickMainButton);
        GetButton((int)Buttons.RestartButton).gameObject.BindEvent(OnClickRestartButton);

        GetText((int)Texts.TitleText).text = _title;
        GetText((int)Texts.CurrentText).text = _currentScore;
        GetText((int)Texts.MainButtonText).text = _mainButton;
        GetText((int)Texts.RestartButtonText).text = _restartButton;

        if (_isGameOver)
        {
            // 버튼 비활성화
            GetButton((int)Buttons.RestartButton).gameObject.SetActive(false);
        }

        _scoreLine = Managers.Resource.Load<GameObject>("Prefabs/UI/ScoreLine");

        DisplayScores();

        return true;
    }

    public void SetBoardDialog(Action onClickMainButton, Action? onClickRestartButton, string title, string currentScore, string mainButton, string restartButton, bool isGameOver)
    {
        _onClickMainButton = onClickMainButton;
        _onClickRestartButton = onClickRestartButton;
        _title = title;
        _currentScore = currentScore.ToString(); // null 이면 restartButton 비활성화
        _mainButton = mainButton.ToString();
        _restartButton = restartButton.ToString();
        _isGameOver = isGameOver;
    }

    Action _onClickMainButton;
    void OnClickMainButton()
    {
        Managers.UI.ClosePopupUI(this);
        if (_onClickMainButton != null)
            _onClickMainButton.Invoke();
    }

    Action _onClickRestartButton;
    void OnClickRestartButton()
    {
        Managers.UI.ClosePopupUI(this);
        if ( _onClickRestartButton != null )
        {
            _onClickRestartButton.Invoke();
        }
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }

    public void DisplayScores()
    {
        // 점수를 내림차순으로 정렬
        List<ScoreData> scoreDataList = Managers.Data.score;
        scoreDataList.Sort();

        // 새로운 ScoreLine 오브젝트 생성 및 점수 표시
        for (int i = 0; i < scoreDataList.Count; ++i)
        {
            GameObject scoreLineInstance = Instantiate(_scoreLine, GetObject((int)GameObjects.Content).transform);
            TextMeshProUGUI[] texts = scoreLineInstance.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI text in texts)
            {
                if (text.name == "Number")
                {
                    text.text = $"{i + 1}";
                }

                if (text.name == "Score")
                {
                    text.text = scoreDataList[i].score.ToString();
                }
            }
        }
    }
}