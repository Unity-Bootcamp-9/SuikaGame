using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UISetPlatePosition : UIPopup
{
    private GameObject _bowlPrefab;

    private ARPlaceObject _arPlaceObject;

    private bool _isDialogShowing;

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
        // Find�迭 �ż��庸�� main Camera�� ������� Parent�� Ÿ������ ����� ���ɸ鿡�� �� ���� �� ���Ƽ� ���� �غôµ� �� ���� ����� �ִ��� ã�ƺ��ߵ�.
        XROrigin xrOrigin = Camera.main.transform.parent.parent.GetComponent<XROrigin>();
        _arPlaceObject = xrOrigin.GetComponent<ARPlaceObject>();

        GetButton((int)Buttons.Install).gameObject.BindEvent(EnterToInGame);
        _arPlaceObject.installable = true;

        return true;
    }

    private void EnterToInGame()
    {
        // ��������� ��ġ�� ���ð� ���� ��� ���̾�α� ��� �� �� ������ ���� ����
        if (_arPlaceObject.GetSpawnedGameObject() == null)
        {
            if (_isDialogShowing == false)
            {
                _isDialogShowing = true;
                Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(
                    () => { _isDialogShowing = false; },
                    "안내",
                    "확인된 그릇이 없어요.\n바닥을 터치하여 그릇을 설치해주세요.",
                    "확인");
            }
            return;
        }

        _arPlaceObject.installable = false;
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UIInGame>();
    }
}
