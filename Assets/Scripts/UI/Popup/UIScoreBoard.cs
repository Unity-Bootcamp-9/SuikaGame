using System;
using UnityEngine.SceneManagement;

public class UIScoreBoard : UIPopup
{
    enum Texts
    {
        TitleText, // 게임 오버
        CurrentText, // 현재 점수
        Number, // 오름 차순 번호
        //UserName, // 사용자 이름 -> 로컬 환경에서는 필요 없어서 우선 주석 처리함
        Score, // 내림 차순으로 정렬된 역대 점수
        MainConfirmText, // 메인 버튼 텍스트
        RestartConfirmText // 재시작 버튼 텍스트
    }

    enum Buttons
    {
        MainConfirm,
        RestartConfirm
    }

    string _title;
    string _number;
    string _currentScore;
    string _score;
    string _mainConfirm;
    string _restartConfirm;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.MainConfirm).gameObject.BindEvent(OnClickMainButton);
        GetButton((int)Buttons.RestartConfirm).gameObject.BindEvent(OnClickMainButton);

        GetText((int)Texts.TitleText).text = _title;
        GetText((int)Texts.Number).text = _number;
        GetText((int)Texts.CurrentText).text = _currentScore;
        GetText((int)Texts.Score).text = _score;
        GetText((int)Texts.MainConfirmText).text = _mainConfirm;
        GetText((int)Texts.RestartConfirmText).text = _restartConfirm;

        return true;
    }

    public void SetMainDialog(Action onClickMainButton, string title, int number, int score, string mainConfirm)
    {
        _onClickMainButton = onClickMainButton;
        _title = title;
        _number = number.ToString();
        _score = score.ToString();
        _mainConfirm = mainConfirm.ToString();
    }

    public void SetRestartDialog(Action onClickMainButtonAction, Action onClickRestartButton, string title, int number, int currentScore, int score, string mainConfirm, string reStartConfirm)
    {
        _onClickMainButton = onClickRestartButton;
        _onClickRestartButton = onClickRestartButton;
        _title = title;
        _number = number.ToString();
        _currentScore = currentScore.ToString();
        _score = score.ToString();
        _mainConfirm = mainConfirm.ToString();
        _restartConfirm = reStartConfirm.ToString();
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
