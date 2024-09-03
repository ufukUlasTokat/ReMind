using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingInteractin : MonoBehaviour
{

    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            // Calculate direction from hand to block
            Vector3 direction = collision.contacts[0].point - transform.position;
            direction = -direction.normalized;

            // Apply force in the direction away from the hand
            GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Force);
        }
    }

}

