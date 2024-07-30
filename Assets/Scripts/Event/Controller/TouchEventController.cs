using UnityEngine;
using UnityEngine.InputSystem;

public class TouchEventController : MonoBehaviour
{
    #region Events
    public delegate void Tap(Vector2 position);
    public event Tap OnTap;
    #endregion

    [SerializeField]
    private InputActionReferences m_InputActionReferences;


    // Start is called before the first frame update
    void Start()
    {
        if (m_InputActionReferences != null)
        {
            m_InputActionReferences.screenTap.action.started += (ctx) => ScreenTapEvent(ctx);
        }
    }

    private void ScreenTapEvent(InputAction.CallbackContext context)
    {
        if (OnTap != null)
        {
            if (context.control.device is not Pointer pointer)
            {
                Debug.LogError("Input actions are incorrectly configured. Expected a Pointer binding ScreenTapped.", this);
                return;
            }
            var tapPosition = pointer.position.ReadValue();

            OnTap((tapPosition));
        }
    }
}
