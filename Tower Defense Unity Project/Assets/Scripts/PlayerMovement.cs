using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//My own script!! 
public class PlayerMovement : MonoBehaviour {

    public float speed = 2.0f;
    public float sensitivity = 2.0f;
    CharacterController player;

    public GameObject eyes;

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;

    float gravity = 4.0f;

    float stepCycle;
    public float stepCycleThreshold = 1;

    Ray centreScreenRay;
    RaycastHit nodeHit;

    private Node currentNode;

    bool shopActive;
    bool lookingAtNode;
    bool shopOpenUp;

    BuildManager buildManager;
    public GameManager gameManager;

    bool gameOver;

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    // Use this for initialization
    void Start () {
        stepCycle = 0f;

        player = GetComponent<CharacterController>();

        eyes = GameObject.FindWithTag("MainCamera");

        buildManager = BuildManager.instance;

        Cursor.lockState = CursorLockMode.Locked;

        gameOver = false;
    }
	

	// Update is called once per frame
	void Update () {

        gameOver = gameManager.GetGameIsOver();

        //Movement code used from another project (CT6CTPRO)
        moveFB = Input.GetAxis("Vertical") * speed;
        moveLR = Input.GetAxis("Horizontal") * speed;

        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotX = Input.GetAxis("Mouse X") * sensitivity;

        rotY = Mathf.Clamp(rotY, -90, 90);

        Vector3 movement = new Vector3(moveLR, -gravity, moveFB);

        //Stop the camera and player moving when the game is over
        if (gameOver)
        {
            transform.Rotate(0, 0, 0);
            movement = Vector3.zero;
            player.Move(Vector3.zero);
            eyes.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            transform.Rotate(0, rotX, 0);
            eyes.transform.localEulerAngles = new Vector3(-rotY, eyes.transform.localEulerAngles.y, eyes.transform.localEulerAngles.z);
            movement = transform.rotation * movement;
            player.Move(movement * Time.deltaTime);
        }

        //Ray cast code from centre screen
        centreScreenRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(centreScreenRay, out nodeHit, 10.0f);

        

        LookAtNode();
        InputManagement();

        if (lookingAtNode)
        {
            currentNode = nodeHit.collider.GetComponent<Node>();
        }

        //Footsteps code to play audio to a determined amount of time
        if (player.velocity.magnitude > 0)
        {
            FootSteps(stepCycleThreshold);
        }

        if (player.velocity.magnitude == 0)
        {
            stepCycle = 0;
        }

    }

    //Game logic code to look at current node
    private void LookAtNode()
    {
        if (nodeHit.collider != null)
        {
            lookingAtNode = true;
        }
        else {
            lookingAtNode = false;
            shopActive = false; 
            return; 
            }
    }

    private void InputManagement()
    {
        //When the player looks at a node and hits left mouse button, it opens up the shop
        if (Input.GetMouseButtonDown(0))
        {
            if (lookingAtNode) {
                shopOpenUp = true;
                shopActive = true;
            }
            if (shopOpenUp)
            {
                if (shopActive)
                {
                    Debug.Log("In Shop");
                    buildManager.SelectNode(currentNode); //shop ui and node ref for build manager
                    shopOpenUp = false;
                }
            }
        }

        //The player can shut the shop by clicking right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            shopActive = false;
            buildManager.DeselectNode();
        }

        //While the shop is open, the player can choose to build and standard, missile or laser turret by pressing 1, 2 or 3 respectively
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (shopActive)
            {
                buildManager.SelectTurretToBuild(standardTurret);
                currentNode.BuildTurret(buildManager.GetTurretToBuild());
                Debug.Log("Standard Turret Built");
                AkSoundEngine.SetRTPCValue("StandardVol", 1.0f, currentNode.gameObject); //Sets the volume of the standard turret on the current node the player is looking at
                shopActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (shopActive)
            {
                buildManager.SelectTurretToBuild(missileLauncher);
                currentNode.BuildTurret(buildManager.GetTurretToBuild());
                Debug.Log("Missile Turret Built");
                AkSoundEngine.SetRTPCValue("MissileVol", 1.0f, currentNode.gameObject); //Sets the volume of the missile turret on the current node the player is looking at
                shopActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (shopActive)
            {
                buildManager.SelectTurretToBuild(laserBeamer);
                currentNode.BuildTurret(buildManager.GetTurretToBuild());
                Debug.Log("Laser Turret Built");
                AkSoundEngine.SetRTPCValue("LaserVol", 1.0f, currentNode.gameObject); //Sets the volume of the laser turret on the current node the player is looking at
                shopActive = false;
            }
        }
    }

    //Periodically posts the footsteps event determined by a certain time threshold
    void FootSteps(float threshold)
    {
        if (stepCycle > threshold)
        {
            AkSoundEngine.PostEvent("Footsteps", gameObject);
            stepCycle = 0;

        }
        stepCycle += Time.deltaTime;
    }

    public bool ShopOpen() { return shopActive; }
    public RaycastHit GetNodeHit() { return nodeHit; }

    //To do:
    //Stop turrets from spawning on top of each other
}
