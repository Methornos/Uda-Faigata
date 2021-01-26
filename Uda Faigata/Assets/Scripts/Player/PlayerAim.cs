using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    private Image _aim;

    public bool IsAimed = false;
    public Transform EnemyTarget;
    public Transform PortalTarget;

    [Range(0, 10)]
    public float AimPoints = 10f;

    public void On()
    {
        IsAimed = true;
        Time.timeScale = 0.6f;
    }

    public void Off()
    {
        IsAimed = false;
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if(!IsAimed)
        {
            if (AimPoints < 10)
                AimPoints += Time.deltaTime * 2f;
        }
        else
        {
            if(AimPoints > 0)
                AimPoints -= Time.deltaTime * 4f;
        }

        if(AimPoints <= 0)
        {
            Off();
        }

        _aim.fillAmount = AimPoints / 10;
    }
}
