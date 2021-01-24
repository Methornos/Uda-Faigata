using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : PickableObject
{
    public override void Activate()
    {
        base.Activate();

        Player.Health.AddHealth(1);

        Destroy(gameObject);
    }
}
