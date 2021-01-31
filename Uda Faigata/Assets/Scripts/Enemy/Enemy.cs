using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;

    public bool CanShoot = false;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void DealDamage(int damage)
    {
        if (CurrentHealth > damage) CurrentHealth -= damage;
        else KillEnemy();
    }

    public void KillEnemy()
    {
        GetComponent<DestructableObject>().SpawnObjects();
        Destroy(gameObject);
    }
}
