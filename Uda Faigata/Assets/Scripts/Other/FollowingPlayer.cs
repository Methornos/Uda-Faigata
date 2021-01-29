using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private bool _withDelay = false;
    [SerializeField, Range(0, 1)]
    private float _delayTime;

    private Transform _player;

    private void Start()
    {
        _offset = transform.position;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (!_withDelay)
            transform.position = _player.position + _offset;
        else transform.position = Vector3.Lerp(transform.position, _player.position + _offset, _delayTime);
    }
}
