using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGame : UIPopup
{
    // 이벤트 변경, 할당할 요소 //
    // 텍스트
    enum Texts
    {
    }

    // 버튼
    enum Buttons 
    { 
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
}
