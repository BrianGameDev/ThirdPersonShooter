using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public Dictionary<string, int> Items = new Dictionary<string, int>();

	public bool AddItem(string itemName, int amount = 1)
	{
		// If the item is found in the players inventory the just increase the quantity of the item
		if (Items.ContainsKey(itemName))
		{
			// TODO: This is where we could add in checking for a maximum quantity or conditions
			Items[itemName] += amount;
			return true;
		}

		// Otherwise create a new Item and it to the dictionary
		Items.Add(itemName, 1);

		return true;
	}

	public bool RemoveItem(string itemName)
	{
		// Check the players inventory for any items with the same name
		var item = Items.FirstOrDefault(i => i.Key == itemName);

		// If the item is found in the players inventory then proceed otherwise return false.
		if (Items.ContainsKey(itemName))
		{
			// If we have more than one of the item remove 1 from the players inventory.
			if (item.Value > 1)
			{
				Items[itemName] -= 1;
				return true;
			}

			// Otherwise just remove the item from the players inventory.
			return Items.Remove(itemName);
		}

		return false;
	}

	public void DisplayInventory()
	{
		Debug.Log("Inventory:");
		foreach (var i in Items)
		{
			Debug.Log(string.Format("{0}: {1}", i.Key, i.Value));
		}
	}
}
