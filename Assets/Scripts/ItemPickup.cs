using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
	public string ItemName = "";     // The name of the item to give
	public int Quantity = 1;	// The quantity of the item to give

	public void OnTriggerEnter(Collider other)
	{
		// Attempt to give the player the item, if its successful, destroy the physical object
		if (other.gameObject.GetComponent<Inventory>().AddItem(ItemName, Quantity))
		{
			Destroy(this.gameObject);
		}
	}
}
