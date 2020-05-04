using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Teleport : MonoBehaviour
{
    //Apparently, since we were using the Character Controller, the player wasn't teleporting properly
    public CharacterController player;

    public Transform receiver;

    private bool PlayerIsOverLapping = false;

    void Update()
    {
        
        if (PlayerIsOverLapping)
        {
            //Since now we are referencing from the Character Controller, he have to specify the transform, and then position
            //since our object isn't a Transform type anymore.
            Vector3 ptp = player.transform.position - transform.position;

            float dotProduct = Vector3.Dot(transform.up, ptp);

            //If this is true: the player has moved across the portal
            if (dotProduct < -0.1f)
            {
                

                //Teleport him
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;

                //That's the same case for our rotation
                //since player isn't a Transform type anymore
                player.transform.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffSet = Quaternion.Euler(0f, rotationDiff, 0f) * ptp;

                //We want to enable the character controller, so that we can actually do the offset.
                //If not, it'll still detect Portal A, which was part of the problem.
                //By doing this, we prevent collisions temporarily
                player.enabled = false;
                player.transform.position = receiver.position + positionOffSet;
                player.enabled = true;
                 
                //Then we do our normal "we aren't overlapping
                //This where only little changes that needed to be made.
                PlayerIsOverLapping = false;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerIsOverLapping = true;
            Debug.Log(gameObject.name);
            return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            PlayerIsOverLapping = false;
    }
}
