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

    [Range(0, 10)]
    public float AimPoints = 10f;

    public void On()
    {
        //Time.timeScale = 0.5f;
        IsAimed = true;
    }

    public void Off()
    {
        //Time.timeScale = 1f;
        IsAimed = false;
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
