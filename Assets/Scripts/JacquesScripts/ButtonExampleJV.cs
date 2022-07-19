using System.Collections;
using UnityEngine;

//Simplified button example, creates a balloon instead of a flower and does so with a particle effect
namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonExampleJV : MonoBehaviour
    {
        //Button object that hovers
        public HoverButton hoverButton;

        //Prefab that will be made
        public GameObject prefab;
        public GameObject[] particleEffectPrefab;

        private void Start()
        {
            //Add listener
            hoverButton.onButtonDown.AddListener(OnButtonDown);
        }

        private void OnButtonDown(Hand hand)
        {
            //Instantiate particle effect
            GameObject effect = GameObject.Instantiate<GameObject>(particleEffectPrefab[UnityEngine.Random.Range(0, particleEffectPrefab.Length)]);

            //Sets the appropriate position
            effect.transform.position = this.transform.position;

            //Instantiate Balloon
            StartCoroutine(InstantiateBalloon());
        }
        private IEnumerator InstantiateBalloon()
        {
            GameObject prefabObject = GameObject.Instantiate<GameObject>(prefab);

            //Sets the appropriate position
            prefabObject.transform.position = this.transform.position;

            //Sets a random color to the balloon
            Color currentColor = UnityEngine.Random.ColorHSV(0f, 0.5f, 1f, 1f, 0.5f, 1f);
            currentColor.a = 0;
            prefabObject.GetComponent<MeshRenderer>().material.SetColor("Color", currentColor);

            //While timer
            while (currentColor.a < 1)
            {
                //Increase alpha
                currentColor.a += 0.05f;

                //Red
                if (currentColor.r < 1f)
                {
                    currentColor.r += 0.05f;
                }
                else
                {
                    currentColor.r = 1;
                }
                //Green
                if (currentColor.g < 1f)
                {
                    currentColor.g += 0.05f;
                }
                else
                {
                    currentColor.g = 1;
                }
                //Blue
                if (currentColor.b < 1f)
                {
                    currentColor.b += 0.05f;
                }
                else
                {
                    currentColor.b = 1;
                }

                prefabObject.GetComponent<MeshRenderer>().material.SetColor("New", currentColor);
                yield return null;
            }
        }
        //private IEnumerator DoPlant()
        //{
        //    GameObject planting = GameObject.Instantiate<GameObject>(prefab);
        //    planting.transform.position = this.transform.position;
        //    planting.transform.rotation = Quaternion.Euler(0, Random.value * 360f, 0);

        //    planting.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));

        //    Rigidbody rigidbody = planting.GetComponent<Rigidbody>();
        //    if (rigidbody != null)
        //        rigidbody.isKinematic = true;


        //    Vector3 initialScale = Vector3.one * 0.01f;
        //    Vector3 targetScale = Vector3.one * (1 + (Random.value * 0.25f));

        //    float startTime = Time.time;
        //    float overTime = 0.5f;
        //    float endTime = startTime + overTime;

        //    while (Time.time < endTime)
        //    {
        //        planting.transform.localScale = Vector3.Slerp(initialScale, targetScale, (Time.time - startTime) / overTime);
        //        yield return null;
        //    }


        //    if (rigidbody != null)
        //        rigidbody.isKinematic = false;
        //}
    }
}