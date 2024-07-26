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

        GetText((int)Texts.StartText).text = "����";
        GetText((int)Texts.ScoreBoardText).text = "���";

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
            "����",
            1, // Json���� ������ ó���� Num ���� �������� ����
            $"",
            Managers.ScoreManager.Score, // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
            "���� �޴�",
            "�����",
            true
            );
    }

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
