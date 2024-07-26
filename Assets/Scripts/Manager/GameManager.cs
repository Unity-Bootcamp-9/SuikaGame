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
                1, // Json���� ������ ó���� Num ���� �������� ����
                $"{Managers.ScoreManager.Score}",
                Managers.ScoreManager.Score, // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
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
                1, // Json���� ������ ó���� Num ���� �������� ����
                $"",
                Managers.ScoreManager.Score, // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
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
