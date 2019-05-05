using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public Animator anim; 
    

    

    // Start is called before the first frame update
    void Start()
    {
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")) {
            AnimateIn();
        }
        if (Input.GetKeyDown("w"))
        {
            AnimateOut();
        }
    }
    /*
     * From a separate script we call th
     */
    public void AnimateIn() {
        anim.enabled = true;
        anim.Play("SlideIn", 0, 0);
    }
    public void AnimateOut() {
        anim.enabled = true;
        anim.Play("SlideOut", 0, 0);
    }
}
