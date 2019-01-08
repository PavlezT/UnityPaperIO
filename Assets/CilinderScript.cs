using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("trigger enter");
    //    print("Collision detected with trigger object " + other.name);
    //}

    //void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("trigger stay");
    //    print("Still colliding with trigger object " + other.name);
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("trigger exit");
    //    print(gameObject.name + " and trigger object " + other.name + " are no longer colliding");
    //}

    // lower not working
    void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("collision enter");
        print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        print("Their relative velocity is " + collisionInfo.relativeVelocity);
    }

    //void OnCollisionStay(Collision collisionInfo)
    //{
    //    Debug.Log("collision stay");
    //    print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
    //}

    void OnCollisionExit(Collision collisionInfo)
    {
        Debug.Log("collision exit");
        print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
    }
}
