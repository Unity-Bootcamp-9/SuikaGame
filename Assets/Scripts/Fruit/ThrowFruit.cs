using UnityEngine;

public class ThrowFruit : MonoBehaviour
{
    [SerializeField]
    private SwipeEventAsset _swipeEventAsset;

    [Range(0f, 1f)]
    public float _minYThreshold;
    [Range(0f, 1f)]
    public float _maxXThreshold;

    //public GameManager scoreManager;

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
        // 레이케스트로 감지 + 태그로 조건 걸기
        Ray ray = Camera.main.ScreenPointToRay(args.StartScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!hit.transform.CompareTag("Fruit") ||
                (hit.transform.GetComponent<CheckGameOver>() != null && 
                hit.transform.GetComponent<CheckGameOver>().InBowl))
            {
                return;
            }
        }
        else // 공백 부분 스와이프 시 예외 처리
        {
            return;
        }

        // �������� �¿������� Screen �������� ���� //
        //scoreManager.checkDropText.text = $"y: {args.ScreenDirection.y >= _minYThreshold} x: {Mathf.Abs(args.ScreenDirection.x) <= _maxXThreshold}";
        //scoreManager.direction.text = $"{args.ScreenDirection} / ABS x: {Mathf.Abs(args.ScreenDirection.x)}";
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

        gameObject.transform.parent = Managers.FruitsManager.fruitsParent.transform;
        Managers.FruitRandomSpawnManager.SpawnFruits();
        Managers.FruitRandomSpawnManager.EnableAllColliders(gameObject);

        Managers.SoundManager.Play(Define.Sound.Throw, "Throw");

        // �� ���� Throw �ǵ��� �̺�Ʈ ���� ����
        _swipeEventAsset.eventRaised -= Throw;
    }
}
