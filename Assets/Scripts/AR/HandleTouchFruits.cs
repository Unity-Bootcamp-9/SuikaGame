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
        Debug.Log($"touchPos is {touchPos}");
    }
}
