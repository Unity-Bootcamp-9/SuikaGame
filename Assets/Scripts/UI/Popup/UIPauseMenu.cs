using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class UIPauseMenu : UIPopup
{
    enum Texts
    {
        BackToGame,
        ReplaceBowl,
        GoBack
    }

    enum Buttons
    {
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        GetText((int)Texts.BackToGame).gameObject.BindEvent(() => OnClickBackToGameButton());
        GetText((int)Texts.ReplaceBowl).gameObject.BindEvent(() => OnClickReplaceBowlButton());
        GetText((int)Texts.GoBack).gameObject.BindEvent(() => OnClickGoBackButton());

        return true;
    }

    private void OnClickGoBackButton()
    {
        Managers.ScoreManager.ResetAll();
        Managers.GameManager.isGameOverDialogEnabled = false;
        SceneManager.LoadScene(0);
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }

    private void OnClickReplaceBowlButton()
    {
        // ±×¸© Àç¼³Ä¡
        Debug.Log("±×¸© Àç¼³Ä¡");
    }

    private void OnClickBackToGameButton()
    {
        Managers.UI.ClosePopupUI();
    }
}
