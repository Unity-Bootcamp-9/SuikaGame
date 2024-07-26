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
        Managers.UI.ShowPopupUI<UIScoreBoard>().SetBoardDialog(
            () => { 
                Managers.UI.ClosePopupUI(this);
                Managers.UI.ShowPopupUI<UIMain>();
            },
            null,
            "점수",
            1, // Json으로 데이터 처리시 Num 내림 차순으로 수정
            $"",
            Managers.ScoreManager.Score, // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
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
