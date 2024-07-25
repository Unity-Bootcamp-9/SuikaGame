using System;
using UnityEngine.SceneManagement;

public class UIScoreBoard : UIPopup
{
    enum Texts
    {
        TitleText, // ���� ����
        CurrentText, // ���� ����
        Number, // ���� ���� ��ȣ
        //UserName, // ����� �̸� -> ���� ȯ�濡���� �ʿ� ��� �켱 �ּ� ó����
        Score, // ���� �������� ���ĵ� ���� ����
        MainConfirmText, // ���� ��ư �ؽ�Ʈ
        RestartConfirmText // ����� ��ư �ؽ�Ʈ
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
