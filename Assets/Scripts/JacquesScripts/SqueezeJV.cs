using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
    public class SqueezeJV : MonoBehaviour
    {
        public GameObject fireEffect;
        public GameObject firedObject;
        public Transform firePoint;
        public Interactable interactable;
        private Hand hand;

        public SteamVR_Action_Single gripSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

        public SteamVR_Action_Single pinchSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

        private new Rigidbody rigidbody;

        //Checks if able to fire again
        bool hasFired = true;

        private void Start()
        {
            //Get components and null checks
            if (rigidbody == null) { rigidbody = GetComponent<Rigidbody>(); }
            if (interactable == null) { interactable = GetComponent<Interactable>(); }
        }

        //-------------------------------------------------
        private void OnAttachedToHand(Hand attachedHand)
        {
            hand = attachedHand;
            hasFired = true;
        }

        private void Update()
        {
            //Reset input
            float grip = 0;
            // float pinch = 0;

            if (interactable.attachedToHand)
            {
                grip = gripSqueeze.GetAxis(interactable.attachedToHand.handType);
                //pinch = pinchSqueeze.GetAxis(interactable.attachedToHand.handType);
            }

            //If pinch is 1, then the input is true
            if (grip == 1)
            {
                //If havent fired yet
                if (!hasFired)
                {
                    fireAction();
                    hasFired = true;
                }
            }
            else if (grip == 0)
            {
                hasFired = false;
            }
        }
        private void fireAction()
        {
            //Instantiate the particle
            GameObject particle = GameObject.Instantiate<GameObject>(fireEffect, firePoint.position, firePoint.rotation);

            //float direction = firePoint.transform.position - transform.position;
            //rBody.velocity = direction.normalized;

            //Sets the appropriate point
            particle.transform.position = firePoint.position;
        }
    }
}