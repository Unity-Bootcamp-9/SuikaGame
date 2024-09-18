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
        SceneManager.LoadScene("InGame");

        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }

    private void OnClickAttackMode()
    {
        SceneManager.LoadScene("InGame");

        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();

        timeAttackMode = true;
        // Ÿ�Ӿ����϶� �Ұ����� UIIngame���� true���� Ȯ���ϰ�
        // Ÿ�̸Ӹ� �ʱ�ȭ �ϴ� ������ �ʿ��ҵ�
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