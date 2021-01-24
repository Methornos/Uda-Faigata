using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : Skill
{
    [SerializeField]
    private GameObject _iceShotPrefab;

    private Transform _enemyTarget;

    private Transform _shotPosition;

    private bool _isShot = false;

    private void Start()
    {
        _enemyTarget = Player.Aim.EnemyTarget;
        _shotPosition = GameObject.FindGameObjectWithTag("ShotPosition").GetComponent<Transform>();

        StartCoroutine(ShotDelay());
    }

    private void Update()
    {
        Vector3 relativePos = _enemyTarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        if(!_isShot) transform.position = Vector3.Lerp(transform.position, _shotPosition.position, 0.1f);
        else transform.position = Vector3.Lerp(transform.position, _enemyTarget.position, Time.deltaTime * 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    public override void UseSkill()
    {
        Instantiate(_iceShotPrefab, Player.Movement.transform.position, Quaternion.identity);
    }

    private IEnumerator ShotDelay()
    {
        yield return new WaitForSeconds(1.5f);
        _isShot = true;
    }
}
