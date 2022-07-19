using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Chest : MonoBehaviour
{

    public Hand handRight;
    public Hand handLeft;
    public float radius;

    public Material grabMat;
    public Material inRangeMat;
    public Material defaultMat;

    public Vector3 defaultPosition;
    public float chestRotation;

    public SteamVR_Input_Sources handType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update!");
        if (radius > Vector3.Distance(transform.position, handRight.transform.position))
        {
            Debug.Log("In radius!");
            if (handRight.grabPinchAction.GetState(handType))
            {
                Debug.Log("Grabbed!");
                GetComponentInChildren<MeshRenderer>().material = grabMat;
                if (handRight.transform.position.y < transform.position.y )
                {
                    Debug.Log("under");
                    chestRotation = 0;
                }
                else if (handRight.transform.position.y > transform.position.y + GetComponentInChildren<Renderer>().bounds.size.y)
                {
                    Debug.Log("top");
                    chestRotation = 90;
                }
                else 
                {
                    Debug.Log("inside boundaries");
                    chestRotation = (handRight.transform.position.y - transform.position.y) * 180;
                    Debug.Log(handRight.transform.position.y - transform.position.y);
                 }
            }
            else
            {
                GetComponentInChildren<MeshRenderer>().material = inRangeMat;
            }
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material = defaultMat;
        }
        transform.rotation = Quaternion.AngleAxis(chestRotation, Vector3.back);
    }
}
