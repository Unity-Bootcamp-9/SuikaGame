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
        Debug.Log($"touchPos is {touchPos}");
    }
}
