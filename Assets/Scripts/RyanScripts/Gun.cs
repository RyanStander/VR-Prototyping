using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Gun : MonoBehaviour
{
    public Interactable interactable;

    private Hand hand;

    public GameObject bullet;
    [SerializeField][Range(1,15)] float fireRate = 1;
    private float nextFire;

    public SteamVR_Action_Single pinchSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

    private void Start()
    {
        if (interactable == null)
            interactable = GetComponent<Interactable>();
    }

    private void OnAttachedToHand(Hand attachedHand)
	{
		hand = attachedHand;
	}
    private void Update()
    {
        if (pinchSqueeze.GetAxis(interactable.attachedToHand.handType) ==1&& Time.time > nextFire && fireRate > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, transform.position+(transform.right*-0.15f)+(transform.forward*0.25f), transform.rotation);
        }
    }



}
