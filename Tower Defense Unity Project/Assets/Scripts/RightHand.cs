using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

public class RightHand : MonoBehaviour {

    Hand handScript;
    LineRenderer lineRenderer;

    Ray fingertipRay;
    RaycastHit fingerTipHit;

    Node currentNode;

    [HideInInspector]
    public OVRGrabber grabbedObj;

    BuildManager buildManager;
    public GameManager gameManager;

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    private Vector3 standardPos, missilePos, laserPos;
    private Quaternion standardRot, missileRot, laserRot;

    bool isGrabbing, lookingAtNode, standardSelected, missileSelected, laserSelected;

    // Use this for initialization
    void Start () {
        handScript = GetComponent<Hand>();
        lineRenderer = GetComponent<LineRenderer>();
        grabbedObj = GetComponent<OVRGrabber>();
        buildManager = BuildManager.instance;

        standardSelected = false;
        missileSelected = false;
        laserSelected = false;

        standardPos = GameObject.FindGameObjectWithTag("SmallStandard").GetComponent<Transform>().position;
        missilePos = GameObject.FindGameObjectWithTag("SmallMissile").GetComponent<Transform>().position;
        laserPos = GameObject.FindGameObjectWithTag("SmallLaserBeam").GetComponent<Transform>().position;

        standardRot = GameObject.FindGameObjectWithTag("SmallStandard").GetComponent<Transform>().rotation;
        missileRot = GameObject.FindGameObjectWithTag("SmallMissile").GetComponent<Transform>().rotation;
        laserRot = GameObject.FindGameObjectWithTag("SmallLaserBeam").GetComponent<Transform>().rotation;

    }
	
	// Update is called once per frame
	void Update () {

        GrabManagement();
        LookAtNode();
        TurretBuilder();

        if (isGrabbing)
        {
            if (this.handScript.GetIsPoint())
            {
                fingertipRay = new Ray(GameObject.FindGameObjectWithTag("FingerTip").transform.position, transform.forward);
                Physics.Raycast(fingertipRay, out fingerTipHit, 10.0f);

                if (lookingAtNode)
                {
                    Debug.Log("Checking current node...");
                    currentNode = fingerTipHit.collider.GetComponent<Node>();
                }

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, GameObject.FindGameObjectWithTag("FingerTip").transform.position);
                lineRenderer.SetPosition(1, GameObject.FindGameObjectWithTag("LinePosition").transform.position);
            }
            else { lineRenderer.enabled = false; }
        }
        if(!isGrabbing){ lineRenderer.enabled = false; }
    }

    void GrabManagement()
    {
        //Check which object is being grabbed
        if(grabbedObj != null) {
            if (grabbedObj.grabbedObject.GetComponent<Collider>().CompareTag("SmallStandard"))
            {
                isGrabbing = true;
                standardSelected = true;
                Debug.Log("Picked up Standard Turret");
            }


            if (grabbedObj.grabbedObject.GetComponent<Collider>().CompareTag("SmallMissile"))
            {
                isGrabbing = true;
                missileSelected = true;
                Debug.Log("Picked up Missile Launcher");
            }


            if (grabbedObj.grabbedObject.GetComponent<Collider>().CompareTag("SmallLaserBeam"))
            {
                laserSelected = true;
                isGrabbing = true;
                Debug.Log("Picked up Laser Beam");
            }

        }
        else { isGrabbing = false; return; }
    }

    //Game logic code to look at current node
    private void LookAtNode()
    {
        if (fingerTipHit.collider != null)
        {
            Debug.Log("Looking at something");
            lookingAtNode = true;
        }
        else
        {
            lookingAtNode = false;
        }
    }

    void TurretBuilder()
    {
        if (standardSelected)
        {
            if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
            //if(!isGrabbing)
            {
                buildManager.SelectNode(currentNode);
                buildManager.SelectTurretToBuild(standardTurret);
                currentNode.BuildTurret(buildManager.GetTurretToBuild());
                Debug.Log("Standard Turret Built");
                AkSoundEngine.SetRTPCValue("StandardVol", 1.0f, currentNode.gameObject);
                buildManager.DeselectNode();
                lineRenderer.enabled = false;
                standardSelected = false;
            }
        }

        if (missileSelected)
        {
            if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
            //if(!isGrabbing)
            {
                buildManager.SelectNode(currentNode);
                buildManager.SelectTurretToBuild(missileLauncher);
                currentNode.BuildTurret(buildManager.GetTurretToBuild());
                Debug.Log("Missile Launcher Built");
                AkSoundEngine.SetRTPCValue("MissileVol", 1.0f, currentNode.gameObject);
                buildManager.DeselectNode();
                lineRenderer.enabled = false;
                missileSelected = false;
            }
        }

        if (laserSelected)
        {
            if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
            //if(!isGrabbing)
            {
                buildManager.SelectNode(currentNode);
                buildManager.SelectTurretToBuild(laserBeamer);
                currentNode.BuildTurret(buildManager.GetTurretToBuild());
                Debug.Log("Laser Beamer Built");
                AkSoundEngine.SetRTPCValue("LaserVol", 1.0f, currentNode.gameObject);
                buildManager.DeselectNode();
                lineRenderer.enabled = false;
                laserSelected = false;
            }
        }
    }

    public RaycastHit GetFingertipHit() { return fingerTipHit; }
    public bool GetIsGrabbing() { return isGrabbing; }
}
