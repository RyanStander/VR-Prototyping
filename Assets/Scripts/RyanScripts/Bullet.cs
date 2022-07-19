using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed=0f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += transform.forward*bulletSpeed;
    }
    void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);
        gameObject.SendMessage("HasAppliedDamage", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }

}
