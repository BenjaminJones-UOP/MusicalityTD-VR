using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

public class LeftHand : MonoBehaviour {

    Hand handScript;
    public RightHand rightHandScript;
    public GameObject standardTurret, missileTurret, laserTurret;

	// Use this for initialization
	void Start () {
        standardTurret.GetComponent<Rigidbody>().freezeRotation = enabled;
        missileTurret.GetComponent<Rigidbody>().freezeRotation = enabled;
        laserTurret.GetComponent<Rigidbody>().freezeRotation = enabled;
        handScript = GetComponent<Hand>();
    }

	// Update is called once per frame
	void Update () {
        TurretGrabbed(standardTurret);
        TurretGrabbed(missileTurret);
        TurretGrabbed(laserTurret);
    }

    void TurretGrabbed(GameObject turret)
    {
        bool isTurretGrabbed = turret.GetComponent<OVRGrabbable>().isGrabbed;

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            turret.SetActive(true);
        }
        
        else if (isTurretGrabbed)
        {
            turret.SetActive(true);
        }

        else { turret.SetActive(false); }
    }
}
