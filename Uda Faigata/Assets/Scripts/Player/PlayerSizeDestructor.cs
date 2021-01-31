using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeDestructor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (Player.IsIncreased )
            {
                Destroy(gameObject);
            }
        }
    }
}
