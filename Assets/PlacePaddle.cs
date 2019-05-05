using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePaddle : MonoBehaviour
{
    private float triggerTh = 0.7f; // if the trigger is past this length, it is pressed 0->1
    private float thumbstickTh = 0.3f; // if the thumbstick goes past this threshold it is enabled x,y:-1->1 , (x,y)

    private bool rTriggerPressed = false;
    private bool canActivateShield = true;
    private bool canPlace = true;

    Rigidbody ball;

    public float distance = 0.5f;
    public float paddleTravelSpeed = 0.01f;
    public float minDist = 0.2f;
    public float maxDist = 5.0f;
    public float forceMagnitude = 5.0f;

    public ParticleSystem effect;


    Material material;
    public Material matDef;
    public Material matBall;
    public Material matInit;

    public GameObject paddleSide;
    public GameObject subPlacePaddle;
    private int maxSubPaddleCount = 3;
    private static List<GameObject> subPaddleList;
    // Start is called before the first frame update
    void Start()
    {
        effect.Stop();
        subPaddleList = new List<GameObject>();
        material = GetComponent<Renderer>().material;
        GetComponent<Renderer>().material = matInit;
    }

    // Update is called once per frame
    void Update()
    {
        Reposition();
        ModifyDistance();

        if (rTriggerPressed = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > triggerTh)
        {
            ActivateShield();
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.3f)
        {
            PlaceSubPaddle();
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
            if (distance + 0.1 < maxDist)
            {
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
        if (OVRInput.Get(OVRInput.Button.Two)) {
            this.GetComponent<Paddle>().enabled = true;
            Destroy(subPaddleList[0]);
            Destroy(subPaddleList[1]);
            Destroy(subPaddleList[2]);
            this.GetComponent<PlacePaddle>().enabled = false;
            
        }


    }

    void ActivateShield()
    {
        effect.transform.position = this.transform.position;
        effect.transform.rotation = this.transform.rotation;
        effect.Emit(50);
        Debug.Log("reached3");
        StartCoroutine(this.GetComponent<OculusHaptics>().VibrateTime(OculusHaptics.VibrationForce.Light, 0.3f));
        Debug.Log("reached1");
        if (!ball) return;
        Debug.Log("reached2");
        if (!canActivateShield) return;
        Debug.Log("reached4");
        Vector3 force = this.transform.forward * forceMagnitude;
        //force = Vector3.Normalize(force) * forceMagnitude;
        //ball.useGravity = true;
        ball.velocity = -ball.velocity;
        ball.velocity += force * Time.deltaTime;
        ball = null;
        canActivateShield = false;
        StartCoroutine("ShieldCooldown");
        Debug.Log("reached");
        //StartCoroutine(SpawnMagicCircle());
        //OVRInput.SetControllerVibration(1f, 1f, OVRInput.Controller.RTouch);

    }
    


    IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log("Shield Cooldown over");
        canActivateShield = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Ball>())
        {
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


    void PlaceSubPaddle()
    {
        if (!canPlace) return;

        GameObject sp = Instantiate(subPlacePaddle);
        sp.transform.position = this.transform.position;
        subPaddleList.Add(sp);
        if (subPaddleList.Count > maxSubPaddleCount)
        {
            GameObject o = subPaddleList[0];
            subPaddleList.RemoveAt(0);
            Destroy(o);
        }
        canPlace = false;
        StartCoroutine("PlaceCooldown");



    }


    IEnumerator PlaceCooldown()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log("Shield Cooldown over");
        canPlace = true;
    }

}
