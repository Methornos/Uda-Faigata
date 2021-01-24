using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private PickableObjectPanel _objectPanel;

    [SerializeField]
    private Vector3 _offset;

    private float x = 0.0f;
    private float y = 0.0f;

    private Camera _camera;

    private RaycastHit _hit;

    private bool _isCatched = false;

    private Transform _player;

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private void Start()
    {
        _camera = Camera.main;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    Vector3 Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, Mathf.Infinity))
        {
            if (_hit.transform.tag == "PickableObject")
            {
                _objectPanel.EnablePanel();
                _objectPanel.SetPanelSettings(_hit.transform.GetComponent<PickableObject>());
            }
            else _objectPanel.DisablePanel();

            if (_hit.transform.tag == "Enemy")
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    _isCatched = true;
                    if(Player.Aim.EnemyTarget == null) Player.Aim.EnemyTarget = _hit.transform;

                    Player.Aim.On();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _isCatched = false;
            Player.Aim.EnemyTarget = null;

            Player.Aim.Off();
        }
    }

    private void FixedUpdate()
    {
        if (Player.Aim.EnemyTarget)
        {
            Vector3 relativePos = Player.Aim.EnemyTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            transform.position = _player.position + _offset - transform.forward * distance  ;
        }
    }

    private void LateUpdate()
    {
        if (target)
        {
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            if (!Player.Aim.EnemyTarget)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                RaycastHit hit;
                if (Physics.Linecast(target.position, transform.position, out hit, 7, QueryTriggerInteraction.Collide))
                {
                    distance -= hit.distance;
                }
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target.position + _offset;

                transform.rotation = rotation;
                transform.position = position;
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}