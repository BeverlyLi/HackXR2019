using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float gravityMitigationFactor;
    public Vector3 originalPosition;
    public GameObject magnetizeTo;
    public float magnetStrength;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody>().useGravity)
        {
            mitigateGravity();
            
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.3f)
        {
            moveTowardsMagnet();
        }

        if (Input.GetKeyDown("s")) {
            Reset();
        }
        if (OVRInput.Get(OVRInput.Button.One)) {
            Reset();
        }
    }

    void mitigateGravity()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, gravityMitigationFactor, 0));
    }
    void Reset() {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = originalPosition;
        this.GetComponent<Rigidbody>().velocity = new Vector3();
    }

    void moveTowardsMagnet() {
        if (!magnetizeTo) return;
        Rigidbody b = this.GetComponent<Rigidbody>();
        //b.velocity = 0.5f * b.velocity;

        Vector3 magnetizeForce = magnetizeTo.transform.position - this.transform.position;
        magnetizeForce = magnetizeForce * magnetStrength;
        b.AddForce(magnetizeForce);
    }
}
