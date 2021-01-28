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

        if (collider.transform.tag == "JumpPlace")
        {
            Player.Movement.JumpForce = 2700;
        }

        if (collider.transform.tag == "Holder")
        {
            transform.position = collider.transform.GetComponent<Holder>().HoldTransform.position;
            Player.Movement.BoostTrail.time = 0.5f;
            Player.Movement._rb.isKinematic = true;
            collider.transform.GetComponent<Animator>().SetBool("IsHold", true);
            Player.IsHold = true;
            Player.Movement.HoldTarget = collider.transform;
            collider.enabled = false;
        }

        if (collider.transform.tag == "PlayerKiller")
        {
            Player.Killer.PlayerFall();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "JumpPlace")
        {
            Player.Movement.JumpForce = 1200;
        }
    }
}
