using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField]
    private Color _boostColor;

    private Color _startColor;

    private bool _isBoosted = false;

    private Material _playerMaterial;

    private void Start()
    {
        _playerMaterial = GetComponent<MeshRenderer>().material;
        _startColor = _playerMaterial.color;
    }

    private void Update()
    {
        if (_isBoosted) _playerMaterial.color = Color.Lerp(_playerMaterial.color, _boostColor, Time.deltaTime * 5); 
        else _playerMaterial.color = Color.Lerp(_playerMaterial.color, _startColor, Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "PickableObject")
        {
            collider.GetComponent<PickableObject>().Activate();
        }

        if (collider.transform.tag == "JumpPlace")
        {
            Player.Movement.JumpForce = 1300;
            _isBoosted = true;
        }

        if (collider.transform.tag == "Holder")
        {
            transform.position = collider.transform.GetComponent<Holder>().HoldTransform.position;
            Player.Movement.BoostTrail.time = 0.5f;
            Player.Movement._rb.isKinematic = true;
            collider.transform.GetComponent<Animator>().SetBool("IsHold", true);
            Player.IsHold = true;
            Player.Movement.HoldTarget = collider.transform;
            collider.enabled = false;
        }

        if (collider.transform.tag == "PlayerKiller")
        {
            Player.Killer.PlayerFall();
        }

        if(collider.transform.tag == "Bullet")
        {
            Player.Health.ApplyDamage(2);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "JumpPlace")
        {
            Player.Movement.JumpForce = 800;
            _isBoosted = false;
        }
    }
}
