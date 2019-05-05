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

    public ParticleSystem effect;

    public float magnetStrength;
    public GameObject magnetizeTo;

    Material material;
    public Material matDef;
    public Material matBall;
    public Material matInit;

    public GameObject paddleSide;
    // Start is called before the first frame update
    void Start()
    {
        effect.Stop();
        material = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = matInit;
    }

    // Update is called once per frame
    void Update()
    {
        Reposition();
        ModifyDistance();

        if (rTriggerPressed = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > triggerTh) {
            ActivateShield();
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.3f)
        {
            magnetizeTo.GetComponent<Rigidbody>().isKinematic = true;

            moveTowardsMagnet();
        }
        else
        {
            magnetizeTo.GetComponent<Rigidbody>().isKinematic = false;

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
        effect.transform.position = this.transform.position;
        effect.transform.rotation = this.transform.rotation;
        effect.Emit(50);
        StartCoroutine(this.GetComponent<OculusHaptics>().VibrateTime(OculusHaptics.VibrationForce.Light, 0.3f));

        if (!ball) return;
        if (!canActivateShield) return;
        Vector3 force = this.transform.forward * forceMagnitude;
        //force = Vector3.Normalize(force) * forceMagnitude;
        //ball.useGravity = true;
        ball.velocity = -ball.velocity;
        ball.velocity += force * Time.deltaTime;
        ball = null;
        canActivateShield = false;
        StartCoroutine("ShieldCooldown");
        //StartCoroutine(SpawnMagicCircle());
        //OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.RTouch);

    }


    IEnumerator ShieldCooldown() {
        yield return new WaitForSeconds(1);
        //Debug.Log("Shield Cooldown over");
        canActivateShield = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Ball>()) {
            GetComponent<Renderer>().material = matBall;
            paddleSide.GetComponent<Renderer>().material = matBall;
            ball = collider.attachedRigidbody;

        }
        
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Ball>())
        {

            GetComponent<Renderer>().material = matDef;
            paddleSide.GetComponent<Renderer>().material = matDef;
            ball = null;
        }
        
    }

    void moveTowardsMagnet() {
        if (!magnetizeTo) return;
        this.GetComponent<OculusHaptics>().Vibrate(OculusHaptics.VibrationForce.Light);
        magnetizeTo.transform.position = Vector3.Lerp(magnetizeTo.transform.position, this.transform.position, Time.deltaTime * 5);

    }



    
}
