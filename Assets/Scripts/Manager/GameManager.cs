using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager
{
    public bool isGameOverDialogEnabled = false;

    public void EnableGameOverDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;

            Managers.ScoreManager.SaveScore();

            var gameScoreData = Managers.ScoreManager.LoadScores();
            var uiScoreData = Managers.UI.ShowPopupUI<UIScoreBoard>();

            int recentNumber = gameScoreData.score.Length > 0 ? gameScoreData.score[0].num : 0;
            int recentScore = gameScoreData.score.Length > 0 ? int.Parse(gameScoreData.score[0].score) : 0;

            uiScoreData.SetBoardDialog(
                LoadMainMenu,
                ReloadGameScene,
                "����",
                $"{Managers.ScoreManager.Score}",
                "���� �޴�",
                "�����",
                false
                );
            uiScoreData.DisplayScores(gameScoreData);
        }
    }

    public void EnableMainDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;

            var gameScoreData = Managers.ScoreManager.LoadScores();
            var uiScoreData = Managers.UI.ShowPopupUI<UIScoreBoard>();

            int recentNumber = gameScoreData.score.Length > 0 ? gameScoreData.score[0].num : 0;
            int recentScore = gameScoreData.score.Length > 0 ? int.Parse(gameScoreData.score[0].score) : 0;

            Managers.UI.ShowPopupUI<UIScoreBoard>().SetBoardDialog(
                LoadMainMenu,
                null,
                "����",
                "", // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
                "���� �޴�",
                "������",
                true
                );
            uiScoreData.DisplayScores(gameScoreData);
        }
    }

    private void LoadMainMenu()
    {
        Managers.ScoreManager.ResetAll();
        isGameOverDialogEnabled = false;
        SceneManager.LoadScene(0);
        Managers.UI.ShowPopupUI<UIMain>();
        LoaderUtility.Deinitialize();
    }

    private void ReloadGameScene()
    {
        Managers.ScoreManager.ResetAll();
        isGameOverDialogEnabled = false;
        SceneManager.LoadScene(1);
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }
}
