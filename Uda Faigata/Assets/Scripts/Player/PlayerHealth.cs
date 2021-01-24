using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Transform _heartsContainer;
    [SerializeField]
    private List<GameObject> _hearts;
    private int _lastActiveHeart;

    public float Health = 10;

    public void ApplyDamage(int value)
    {
        if (Health > value)
        {
            if (Health == 1)
            {
                Health -= value;
                _hearts[0].SetActive(false);
            }
            else
            {
                for (int i = _hearts.Count - 1; i > 0; i--)
                {
                    if (_hearts[i].activeSelf == true)
                    {
                        Health -= value;
                        _hearts[i].SetActive(false);
                        return;
                    }
                }
            }
        }
        else
        {
            Death();
        }
    }

    public void AddHealth(int value)
    {
        if(Health < 28)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                if (_hearts[i].activeSelf == false)
                {
                    Health += value;
                    _hearts[i].SetActive(true);
                    return;
                }
            }
        }
    }

    public void Death()
    {
        Debug.Log("Troop");
    }
}
