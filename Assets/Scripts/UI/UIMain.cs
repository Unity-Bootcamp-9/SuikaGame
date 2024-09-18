using System;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class UIMain : UIPopup
{
    enum Texts
    {
        StartText,
        ScoreBoardText
    }

    enum Buttons
    {
        StartButton,
        ScoreBoardButton
    }

    string _start;
    string _scoreBoard;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.ScoreBoardButton).gameObject.BindEvent(OnClickScoreBoardButton);

        GetText((int)Texts.StartText).text = "시작";
        GetText((int)Texts.ScoreBoardText).text = "기록";

        return true;
    }

    void OnClickStartButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UIModeSelect>();
    }

    void OnClickScoreBoardButton()
    {
        Managers.UI.ClosePopupUI(this);

        // UIScoreBoard 보여주기
        var uiScoreBoard = Managers.UI.ShowPopupUI<UIScoreBoard>();

        uiScoreBoard.SetBoardDialog(
            () => { 
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UIMain>();
            },
            null,
            "점수",
            "",
            "메인 메뉴",
            "재시작",
            true
            );
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
