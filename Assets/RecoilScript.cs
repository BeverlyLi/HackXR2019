using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation anim;
    public LensFlare kool;
    public GameObject light1;
    public GameObject light2;
    public AudioSource sound;
    public Animator ani;

    private float triggerTh = 0.3f;
    private float triggerState = 0.0f;
    private float prevTriggerState = 1.0f;

    void Start()
    {
        ani.enabled = false;
        light1.SetActive(false);
        light2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((triggerState = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) )> triggerTh)
        {
            if (triggerState > triggerTh && prevTriggerState < triggerTh) {
                sound.Play();

            }
            kool.brightness = 1;
            ani.enabled = true;
            ani.Play("Gun Animation");
            light1.SetActive(true);
            light2.SetActive(true);
            
            Debug.Log("RREEEEEE");
        }
        
        if (triggerState < triggerTh && prevTriggerState > triggerTh)
        {
            light1.SetActive(false);
            light2.SetActive(false);
            kool.brightness = 0;
            ani.Play("Gun Animation", 0, 0);
            //ani.R = false;
        }
        prevTriggerState = triggerState;
    }
}
