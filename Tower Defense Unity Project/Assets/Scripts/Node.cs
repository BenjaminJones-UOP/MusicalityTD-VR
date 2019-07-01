using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
    public Color haveEnoughMoneyColor;
    public Color startColor;
    public Vector3 positionOffset;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;
    [HideInInspector]
    public bool isBuilt = false;

    public RightHand rightHandScript;
    public Music musicScript;
    uint playerID = 0;
    GameObject currentNode;

	BuildManager buildManager;

    bool selectedNode;
    bool doOnce;

    void Start ()
	{
		buildManager = BuildManager.instance;

        this.doOnce = true;

        //Posts the 3D music tracks on all the nodes depending on the level
        if (SceneManager.GetSceneByName("Level01") == SceneManager.GetActiveScene())
        {
            AkSoundEngine.PostEvent("Level1_3D", this.gameObject);
        }
        if (SceneManager.GetSceneByName("Level02") == SceneManager.GetActiveScene())
        {
            AkSoundEngine.PostEvent("Level2_3D", this.gameObject);
        }

    }

	public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}

    /*
	void OnMouseDown ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (turret != null)
		{
			buildManager.SelectNode(this);
			return;
		}

		if (!buildManager.CanBuild)
			return;

		BuildTurret(buildManager.GetTurretToBuild());
	}*/

	public void BuildTurret (TurretBlueprint blueprint)
	{

		if (PlayerStats.Money < blueprint.cost)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}

		PlayerStats.Money -= blueprint.cost;
		       

		GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBlueprint = blueprint;

		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Debug.Log("Turret build!");

        isBuilt = true;
        
    }

	public void UpgradeTurret ()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

		//Get rid of the old turret
		Destroy(turret);

		//Build a new one
		GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;

		Debug.Log("Turret upgraded!");
	}

	public void SellTurret ()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();

        isBuilt = false;

        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(turret);
		turretBlueprint = null;
	}

    /*
	void OnMouseEnter ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!buildManager.CanBuild)
			return;

		if (buildManager.HasMoney)
		{
			rend.material.color = hoverColor;
		} else
		{
			rend.material.color = notEnoughMoneyColor;
		}

	}

	void OnMouseExit ()
	{
		rend.material.color = startColor;
    }
    */

    private void Update()
    {
        //raycast select the current looked at node
        if(rightHandScript.GetFingertipHit().collider == null){ selectedNode = false; }
        else if (rightHandScript.GetFingertipHit().collider.CompareTag("Node"))
        {
            selectedNode = true;
            currentNode = this.gameObject;
        }
        if (rightHandScript.GetIsGrabbing())
        {
            //change colour of the current looked at node for building purposes
            if (selectedNode)
            {
                if (rightHandScript.GetFingertipHit().collider.gameObject == currentNode)
                {
                    currentNode.GetComponent<Renderer>().material.color = hoverColor;
                }
                else { currentNode.GetComponent<Renderer>().material.color = startColor; }
            }
        }
    }
}