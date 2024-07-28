using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreBoard : UIPopup
{
    enum Texts
    {
        TitleText, // ���� ����
        CurrentText, // ���� ����
        Number, // ���� ���� ��ȣ
        //UserName, // ����� �̸� -> ���� ȯ�濡���� �ʿ� ��� �켱 �ּ� ó����
        Score, // ���� �������� ���ĵ� ���� ����
        MainButtonText, // ���� ��ư �ؽ�Ʈ
        RestartButtonText // ����� ��ư �ؽ�Ʈ
    }

    enum Buttons
    {
        MainButton,
        RestartButton
    }

    enum GameObjects
    {
        ScoreLine,
        Content
    }

    string _title;
    string _number;
    string _currentScore;
    string _score;
    string _mainButton;
    string _restartButton;
    bool _isGameOver;
    GameObject _scoreLine;
    Transform _content;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.MainButton).gameObject.BindEvent(OnClickMainButton);
        GetButton((int)Buttons.RestartButton).gameObject.BindEvent(OnClickRestartButton);

        GetText((int)Texts.TitleText).text = _title;
        GetText((int)Texts.Number).text = _number;
        GetText((int)Texts.CurrentText).text = _currentScore;
        GetText((int)Texts.Score).text = _score;
        GetText((int)Texts.MainButtonText).text = _mainButton;
        GetText((int)Texts.RestartButtonText).text = _restartButton;

        _scoreLine = GetObject((int)GameObjects.ScoreLine);
        _content = GetObject((int)GameObjects.Content).transform;

        if (_isGameOver)
        {
            // ��ư ��Ȱ��ȭ
            GetButton((int)Buttons.RestartButton).gameObject.SetActive(false);
        }

        return true;
    }

    public void SetBoardDialog(Action onClickMainButton, Action? onClickRestartButton, string title, string currentScore, string mainButton, string restartButton, bool isGameOver)
    {
        _onClickMainButton = onClickMainButton;
        _onClickRestartButton = onClickRestartButton;
        _title = title;
        _currentScore = currentScore.ToString(); // null �̸� restartButton ��Ȱ��ȭ
        _mainButton = mainButton.ToString();
        _restartButton = restartButton.ToString();
        _isGameOver = isGameOver;
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

    public void DisplayScores(GameScoreData gameScoreData)
    {
        // ������ ������������ ����
        Array.Sort(gameScoreData.score, (x, y) => y.score.CompareTo(x.score));

        // ���ο� ScoreLine ������Ʈ ���� �� ���� ǥ��
        foreach (var scoreData in gameScoreData.score)
        {
            var scoreLineInstance = Instantiate(_scoreLine, _content);
            var texts = scoreLineInstance.GetComponentsInChildren<TextMeshPro>();

            foreach (var text in texts)
            {
                if (text.name == "Number")
                {
                    text.text = scoreData.num.ToString();
                }
                else if (text.name == "Score")
                {
                    text.text = scoreData.score;
                }
            }
        }
    }
}