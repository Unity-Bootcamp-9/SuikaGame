using System;
using UnityEngine;

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

    string _title;
    string _number;
    string _currentScore;
    string _score;
    string _mainButton;
    string _restartButton;
    bool _isGameOver;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.MainButton).gameObject.BindEvent(OnClickMainButton);
        GetButton((int)Buttons.RestartButton).gameObject.BindEvent(OnClickRestartButton);

        GetText((int)Texts.TitleText).text = _title;
        GetText((int)Texts.Number).text = _number;
        GetText((int)Texts.CurrentText).text = _currentScore;
        GetText((int)Texts.Score).text = _score;
        GetText((int)Texts.MainButtonText).text = _mainButton;
        GetText((int)Texts.RestartButtonText).text = _restartButton;

        if (_isGameOver)
        {
            // 버튼 비활성화
            GetButton((int)Buttons.RestartButton).gameObject.SetActive(false);
        }

        return true;
    }

    public void SetBoardDialog(Action onClickMainButton, Action? onClickRestartButton, string title, int number, string currentScore, int score, string mainButton, string restartButton, bool isGameOver)
    {
        _onClickMainButton = onClickMainButton;
        _onClickRestartButton = onClickRestartButton;
        _title = title;
        _number = number.ToString();
        _currentScore = currentScore.ToString(); // null 이면 restartButton 비활성화
        _score = score.ToString();
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
}