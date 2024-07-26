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
            Managers.UI.ShowPopupUI<UIScoreBoard>().SetBoardDialog(
                LoadMainMenu,
                ReloadGameScene,
                "Score",
                1, // Json으로 데이터 처리시 Num 내림 차순으로 수정
                $"{Managers.ScoreManager.Score}",
                Managers.ScoreManager.Score, // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
                "MainMenu",
                "ReStart",
                false
                );
        }
    }

    public void EnableMainDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;
            Managers.UI.ShowPopupUI<UIScoreBoard>().SetBoardDialog(
                LoadMainMenu,
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
    }

    private void LoadMainMenu()
    {
        Managers.ScoreManager.ResetAll();
        isGameOverDialogEnabled = false;
        SceneManager.LoadScene("Main");
        Managers.UI.ShowPopupUI<UIMain>();
        LoaderUtility.Deinitialize();
    }

    private void ReloadGameScene()
    {
        Managers.ScoreManager.ResetAll();
        isGameOverDialogEnabled = false;
        SceneManager.LoadScene(0);
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }
}
