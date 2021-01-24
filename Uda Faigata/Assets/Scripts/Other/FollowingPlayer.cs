using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;

    private Transform _player;

    private void Start()
    {
        _offset = transform.position;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = _player.position + _offset;
    }
}
