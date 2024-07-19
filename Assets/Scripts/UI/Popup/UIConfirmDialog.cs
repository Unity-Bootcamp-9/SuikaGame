using System;

public class UIConfirmDialog : UIPopup
{
    enum Texts
    {
        DialogTitle,
        DialogBody
    }

    enum Buttons
    { 
        Confirm
    }

    string _title;
    string _body;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Confirm).gameObject.BindEvent(OnClickYesButton);

        return true;
    }

    public void SetDialog(Action onClickYesButton, string title, string body)
    {
        _onClickYesButton = onClickYesButton;
        _title = title;
        _body = body;
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
