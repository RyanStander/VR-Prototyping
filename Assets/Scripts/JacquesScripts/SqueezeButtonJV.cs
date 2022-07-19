using UnityEngine;
using Valve.Newtonsoft.Json.Bson;
using Valve.VR;
using Valve.VR.InteractionSystem;
namespace Valve.VR.InteractionSystem.Sample
{
    public class SqueezeButtonJV : MonoBehaviour
    {
        //Material changes on these based on controller state
        public Renderer[] visualCaps;
        public Material inactiveGripMaterial;
        public Material activeGripMaterial;
        public Material pinchGripMaterial;
        private Material currentMaterial;

        //Creation point for the effect
        public Transform effectCreationPoint;

        //Avoids spamming
        private bool wasPressedLast=true;

        //Particle prefab that will be made
        public GameObject particleEffectPress, particleEffectRelease;

        //Relies on interactable to pick up
        public Interactable interactable;

        //Gets the user control input
        public SteamVR_Action_Single gripSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");

        private void Start()
        {
            if (interactable == null)
                interactable = GetComponent<Interactable>();
        }

        private void Update()
        {
            //Is either 0 or 1 based on input
            float grip = 0;

            //Gets the control inputs if controlled is in hand
            if (interactable.attachedToHand)
            {
                grip = gripSqueeze.GetAxis(interactable.attachedToHand.handType);
            }
            else
            {
                //Set current material to active grip
                currentMaterial = activeGripMaterial;
                return;
            }

            //The check prioritizes 
            //If pinch is 1, then the effect input is true
            if (grip == 1)
            {
                //Set current material
                currentMaterial = pinchGripMaterial;

                //If was previously not pressed
                if (!wasPressedLast)
                {
                    //Create particles
                    createEffect(particleEffectPress);

                    //Prevents spam firing
                    wasPressedLast = true;
                }
            }
            else if (grip == 0)
            {
                //Set current material
                currentMaterial = activeGripMaterial;

                //If was pressed last
                if(wasPressedLast)
                {
                    //Create particles
                    createEffect(particleEffectRelease);

                    //Prevents spam firing
                    wasPressedLast = false;
                }

            }

            SetRenderMaterial(currentMaterial);
        }
        private void createEffect(GameObject prefabEffect)
        {
            //Instantiate particle effect
            GameObject effect = GameObject.Instantiate<GameObject>(prefabEffect);

            //Sets the appropriate position
            effect.transform.position = effectCreationPoint.position;
        }

        //Change visual material for user
        private void SetRenderMaterial(Material givenMaterial)
        {
            foreach(Renderer cap in visualCaps)
            {
                cap.material = givenMaterial;
            }
        }
    }
}