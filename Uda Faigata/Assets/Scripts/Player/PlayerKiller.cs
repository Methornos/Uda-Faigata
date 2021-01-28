using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    [SerializeField]
    private Transform _startPosition;

    private Transform _self;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _self = transform;
    }

    private void FixedUpdate()
    {
        _self.position = new Vector3(_player.position.x, -15, _player.position.z);
    }

    public void PlayerFall()
    {
        if(Player.Health.Health > 3)
        {
            _player.position = _startPosition.position;
            Player.Health.ApplyDamage(3);
        }
        else
        {
            Player.Health.Death();
#if UNITY_EDITOR
            _player.position = _startPosition.position;
#endif
        }
    }
}
