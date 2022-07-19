using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Test : MonoBehaviour
{
    public Hand handRight;
    public Hand handLeft;
    public float radius;

    public Material grabMat;
    public Material inRangeMat;
    public Material defaultMat;

    public Vector3 defaultPosition;

    public SteamVR_Input_Sources handType;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
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
                GetComponent<MeshRenderer>().material = grabMat;
                transform.position = handRight.transform.position;
            }
            else
            {
                GetComponent<MeshRenderer>().material = inRangeMat;
                transform.position = defaultPosition;
            }
        }
        else
        {
            GetComponent<MeshRenderer>().material = defaultMat;
        }
    }
}
