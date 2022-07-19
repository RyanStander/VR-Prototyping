using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class InteractiveUI : MonoBehaviour
{

    public GameObject image1, image2;
    public float radius;

    private Vector3 handPos;

    public Hand handRight;
    public Hand handLeft;
    public SteamVR_Input_Sources handType;

    // Start is called before the first frame update
    void Start()
    {
        //image1.SetActive(false);
        //image2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (radius > Vector3.Distance(transform.position, handRight.transform.position))
        {
            if (handRight.grabPinchAction.GetStateDown(handType))
            {
                handPos = handRight.transform.position;
            }
            if (handRight.grabPinchAction.GetState(handType))
            {
                Vector2 startPos = new Vector2(handPos.x, handPos.z);
                Vector2 newPos = new Vector2(handRight.transform.position.x, handRight.transform.position.z);
                float distance = Vector2.Distance(newPos, startPos);
                if (startPos.x > newPos.x || startPos.y > newPos.y)
                {
                    distance *= -1;
                }
                //Vector3 newDistance = new Vector3(image1.transform.position.x + distance.x, image1.transform.position.y, image1.transform.position.z + distance.y);
                //image1.transform.position = newDistance;
                //Debug.Log(distance);
                image1.transform.rotation = Quaternion.Euler(image1.transform.rotation.x, distance * 50, image1.transform.rotation.z);
                //handPos = handRight.transform.position;
                //image1.transform.parent = handRight.transform;
            }
        }
    }
}
