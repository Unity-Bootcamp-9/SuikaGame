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
            "안내",
            "화면을 돌리면서 평면을 인식한 후\n바닥을 터치하여 그릇을 설치하세요.",
            "확인");

        Debug.Log("Init");

        return true;
    }

    private void MoveToInstallBowlStep()
    {
        Managers.UI.ShowPopupUI<UISetPlatePosition>();
    }
}