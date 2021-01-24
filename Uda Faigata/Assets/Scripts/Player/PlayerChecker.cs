using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "PickableObject")
        {
            collider.GetComponent<PickableObject>().Activate();
        }
    }
}
