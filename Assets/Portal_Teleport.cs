using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Teleport : MonoBehaviour
{
    public Transform Player;
    public Transform receiver;


    private bool PlayerIsOverLapping = false;
    void Update()
    {

        if (PlayerIsOverLapping)
        {
            Vector3 ptp = Player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, ptp);


            if (dotProduct < 0f)
            {

                float rotationDiff = Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                Player.Rotate(Vector3.up, rotationDiff);

                Vector3 PositionOffSet = Quaternion.Euler(0f, rotationDiff, 0f) * ptp;
                Player.position = receiver.position + PositionOffSet;
                 
                PlayerIsOverLapping = false;
                Debug.Log("PlayerIsOverLapping"); 



            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerIsOverLapping = true;
            Debug.Log("Colliding");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerIsOverLapping = false;
        }
    }
   


}
