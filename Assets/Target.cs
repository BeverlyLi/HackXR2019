using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    public TargetMover mover;
    public Animator doorL;
    public Animator doorR;
    public ButtonCollide door;
    public SceneController sc;
    public int ind;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision) {
        mover.AnimateOut();

        doorL.Play("SlideL Return", 0, 0);
        doorR.Play("SlideR Return", 0, 0);
        door.open = false;
        sc.RemoveTarget(ind);
    }
}
