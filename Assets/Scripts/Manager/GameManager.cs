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
                "점수",
                $"{Managers.ScoreManager.Score}",
                "메인 메뉴",
                "재시작",
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
                "점수",
                "", // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
                "메인 메뉴",
                "제시작",
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
