using UnityEngine;

public class InGameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.ItemManager.ResetState();

        SceneType = Define.Scene.Game;
        Managers.UI.ShowPopupUI<UIModeSelect>();

        return true;
    }
}