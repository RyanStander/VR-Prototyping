using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class InstantiateFireBall : MonoBehaviour
{

    public Hand handRight;
    public Hand handLeft;
    public SteamVR_Input_Sources handTypeRight;
    public SteamVR_Input_Sources handTypeLeft;

    public GameObject ball;
    public bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (handRight.grabPinchAction.GetState(handTypeRight) && handLeft.grabPinchAction.GetState(handTypeLeft))
        {
            if (!spawned)
            {
                Vector3 instPos = (handRight.transform.position + handLeft.transform.position) / 2;
                GameObject newBall = Instantiate(ball, instPos, Quaternion.identity);
                newBall.GetComponent<Fireball>().handRight = handRight;
                newBall.GetComponent<Fireball>().handLeft = handLeft;
                newBall.GetComponent<Fireball>().inst = this;
                spawned = true;
            }
        }
    }
}
