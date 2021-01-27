using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField]
    private PickableObjectPanel _objectPanel;
    [SerializeField]
    private EnemyPanel _enemyPanel;

    [SerializeField]
    private Vector3 _offset;

    private float x = 0.0f;
    private float y = 0.0f;

    private Camera _camera;

    private RaycastHit _hit;

    private Transform _player;

    private Transform _holdTarget = null;

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
                if (!_objectPanel.IsEnabled)
                {
                    _objectPanel.EnablePanel();
                    _objectPanel.SetPanelSettings(_hit.transform.GetComponent<PickableObject>());
                }
            }
            else _objectPanel.DisablePanel();

            if (_hit.transform.tag == "Enemy")
            {
                if (!_enemyPanel.IsEnabled)
                {
                    _enemyPanel.EnablePanel();
                    _enemyPanel.SetPanelSettings(_hit.transform.GetComponent<Enemy>());
                }
                else _enemyPanel.SetPanelSettings(_hit.transform.GetComponent<Enemy>());

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (Player.Aim.EnemyTarget == null) Player.Aim.EnemyTarget = _hit.transform;
                }
            }
            else _enemyPanel.DisablePanel();

            if(_hit.transform.tag == "Holder")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    _holdTarget = _hit.transform;
                    _player.position = Vector3.Lerp(_player.position, _holdTarget.GetComponent<Holder>().HoldTransform.position, 1f);
                    Player.Movement.BoostTrail.time = 0.5f;
                    Player.Movement._rb.isKinematic = true;
                    _hit.transform.GetComponent<Animator>().SetBool("IsHold", true);
                    Player.IsHold = true;
                }
            }

            if (Input.GetKey(KeyCode.R))
            {
                Player.Aim.On();
                if (_hit.transform.tag == "Portal")
                {
                    if (Player.Aim.EnemyTarget == null) Player.Aim.PortalTarget = _hit.transform;

                    if (Input.GetKeyDown(KeyCode.Mouse0)) _player.position = Player.Aim.PortalTarget.position;
                }
            }
            else
            {
                Player.Aim.PortalTarget = null;

                Player.Aim.Off();
            }
        }
        else
        {
            _enemyPanel.DisablePanel();
            Player.Aim.PortalTarget = null;

            Player.Aim.Off();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Player.Aim.EnemyTarget = null;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Player.Aim.PortalTarget = null;

            Player.Aim.Off();
        }

        if(Input.GetKeyUp(KeyCode.Space) &&
            _holdTarget)
        {
            Player.Movement._rb.isKinematic = false;
            _holdTarget.GetComponent<Animator>().SetBool("IsHold", false);
            _holdTarget = null;
            Player.Movement.BoostTrail.time = 0;

            Player.IsHold = false;
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

        if (Player.Aim.PortalTarget)
        {
            Vector3 relativePos = Player.Aim.PortalTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;

            transform.position = _player.position + _offset - transform.forward * distance;
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