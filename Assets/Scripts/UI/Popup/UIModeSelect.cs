using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class UIModeSelect : UIPopup
{
    enum Texts
    {
        NormalMode,
        TimeAttackMode,
        GoBackButton
    }

    public bool timeAttackMode = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        GetText((int)Texts.NormalMode).gameObject.BindEvent(() => OnClickNormalMode());
        GetText((int)Texts.TimeAttackMode).gameObject.BindEvent(() => OnClickAttackMode());
        GetText((int)Texts.GoBackButton).gameObject.BindEvent(() => OnClickGoBack());

        return true;
    }

    private void OnClickNormalMode()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(
            MoveToInstallBowlStep,
            "안내",
            "화면을 돌리면서 평면을 인식한 후\n바닥을 터치하여 그릇을 설치하세요.",
            "확인");
    }

    private void MoveToInstallBowlStep()
    {
        Managers.UI.ShowPopupUI<UISetPlatePosition>();
    }

    private void OnClickAttackMode()
    {
        timeAttackMode = true;
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(
            MoveToInstallBowlStep,
            "안내",
            "화면을 돌리면서 평면을 인식한 후\n바닥을 터치하여 그릇을 설치하세요.",
            "확인");
        // 타임어택일때 불값으로 UIIngame에서 true인지 확인하고
        // 타이머를 초기화 하는 로직이 필요할듯
    }

    private void OnClickGoBack()
    {
        Managers.ScoreManager.ResetAll();
        Managers.GameManager.isGameOverDialogEnabled = false;
        SceneManager.LoadScene(0);
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }
}