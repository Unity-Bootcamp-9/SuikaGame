using System;

public class UISetPlatePosition : UIPopup
{
    enum Texts
    {
    }

    enum Buttons
    { 
        Install
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Install).gameObject.BindEvent(EnterToInGame);

        return true;
    }

    private void EnterToInGame()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UIInGame>();
    }
}
