using System;
using UnityEngine.SceneManagement;

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
        GetText((int)Texts.ScoreBoardText).text = "리더보드";

        return true;
    }

    void OnClickStartButton()
    {
        SceneManager.LoadScene("InGame");
    }

    void OnClickScoreBoardButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UIScoreBoard>().SetBoardDialog(
            () => { 
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UIMain>();
            },
            null,
            "Score",
            1, // Json으로 데이터 처리시 Num 내림 차순으로 수정
            $"",
            Managers.ScoreManager.Score, // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
            "MainMenu",
            "Restart",
            true
            );
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
