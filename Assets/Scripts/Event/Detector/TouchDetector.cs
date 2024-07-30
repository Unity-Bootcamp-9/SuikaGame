using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    [SerializeField]
    private TapEventAsset _tapEventAsset;

    private TouchEventController _touchEventController;

    private void Awake()
    {
        _touchEventController = GetComponent<TouchEventController>();
    }

    private void OnEnable()
    {
        _touchEventController.OnTap += DetectTap;
    }

    private void OnDisable()
    {
        _touchEventController.OnTap -= DetectTap;

    }

    private void DetectTap(Vector2 pos)
    {
        _tapEventAsset.Raise(pos);
    }
}
