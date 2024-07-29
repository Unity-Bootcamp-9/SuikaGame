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

            var uiScoreData = Managers.UI.ShowPopupUI<UIScoreBoard>();

            uiScoreData.SetBoardDialog(
                LoadMainMenu,
                ReloadGameScene,
                "����",
                $"{Managers.ScoreManager.Score}",
                "���� �޴�",
                "�����",
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
                "����",
                "", // �� �ٲ�鼭 ���� �ʱ�ȭ �Ǳ� ������ Json�� ���� ���� �� �ҷ����� ���� �ʿ�
                "���� �޴�",
                "������",
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
