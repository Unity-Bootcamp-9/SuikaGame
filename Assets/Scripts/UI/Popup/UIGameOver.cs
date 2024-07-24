using System;
using UnityEngine.SceneManagement;

public class UIGameOver : UIPopup
{
    enum Texts
    {
        TitleText,
        BestScoreText,
        CurrentText,
        ConfirmText
    }

    enum Buttons
    {
        Confirm
    }

    string _title;
    string _best;
    string _score;
    string _confirm;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Confirm).gameObject.BindEvent(OnClickYesButton);

        GetText((int)Texts.TitleText).text = _title;
        GetText((int)Texts.BestScoreText).text = _best;
        GetText((int)Texts.CurrentText).text = _score;
        GetText((int)Texts.ConfirmText).text = _confirm;

        return true;
    }

    public void SetDialog(Action onClickYesButton, string title, string best, string score, string confirm)
    {
        _onClickYesButton = onClickYesButton;
        _title = title;
        _best = best;
        _score = score;
        _confirm = confirm;
    }

    Action _onClickYesButton;
    void OnClickYesButton()
    {
        Managers.UI.ClosePopupUI(this);
        if (_onClickYesButton != null)
            _onClickYesButton.Invoke();
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
