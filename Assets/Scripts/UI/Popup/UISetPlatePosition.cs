using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UISetPlatePosition : UIPopup
{
    private GameObject _bowlPrefab;

    private ARPlaceObject _arPlaceObject;

    enum Texts
    {
    }

    enum Buttons
    { 
        Install
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        _arPlaceObject = FindObjectOfType<ARPlaceObject>();

        GetButton((int)Buttons.Install).gameObject.BindEvent(EnterToInGame);
        _arPlaceObject.installable = true;

        return true;
    }

    private void EnterToInGame()
    {
        // ��������� ��ġ�� ���ð� ���� ��� ���̾�α� ��� �� �� ������ ���� ����
        if (_arPlaceObject.GetSpawnedGameObject() == null)
        {
            Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(() => { }, "Info", "Please add a bowl in world with touch.", "Confirm");
            return;
        }

        _arPlaceObject.installable = false;
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UIInGame>();
    }
}
