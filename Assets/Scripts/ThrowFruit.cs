using UnityEngine;

public class ThrowFruit : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    [Range(0f, 1f)]
    public float _minYThreshold;
    [Range(0f, 1f)]
    public float _maxXThreshold;

    //public GameManager gameManager;

    private Rigidbody _rigidbody;

    [SerializeField]
    private float launchAngle = 45f;

    // TODO : UI�� Speed ������ Slider �ʿ�.
    public float speed = 4f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _swipeEventAsset.eventRaised += Throw;
    }

    private void OnDisable()
    {
        _swipeEventAsset.eventRaised -= Throw;
    }

    private void Throw(object sender, SwipeData args)
    {
        // �������� �¿������� Screen �������� ���� //
        //gameManager.checkDropText.text = $"y: {args.ScreenDirection.y >= _minYThreshold} x: {Mathf.Abs(args.ScreenDirection.x) <= _maxXThreshold}";
        //gameManager.direction.text = $"{args.ScreenDirection} / ABS x: {Mathf.Abs(args.ScreenDirection.x)}";
        if (args.ScreenDirection.y < _minYThreshold || Mathf.Abs(args.ScreenDirection.x) > _maxXThreshold)
        {
            return;
        }

        // ��ô�Ÿ� ����� World�������� ��� //
        float force = args.Distance / args.Duration;

        // ������ �������� ��ȯ
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // xz ��鿡���� �ӵ�
        float horizontalSpeed = Mathf.Cos(angleInRadians);

        // y ���� �ӵ�
        float verticalSpeed = Mathf.Sin(angleInRadians);

        Vector3 launchVelocity = Camera.main.transform.forward + horizontalSpeed * verticalSpeed * Camera.main.transform.up;

        _rigidbody.useGravity = true;
        _rigidbody.AddForce(launchVelocity * force * speed, ForceMode.Impulse);

        gameObject.transform.SetParent(transform.root);

        // �� ���� Throw �ǵ��� �̺�Ʈ ���� ����
        _swipeEventAsset.eventRaised -= Throw;
    }
}
