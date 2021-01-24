using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name;
    public int MaxHealth;
    public int CurrentHealth;

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
}
