using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float triggerTh = 0.7f; // if the trigger is past this length, it is pressed 0->1
    private float thumbstickTh = 0.3f; // if the thumbstick goes past this threshold it is enabled x,y:-1->1 , (x,y)

    private bool rTriggerPressed = false;
    private bool canActivateShield = true;

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

        if (rTriggerPressed = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > triggerTh) {
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
        if (!canActivateShield) return;
        Vector3 force = this.transform.forward * forceMagnitude;
        //force = Vector3.Normalize(force) * forceMagnitude;
        ball.useGravity = true;
        ball.velocity = -ball.velocity;
        ball.AddForce(force);
        ball = null;
        canActivateShield = false;
        StartCoroutine("ShieldCooldown");
        Debug.Log("Shield Activated");

    }
    IEnumerator ShieldCooldown() {
        yield return new WaitForSeconds(1);
        Debug.Log("Shield Cooldown over");
        canActivateShield = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        
        ball = collider.attachedRigidbody;
    }
    void OnTriggerExit(Collider collider)
    {
        ball = null;
    }
}
