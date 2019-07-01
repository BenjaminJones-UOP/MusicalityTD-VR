using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using UnityEngine;

public class PlayerVR : MonoBehaviour {

    CharacterController player;

    float stepCycle;
    public float stepCycleThreshold = 1;

    // Use this for initialization
    void Start () {
        stepCycle = 0f;
        player = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (player.velocity.magnitude > 0)
        {
            FootSteps(stepCycleThreshold);
        }

        if (player.velocity.magnitude == 0)
        {
            stepCycle = 0;
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
}
