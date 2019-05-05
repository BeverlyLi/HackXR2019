using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float gravityMitigationFactor;
    public Vector3 originalPosition;
    public GameObject magnetizeTo;
    public float magnetStrength;
    public float maxVelocity;

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
        

        if (Input.GetKeyDown("s")) {
            Reset();
        }
        if (OVRInput.Get(OVRInput.Button.One)) {
            Reset();

        }
        //Debug.Log("magnitude " + this.GetComponent<Rigidbody>().velocity.magnitude);
        if (this.GetComponent<Rigidbody>().velocity.magnitude > maxVelocity) {
            this.GetComponent<Rigidbody>().velocity = Vector3.Normalize(this.GetComponent<Rigidbody>().velocity) * maxVelocity;
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

    

    void OnCollisionEnter(Collision collision) {
        Vector3 magnetforce = magnetizeTo.transform.position - this.transform.position;



        this.GetComponent<Rigidbody>().AddForce(magnetforce);
    }
}
