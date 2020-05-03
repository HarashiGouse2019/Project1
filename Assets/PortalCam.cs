using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCam : MonoBehaviour
{
    public Transform playerCam;
    public Transform portal;
    public Transform oPortal;
 
    void Update()
    {
        //pofp = PlayerOffsetFromPortal
        Vector3 pofp = playerCam.position - oPortal.position;
        transform.position = portal.position + pofp;

        float adbpr = Quaternion.Angle(portal.rotation, oPortal.rotation);

        Quaternion prd = Quaternion.AngleAxis(adbpr, Vector3.up);
        Vector3 ncd = prd * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(ncd, Vector3.up);

    }
    


}
