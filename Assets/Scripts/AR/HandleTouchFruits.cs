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

    // touchPos�� ���� ����ڰ� ��ġ�� ��ũ�� ��ǥ�� �޾ƿ�.
    private void OnTap(object sender, Vector2 touchPos)
    {
        if (Managers.ItemManager.selectedSlot == -1 || Managers.ItemManager.slot[Managers.ItemManager.selectedSlot] == -1)
            return;

        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Fruit") && hit.transform.GetComponent<CheckGameOver>().InBowl)
            {
                // ������� ������ üũ
                if ((Define.Item)Managers.ItemManager.slot[Managers.ItemManager.selectedSlot] == Define.Item.Delete)
                {
                    Managers.ItemManager.DeleteItem(hit.transform.gameObject);
                    Debug.Log("���� �� ����");
                }

                else if ((Define.Item)Managers.ItemManager.slot[Managers.ItemManager.selectedSlot] == Define.Item.LevelUp)
                {
                    Managers.ItemManager.LevelUpItem(hit.transform.gameObject);
                }
            }
        }
    }
}
