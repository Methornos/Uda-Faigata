using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableObjectPanel : MonoBehaviour
{
    [SerializeField]
    private Text _objectName;
    [SerializeField]
    private Text _objectDescription;
    [SerializeField]
    private Text _objectEffect;

    public GameObject ObjectPanel;

    public void SetPanelSettings(PickableObject input)
    {
        _objectName.text = input.Name;
        _objectDescription.text = input.Description;
        _objectEffect.text = input.Effect;
    }

    public void EnablePanel() { ObjectPanel.SetActive(true); }
    public void DisablePanel() { ObjectPanel.SetActive(false); }
}
