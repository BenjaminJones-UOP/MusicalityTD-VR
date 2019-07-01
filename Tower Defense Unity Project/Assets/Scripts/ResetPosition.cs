using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    Vector3 originalPosition;
    public RightHand rightHandScript;
    public GameObject anchorPoint;
    Vector3 resetPos;

	// Update is called once per frame
	void Update () {

        StartCoroutine(checkAnchorPosition());

        if (!gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            Debug.Log("Resetting");
            transform.position = Vector3.MoveTowards(transform.position, resetPos, 1);
        }
	}

    //Small turrets ignore each other's colliders and hand colliders (Untagged)
    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreLayerCollision(8, 8);

        if (collision.collider.tag == "SmallStandard" || collision.collider.tag == "SmallMissile" || collision.collider.tag == "SmallLaserBeam" || collision.collider.tag == "Untagged" || collision.collider.tag == "Node")
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
        }
    }

    IEnumerator checkAnchorPosition()
    {
        resetPos = anchorPoint.GetComponent<Transform>().position;
        yield return new WaitForSeconds(0.2f);
    }
}
