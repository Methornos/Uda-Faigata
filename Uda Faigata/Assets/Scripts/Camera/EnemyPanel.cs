using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour
{
    [SerializeField]
    private Text _enemyName;
    [SerializeField]
    private Image _enemyHealth;

    public GameObject Panel;

    public bool IsEnabled = false;

    public void SetPanelSettings(Enemy enemy)
    {
        _enemyName.text = enemy.Name;
        _enemyHealth.fillAmount = (enemy.CurrentHealth / enemy.MaxHealth) * 100;
    }

    public void CalculateHealth()
    {

    }

    public void EnablePanel() { Panel.SetActive(true); IsEnabled = true; }
    public void DisablePanel() { Panel.SetActive(false); IsEnabled = false; }
}
