using System.Collections;
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
        SceneManager.LoadScene(1);
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
    }

    private void OnClickReplaceBowlButton()
    {
        // 그릇 재설치 함수 호출
        Debug.Log("그릇 재설치");
    }

    private void OnClickBackToGameButton()
    {
        Managers.UI.ClosePopupUI(this);
        //Managers.FruitsManager.GetComponent<ThrowFruit>().enabled = true;
        Time.timeScale = 1;
    }
}
