using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollide : MonoBehaviour
{
    public GameObject bullet;
    public GameObject LeftDoor;
    public GameObject RightDoor;
    private Animator buttonAnimator;
    private Animator leftDoorAnimator;
    private Animator rightDoorAnimator;

    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator = GetComponent<Animator>();
        leftDoorAnimator = LeftDoor.GetComponent<Animator>();
        rightDoorAnimator = RightDoor.GetComponent<Animator>();
        buttonAnimator.enabled = false;
        leftDoorAnimator.enabled = false;
        rightDoorAnimator.enabled = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Ball")
        {
            if (!open)
            {
                buttonAnimator.enabled = true;
                leftDoorAnimator.enabled = true;
                rightDoorAnimator.enabled = true;

                buttonAnimator.Play("ButtonPress", 0, 0);
                leftDoorAnimator.Play("SlideL", 0, 0);
                rightDoorAnimator.Play("SlideR", 0, 0);
                bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 200));
            }
            open = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
