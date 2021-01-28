using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static PlayerHealth Health;
    public static PlayerMovement Movement;
    public static PlayerAim Aim;
    public static PlayerKiller Killer;

    public static bool IsHold = false;
    public static Collider HoldTarget;

    private void Start()
    {
        Health = GetComponent<PlayerHealth>();
        Movement = GetComponent<PlayerMovement>();
        Aim = GetComponent<PlayerAim>();
        Killer = GameObject.FindGameObjectWithTag("PlayerKiller").GetComponent<PlayerKiller>();
    }
}
