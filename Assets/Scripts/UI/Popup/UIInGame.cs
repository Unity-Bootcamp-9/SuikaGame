using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGame : UIPopup
{
    // �̺�Ʈ ����, �Ҵ��� ��� //
    // �ؽ�Ʈ
    enum Texts
    {
    }

    // ��ư
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
