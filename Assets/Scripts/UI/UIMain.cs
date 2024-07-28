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
        SceneManager.LoadScene("InGame");

        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }

    void OnClickScoreBoardButton()
    {
        Managers.UI.ClosePopupUI(this);

        // 저장된 점수 불러오기
        var gameScoreData = Managers.ScoreManager.LoadScores();

        // 최근 점수를 불러오기
        int recentNumber = gameScoreData.score.Length > 0 ? gameScoreData.score[0].num : 0;
        int recentScore = gameScoreData.score.Length > 0 ? int.Parse(gameScoreData.score[0].score) : 0;

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
        uiScoreBoard.DisplayScores(gameScoreData);
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
