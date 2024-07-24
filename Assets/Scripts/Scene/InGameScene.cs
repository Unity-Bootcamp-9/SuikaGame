using UnityEngine;

public class InGameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Game;
        Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(
            MoveToInstallBowlStep,
            "Info",
            "Please detect the floor to start game",
            "Enter");

        Debug.Log("Init");

        return true;
    }

    private void MoveToInstallBowlStep()
    {
        Managers.UI.ShowPopupUI<UISetPlatePosition>();
    }
}