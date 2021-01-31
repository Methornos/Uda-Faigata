using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelRotate : MonoBehaviour
{
    private Transform _player;
    private Enemy _enemy;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _enemy = GetComponentInChildren<Enemy>();
    }
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        if (distance <= 50)
        {
            _enemy.CanShoot = true;
            Vector3 relativePos = _player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.down);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * 10);
        }
        else
        {
            _enemy.CanShoot = false;
        }
    }
}
