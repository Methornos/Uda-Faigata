using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    private Rigidbody _playerRigid;

    private bool _isStarted = false;
    private bool _isStopped = false;

    private void Start()
    {
        _playerRigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_playerRigid.velocity.x == 0 &&
            _playerRigid.velocity.y == 0 &&
            _playerRigid.velocity.z == 0 &&
            Player.Movement.IsGrounded)
        {
            _isStopped = true;
            if (!_isStarted) StartCoroutine(DoDamage());
        }
        else
        {
            _isStopped = false;
            StopCoroutine(DoDamage());
            _isStarted = false;
        }
    }

    private IEnumerator DoDamage()
    {
        _isStarted = true;
        if(_isStopped) Player.Health.ApplyDamage(1);
        yield return new WaitForSeconds(1f);
        if(_isStarted) StartCoroutine(DoDamage());
    }
}
