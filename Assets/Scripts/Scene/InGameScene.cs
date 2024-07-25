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
            "�ȳ�",
            "ȭ���� �����鼭 ����� �ν��� ��\n�ٴ��� ��ġ�Ͽ� �׸��� ��ġ�ϼ���.",
            "Ȯ��");

        Debug.Log("Init");

        return true;
    }

    private void MoveToInstallBowlStep()
    {
        Managers.UI.ShowPopupUI<UISetPlatePosition>();
    }
}