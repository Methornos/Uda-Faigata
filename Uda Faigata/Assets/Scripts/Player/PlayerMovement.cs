using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Image _boostImage;
    [SerializeField]
    private ParticleSystem _dustParticle;
    [SerializeField]
    private TrailRenderer _boostTrail;

    private Rigidbody _rb;
    private Transform _camera;

    private Collision _collision;

    private bool _isBoostCd = false;

    public bool IsBoosted = false;

    public float Speed = 10f;
    public float JumpForce = 300f;
    public float BoostPoints = 10f;

    public bool IsGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;

        _rb.AddForce(_camera.forward * 10);
    }

    private void Update()
    {
        BoostLogic();
    }

    private void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void MovementLogic()
    {
        Vector3 forward = _camera.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        if(Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(forward * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddForce(-forward * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(-_camera.right * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(_camera.right * Speed);
        }
    }

    private void BoostLogic()
    {
        if (!IsBoosted)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) StartBoost();

            if (BoostPoints < 10
                && !_isBoostCd)
            {
                BoostPoints += Time.deltaTime * 10f;
            }

            if(_boostTrail.time > 0)
            {
                _boostTrail.time -= Time.deltaTime * 0.5f;
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.LeftShift)) StopBoost();

            if (BoostPoints > 0)
            {
                BoostPoints -= Time.deltaTime * 5f;
            }
            else if (BoostPoints <= 0)
            {
                StopBoost();
                StartCoroutine(BoostCd());
            }
        }

        _boostImage.fillAmount = BoostPoints / 10f;
    }

    private void StartBoost()
    {
        IsBoosted = true;
        Speed = 90;
        _boostTrail.time = 0.5f;
    }

    private void StopBoost()
    {
        IsBoosted = false;
        Speed = 60;
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (IsGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);

                IsGroundedUpdate(_collision, false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpdate(collision, true);
        _collision = collision;
    }

    

    private void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            IsGrounded = value;
        }

        if (IsGrounded) _dustParticle.Play();
    }

    private IEnumerator BoostCd()
    {
        _isBoostCd = true;
        yield return new WaitForSeconds(1f);
        _isBoostCd = false;
    }
}