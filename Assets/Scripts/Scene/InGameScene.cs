using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Game;
        //Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(() => { SceneManager.LoadScene(0); }, "GameOver", Managers.ScoreManager.score.ToString(), "ReStart");
        Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(() => { Managers.UI.ShowPopupUI<UISetPlatePosition>(); }, "Info", "Please detect the floor to start game", "Enter");
        Debug.Log("Init");

        return true;
    }
}