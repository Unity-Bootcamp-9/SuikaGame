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
                1, // Json���� ������ ó���� Num ���� �������� ����
                Managers.ScoreManager.Score, // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
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
                1, // Json���� ������ ó���� Num ���� �������� ����
                Managers.ScoreManager.Score, // ���� �ֱ� ���� ����
                Managers.ScoreManager.Score, // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
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
