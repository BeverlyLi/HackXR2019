using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public GameObject TargetUL; // Upper-left
    public GameObject TargetUM; // Upper-middle
    public GameObject TargetUR; // Upper-right
    public GameObject TargetLL; // Lower-left
    public GameObject TargetLM; // Lower-middle
    public GameObject TargetLR; // Lower-right

    private Animator TULAnimator;
    private Animator TUMAnimator;
    private Animator TURAnimator;
    private Animator TLLAnimator;
    private Animator TLMAnimator;
    private Animator TLRAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
     * From a separate script we call th
     */
    void AnimateIn() {
        //TBD
    }
    void AnimateOut() {
        //TBD
    }
}
