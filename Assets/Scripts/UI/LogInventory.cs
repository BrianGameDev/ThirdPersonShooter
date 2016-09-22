using UnityEngine;
using System.Collections;

public class LogInventory : MonoBehaviour {

	public void Log()
	{
		Inventory.Instance.LogInventory();
	}
}
