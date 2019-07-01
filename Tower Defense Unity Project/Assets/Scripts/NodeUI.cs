using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

	public GameObject upgradeSellUI;
    public GameObject shopUI;

	public Text upgradeCost;

	public Text sellAmount;

	private Node target;

	public void SetUpgradeSellTarget (Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		if (!target.isUpgraded)
		{
			upgradeCost.text = "£" + target.turretBlueprint.upgradeCost;
		} else
		{
			upgradeCost.text = "DONE";
		}

		sellAmount.text = "£" + target.turretBlueprint.GetSellAmount();

		upgradeSellUI.SetActive(true);
	}

    public void SetShopTaget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();

        if (!target.isBuilt)
        {
            shopUI.SetActive(true);
        }

    }

    public void Hide (GameObject ui)
	{
		ui.SetActive(false);
	}

	public void Upgrade ()
	{
		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
	}

	public void Sell ()
	{
		target.SellTurret();
		BuildManager.instance.DeselectNode();
	}

    private void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(transform.position - cameraPos);
    }

}

/* Program flow
 * Click on node
 * Shop UI turns up
 * Press 1, 2 or 3 to build turret
 * Click on turret again
 * Upgrade/sell UI turns up
 * Click upgrade or sell
 */