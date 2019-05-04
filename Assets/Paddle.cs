using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float triggerTh = 0.7f; // if the trigger is past this length, it is pressed 0->1
    private float thumbstickTh = 0.3f; // if the thumbstick goes past this threshold it is enabled x,y:-1->1 , (x,y)

    Rigidbody ball;

    public float distance = 0.5f;
    public float paddleTravelSpeed = 0.01f;
    public float minDist = 0.2f;
    public float maxDist = 5.0f;
    public float forceMagnitude = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Reposition();
        ModifyDistance();

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > triggerTh) {
            ActivateShield();
        }
    }

    private void Reposition()
    {
        this.transform.localPosition = new Vector3(0, 0, distance);
    }
    private void ModifyDistance()
    {
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y > thumbstickTh)
        {
            if(distance + 0.1 < maxDist){
                distance += paddleTravelSpeed;
            }
            
        }
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y < -triggerTh)
        {
            if (distance - 0.1 > minDist)
            {
                distance -= paddleTravelSpeed;
            }
        }
        
    }

    void ActivateShield()
    {

        if (!ball) return;
        Debug.Log("not null ball");
        Vector3 force = ball.transform.position - this.transform.position;
        force += this.transform.forward * distance;
        force = Vector3.Normalize(force);
        ball.useGravity = true;
        ball.AddForce(force);

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("trigger enter");
        ball = collider.attachedRigidbody;
    }
    void OnTriggerExit(Collider collider)
    {
        Debug.Log("trigger exit");
        ball = null;
    }
}
