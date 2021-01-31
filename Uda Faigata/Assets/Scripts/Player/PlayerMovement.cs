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

    private Collision _collision;

    private bool _isBoostCd = false;

    public bool IsBoosted = false;

    public TrailRenderer BoostTrail;

    public Transform HoldTarget;
    public Transform Camera;

    [HideInInspector]
    public Rigidbody _rb;

    public float Speed = 10f;
    public float JumpForce = 300f;
    public float BoostPoints = 10f;

    public bool IsGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Camera = UnityEngine.Camera.main.transform;

        _rb.AddForce(Camera.forward * 10);
    }

    private void Update()
    {
        if (!GamePause.IsPaused) BoostLogic();
        StopLogic();
    }

    private void FixedUpdate()
    {
        if (!GamePause.IsPaused)
        {
            if (!Player.IsHold) MovementLogic();
            JumpLogic();
        }
    }

    private void MovementLogic()
    {
        Vector3 forward = Camera.TransformDirection(Vector3.forward);
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
            _rb.AddForce(-Camera.right * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(Camera.right * Speed);
        }
    }

    private void StopLogic()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            _rb.velocity = new Vector3(1, 0, 0);
            Player.Health.ApplyDamage(1);
        }
    }

    private void BoostLogic()
    {
        if (!IsBoosted)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_isBoostCd) StartBoost();

            if (BoostPoints < 10
                && !_isBoostCd)
            {
                BoostPoints += Time.deltaTime * 10f;
            }

            if(BoostTrail.time > 0)
            {
                BoostTrail.time -= Time.deltaTime * 0.5f;
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
        Speed *= 1.5f;
        BoostTrail.time = 0.5f;
    }

    private void StopBoost()
    {
        IsBoosted = false;
        Speed /= 1.5f;
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (!Player.IsHold)
            {
                if (IsGrounded)
                {
                    _rb.AddForce(Vector3.up * JumpForce);
                }
            }
            else
            {
                _rb.isKinematic = false;
                Player.Movement._rb.AddForce(Vector3.up * (Player.Movement.JumpForce + 1000));
                Player.Movement._rb.AddForce(Player.Movement.Camera.forward * (Player.Movement.JumpForce + 1000));
                Player.IsHold = false;
                HoldTarget.GetComponent<Animator>().SetBool("IsHold", false);
                StartCoroutine(HolderCd());
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == ("Ground"))
    //    {
    //        if (!IsGrounded)
    //        {
    //            IsGrounded = true;
    //            _dustParticle.Play();
    //        }
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            if (!IsGrounded) IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            IsGrounded = false;
        }
    }

    //private void IsGroundedUpdate(Collision collision, bool value)
    //{
    //    if (collision.gameObject.tag == ("Ground"))
    //    {
    //        if(value != IsGrounded)
    //        {
    //            IsGrounded = value;
    //            _dustParticle.Play();
    //        }
    //    }
    //    else
    //    {
    //        IsGrounded = false;
    //    }
    //}

    private IEnumerator BoostCd()
    {
        _isBoostCd = true;
        yield return new WaitForSeconds(1f);
        _isBoostCd = false;
    }

    private IEnumerator HolderCd()
    {
        yield return new WaitForSeconds(2f);
        HoldTarget.GetComponent<Collider>().enabled = true;
        HoldTarget = null;
    }
}