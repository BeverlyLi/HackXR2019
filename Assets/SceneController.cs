using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public TargetMover ul;
    public TargetMover um;
    public TargetMover ur;
    public TargetMover ll;
    public TargetMover lm;
    public TargetMover lr;
    public TargetMover finalBoss;
    public GameObject revealWall;

    private bool[][] sceneList;
    private bool allFalseDetected = false;
    private int sceneNum = 0;
    void Start()
    {
        sceneList = new bool[5][];
        sceneList[0] = new bool[] { false, false, false, true, false, false, true };
        sceneList[1] = GenerateNewScene();
        sceneList[2] = GenerateNewScene();
        sceneList[3] = GenerateNewScene();

        sceneList[4] = new bool[]{ false, false, false, false, false, false, true };


        
        StartCoroutine(AnimateBuffer());
    }

    // Update is called once per frame
    void Update()
    {
        bool allDown = true;
        for (int i = 0; i < 6; i++) {
            if (sceneList[sceneNum][i]) {
                allDown = false;
            }
        }
        if (sceneNum == 4) {
            allDown = false;
        }
        if (allDown && !allFalseDetected) {
            allFalseDetected = true;
            StartCoroutine(AnimateBuffer());
        }
        if (sceneNum == 4) {
            revealWall.transform.position = Vector3.Lerp(revealWall.transform.position, new Vector3(12f, 2f, 7.6f),Time.deltaTime * 5);
        }

        
    }
    IEnumerator AnimateBuffer()
    {
        yield return new WaitForSeconds(4);
        LoadScene(sceneList[++sceneNum]);
        allFalseDetected = false;
    }
    void LoadScene(bool[] scene) {
        
        for (int i = 0; i < scene.Length; i++)
        {
            if (scene[i])
            {
                switch (i)
                {
                    case 0:
                        ul.AnimateIn();
                        break;
                    case 1:
                        um.AnimateIn();
                        break;
                    case 2:
                        ur.AnimateIn();
                        break;
                    case 3:
                        ll.AnimateIn();
                        break;
                    case 4:
                        lm.AnimateIn();
                        break;
                    case 5:
                        lr.AnimateIn();
                        break;
                    case 6:
                        finalBoss.AnimateIn();
                        break;
                    default:
                        Debug.Log("animator bool array index mismatch");
                        break;


                }
            }
        }
    }

    bool[] GenerateNewScene() {
        bool[] newScene = new bool[7];
        for (int i = 0; i < 6; i++)
        {
            newScene[i] = Random.value > 0.5f;
        }
        newScene[6] = false;
        return newScene;
    }

    

    public void RemoveTarget(int ind) {
        sceneList[sceneNum][ind] = false;
    }
}
