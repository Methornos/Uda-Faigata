using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public string Name;
    [Multiline]
    public string Description;
    [Multiline]
    public string Effect;

    public virtual void Activate()
    {

    }
}
