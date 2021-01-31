using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPosition;
    [SerializeField]
    private GameObject _bulletPrefab;

    private Enemy _enemy;

    private bool _isShooted = false;

    public float ShootCooldown = 2f;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if(_enemy.CanShoot)
        {
            if (!_isShooted)
                Shoot();
        }
    }

    private void Shoot()
    {
        _isShooted = true;

        GameObject bullet = Instantiate(_bulletPrefab, _shootPosition.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(_shootPosition.right * 10000);
        Destroy(bullet, 5f);
        StartCoroutine(ShootCD());
    }

    private IEnumerator ShootCD()
    {
        yield return new WaitForSeconds(ShootCooldown);
        _isShooted = false;
    }
}
