using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager
{
    public bool isGameOverDialogEnabled = false;

    public void EnableMainDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;
            Managers.UI.ShowPopupUI<UIScoreBoard>().SetMainDialog(
                ReloadGameScene,
                "Score",
                1, // Json으로 데이터 처리시 Num 내림 차순으로 수정
                Managers.ScoreManager.Score, // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
                "MainMenu"
                );
        }
    }

    public void EnableGameOverDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;
            Managers.UI.ShowPopupUI<UIScoreBoard>().SetRestartDialog(
                LoadMainMenu,
                ReloadGameScene,
                "Score",
                1, // Json으로 데이터 처리시 Num 내림 차순으로 수정
                Managers.ScoreManager.Score, // 가장 최근 게임 점수
                Managers.ScoreManager.Score, // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
                "MainMenu",
                "ReStart"
                );
        }
    }

    private void LoadMainMenu()
    {
        Managers.ScoreManager.ResetAll();
        isGameOverDialogEnabled = false;
        //Managers.UI.ShowPopupUI<UIMainMenu>;
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
