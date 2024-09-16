using UnityEngine;

public class HandleTouchFruits : MonoBehaviour
{
    [SerializeField]
    TapEventAsset _tapEventAsset;

    private void OnEnable()
    {
        _tapEventAsset.eventRaised += OnTap;
    }

    private void OnDisable()
    {
        _tapEventAsset.eventRaised -= OnTap;
    }

    // touchPos로 현재 사용자가 터치한 스크린 좌표를 받아옴.
    private void OnTap(object sender, Vector2 touchPos)
    {
        if (Managers.ItemManager.selectedSlot == -1 || Managers.ItemManager.slot[Managers.ItemManager.selectedSlot] == -1)
            return;

        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Fruit") && hit.transform.GetComponent<CheckGameOver>().InBowl)
            {
                // 사용중인 아이템 체크
                if ((Define.Item)Managers.ItemManager.slot[Managers.ItemManager.selectedSlot] == Define.Item.Delete)
                {
                    Managers.ItemManager.DeleteItem(hit.transform.gameObject);
                    Debug.Log("접시 위 과일");
                }

                else if ((Define.Item)Managers.ItemManager.slot[Managers.ItemManager.selectedSlot] == Define.Item.LevelUp)
                {
                    Managers.ItemManager.LevelUpItem(hit.transform.gameObject);
                }
            }
        }
    }
}
