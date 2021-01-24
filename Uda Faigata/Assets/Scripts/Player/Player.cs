using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public static PlayerHealth Health;
    [HideInInspector]
    public static PlayerMovement Movement;
    [HideInInspector]
    public static PlayerAim Aim;

    private void Start()
    {
        Health = GetComponent<PlayerHealth>();
        Movement = GetComponent<PlayerMovement>();
        Aim = GetComponent<PlayerAim>();
    }
}
