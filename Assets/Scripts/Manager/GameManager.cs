using UnityEngine;
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
