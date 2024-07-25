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
        // Find계열 매서드보다 main Camera를 기반으로 Parent를 타고가는 방식이 성능면에서 더 좋을 것 같아서 적용 해봤는데 더 좋은 방법이 있는지 찾아봐야됨.
        XROrigin xrOrigin = Camera.main.transform.parent.parent.GetComponent<XROrigin>();
        _arPlaceObject = xrOrigin.GetComponent<ARPlaceObject>();

        GetButton((int)Buttons.Install).gameObject.BindEvent(EnterToInGame);
        _arPlaceObject.installable = true;

        return true;
    }

    private void EnterToInGame()
    {
        // 월드상으로 설치된 접시가 없을 경우 다이얼로그 출력 후 그 이후의 로직 생략
        if (_arPlaceObject.GetSpawnedGameObject() == null)
        {
            if (_isDialogShowing == false)
            {
                _isDialogShowing = true;
                Managers.UI.ShowPopupUI<UIConfirmDialog>().SetDialog(
                    () => { _isDialogShowing = false; },
                    "Info",
                    "Please add a bowl in world with touch.",
                    "Confirm");
            }
            return;
        }

        _arPlaceObject.installable = false;
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UIInGame>();
    }
}
