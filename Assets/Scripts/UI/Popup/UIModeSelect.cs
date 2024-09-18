using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
        Managers.GameManager.timeAttackMode = false; // 일반 모드 설정
        SceneManager.LoadScene("InGame");

        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }

    private void OnClickAttackMode()
    {
        Managers.GameManager.timeAttackMode = true; // 타임어택 모드 설정
        SceneManager.LoadScene("InGame");

        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();

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