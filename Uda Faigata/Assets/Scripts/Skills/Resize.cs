using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : Skill
{
    private Transform _playerTransform;

    private bool _isActive = false;

    private Vector3 _resizeSize = new Vector3(3, 3, 3);
    private Vector3 _startSize = new Vector3(1, 1, 1);

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (_isActive) _playerTransform.localScale = Vector3.Lerp(_playerTransform.localScale, _resizeSize, Time.deltaTime * 2);
        else _playerTransform.localScale = Vector3.Lerp(_playerTransform.localScale, _startSize, Time.deltaTime * 2);
    }

    public override void UseSkill()
    {
        _isActive = true;
        Player.Movement._rb.mass *= 2;
        Player.Movement.Speed += 30;
        Player.Movement.JumpForce += 300;
        Player.IsIncreased = true;
    }

    public override void DeactivateSkill()
    {
        _isActive = false;
        Player.Movement._rb.mass /= 2;
        Player.Movement.Speed -= 30;
        Player.Movement.JumpForce -= 300;
        Player.IsIncreased = false;
    }
}
