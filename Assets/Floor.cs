using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float floorBounceFactor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider collider) {
        Vector3 v = collider.attachedRigidbody.velocity;
        v = v + 2 * Vector3.Project(v, Vector3.up) * 2;
        collider.attachedRigidbody.velocity = v;
    }
}
