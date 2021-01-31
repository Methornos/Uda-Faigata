using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public List<GameObject> SpawnableObjects;

    public void SpawnObjects()
    {
        for (int i = 0; i < SpawnableObjects.Count; i++)
        {
            Instantiate(SpawnableObjects[i], transform.position + new Vector3(Random.Range(0, 2), transform.position.y - 3, Random.Range(0, 2)), Quaternion.identity);
        }
    }
}
