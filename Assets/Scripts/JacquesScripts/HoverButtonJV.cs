//======= Recreation of Vale Corporations HoverButton Script by Jacques. ===============
//
// Most of this is a rephrased version of the original copy, recreated for understanding purposes
//THIS SCRIPT DOES NOT CURRENTLY WORK!
//
//=============================================================================
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
    [RequireComponent(typeof(Interactable))]
    public class HoverButtonJV : MonoBehaviour
    {
        [Header("Button Attributes")]
        //Part of the button that will move
        public Transform movingPart;

        //Rate of movement locally
        public Vector3 localMoveDistance = new Vector3(0, -0.1f, 0);

        //How "pressed" the button must be before either engaging or disengaging
        [Range(0, 1)]
        public float engageAtPercent = 0.90f;

        [Range(0, 1)]
        public float disengageAtPercent = 0.80f;

        //Events defined by the hand
        public HandEvent onButtonDown;
        public HandEvent onButtonUp;
        public HandEvent onButtonIsPressed;

        public bool engaged = false;
        public bool buttonDown = false;
        public bool buttonUp = false;

        //Start position is defined on start and end position is the start position plus the local moving distance
        private Vector3 startPosition;
        private Vector3 endPosition;

        //Used to check if hand is within hover distance
        private Vector3 handEnteredPosition;

        //Checks if hand is hovering over button
        private bool hovering;

        //The specific player hand that hovered last
        private Hand lastHoveredHand;

        // Start is called before the first frame update
        private void Start()
        {
            //Null Check for the button piece that moves
            if (movingPart == null)
            {
                //Set the moving part to the object that the script is currently attached to
                movingPart = this.transform.GetChild(0);
            }

            //Set the starting position of the button
            startPosition = movingPart.localPosition;

            //Set the end position
            endPosition = startPosition + localMoveDistance;

            //Set the inital hand entering position, updated in HandHoverUpdate
            handEnteredPosition = endPosition;
        }

        //Is called from the hand script
        private void HandHoverUpdate(Hand hand)
        {
            //Set to true when hovering
            hovering = true;

            //Set the current hovering hand to the last hovering hand
            lastHoveredHand = hand;

            bool wasEngaged = engaged;

            //Calculates distances for the hand and the button
            float currentDistance = Vector3.Distance(movingPart.parent.InverseTransformPoint(hand.transform.position), endPosition);
            float enteredDistance = Vector3.Distance(handEnteredPosition, endPosition);

            //If the hand is in position
            if (currentDistance > enteredDistance)
            {
                enteredDistance = currentDistance;
                handEnteredPosition = movingPart.parent.InverseTransformPoint(hand.transform.position);
            }

            //Calculate difference
            float distanceDifference = enteredDistance - currentDistance;

            float lerp = Mathf.InverseLerp(0, localMoveDistance.magnitude, distanceDifference);

            if (lerp > engageAtPercent)
                engaged = true;
            else if (lerp < disengageAtPercent)
                engaged = false;

            movingPart.localPosition = Vector3.Lerp(startPosition, endPosition, lerp);

            InvokeEvents(wasEngaged, engaged);
        }

        private void LateUpdate()
        {
            //If the hand is not currently hovering
            if (hovering == false)
            {
                movingPart.localPosition = startPosition;
                handEnteredPosition = endPosition;

                //Invoke events
                InvokeEvents(engaged, false);
                engaged = false;
            }

            //Set it back to false (in case it was true) to be checked again
            hovering = false;
        }

        private void InvokeEvents(bool wasEngaged, bool isEngaged)
        {
            buttonDown = wasEngaged == false && isEngaged == true;
            buttonUp = wasEngaged == true && isEngaged == false;

            //Button is pressing
            if (buttonDown && onButtonDown != null)
            {
                onButtonDown.Invoke(lastHoveredHand);
            }
            //Button is lifting
            if (buttonUp && onButtonUp != null)
            {
                onButtonUp.Invoke(lastHoveredHand);
            }
            //Button is engaged
            if (isEngaged && onButtonIsPressed != null)
            {
                onButtonIsPressed.Invoke(lastHoveredHand);
            }
        }
    }
}