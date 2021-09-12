using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform Points;
    public GameObject spawnObjects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetType() == typeof(MeshCollider))
                Instantiate(spawnObjects, Points.position, Quaternion.identity);
        }
    }
}
