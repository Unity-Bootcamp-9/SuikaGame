using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.Scene.Game;
        Managers.UI.ShowPopupUI<UIInGame>();
        Debug.Log("Init");

        return true;
    }
}
