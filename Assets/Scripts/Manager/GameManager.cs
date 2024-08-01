using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class GameManager
{
    public bool isGameOverDialogEnabled = false;
    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;

    public void EnableGameOverDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;

            Managers.ScoreManager.SaveScore();

            OnGameOverEvent?.Invoke();
            var uiScoreData = Managers.UI.ShowPopupUI<UIScoreBoard>();

            uiScoreData.SetBoardDialog(
                LoadMainMenu,
                ReloadGameScene,
                "점수",
                $"{Managers.ScoreManager.Score}",
                "메인 메뉴",
                "재시작",
                false
                );
        }
    }

    public void EnableMainDialog()
    {
        if (isGameOverDialogEnabled == false)
        {
            isGameOverDialogEnabled = true;
            var uiScoreData = Managers.UI.ShowPopupUI<UIScoreBoard>();

            Managers.UI.ShowPopupUI<UIScoreBoard>().SetBoardDialog(
                LoadMainMenu,
                null,
                "점수",
                "", // 씬 바뀌면서 점수 초기화 되기 때문에 Json에 점수 저장 후 불러오는 로직 필요
                "메인 메뉴",
                "제시작",
                true
                );
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
