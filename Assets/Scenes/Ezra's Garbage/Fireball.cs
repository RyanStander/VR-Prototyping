using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Fireball : MonoBehaviour
{

    public Hand handRight;
    public Hand handLeft;
    public InstantiateFireBall inst;
    public SteamVR_Input_Sources handTypeRight;
    public SteamVR_Input_Sources handTypeLeft;

    Vector3 handPosRight;
    Vector3 handPosLeft;

    Vector3 newBallPos;

    bool firstRun = true;
    // Start is called before the first frame update
    void Start()
    {
        //newBallPos = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject newBall;
        if (handRight.grabPinchAction.GetState(handTypeRight) && handLeft.grabPinchAction.GetState(handTypeLeft))
        {

            if (firstRun)
            {
                newBallPos = this.transform.position;
                handPosRight = handRight.transform.position;
                handPosLeft = handLeft.transform.position;

                firstRun = false;
            }

            if(handRight.grabGripAction.GetState(handTypeRight) && handLeft.grabGripAction.GetState(handTypeLeft))
            {
                transform.localScale += new Vector3(0.005f,0.005f,0.005f);
            }

            //right hand
            newBallPos.x += (handRight.transform.position.x - handPosRight.x) / 8;
            newBallPos.z += (handRight.transform.position.z - handPosRight.z) / 8;
            
            //left hand
            newBallPos.y += (handLeft.transform.position.y - handPosLeft.y) / 8;

            transform.position = newBallPos;
        }
        if (handRight.grabPinchAction.GetStateUp(handTypeRight) || handLeft.grabPinchAction.GetStateUp(handTypeLeft))
        {
            inst.spawned = false;
            Destroy(this.gameObject);
        }
    }
}
