using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRigidbodies : MonoBehaviour
{
    public float lifetime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody [] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            Destroy(rb, lifetime);
        }
    }
}
