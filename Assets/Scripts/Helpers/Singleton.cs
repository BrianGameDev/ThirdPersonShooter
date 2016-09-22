using UnityEngine;

/// <summary>
///     Singleton.
///     This class is only meant to have one instance. A static property is provided for fast and easy access from
///     anywhere.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T mInstance;

	public static bool Exists
	{
		get { return mInstance != null; }
	}

	public static T Instance
	{
		get
		{
			EnsureExists();

			return mInstance;
		}
	}

	public static T GetPrivateInstance()
	{
		return mInstance;
	}


	public static void SetInstance(T instance)
	{
		mInstance = instance;
	}

	public static T EnsurePrefab(GameObject prefab)
	{
		if (mInstance == null)
		{
			// Create the prefab and get the script reference.
			mInstance = Helpers.Spawn<T>(prefab);
		}
		else
		{
			Debug.LogError(
				"Attempted to EnsurePrefab for type " + typeof (T).Name + " but instance already exists.");
		}
		return Instance;
	}

	public static T EnsurePrefab(string prefabName)
	{
		if (mInstance == null)
		{
			// Create the prefab and get the script reference.
			var prefab = Resources.Load(prefabName) as GameObject;
			if (prefab == null)
			{
				Debug.LogError("Could not find prefab " + prefabName + " for type " + typeof (T).Name);
			}
			mInstance = EnsurePrefab(prefab);
		}
		else
		{
			Debug.LogError("Attempted to EnsurePrefab for type " + typeof (T).Name +
							"but instance already exists.");
		}
		return Instance;
	}

	private static T findInScene()
	{
		// Warn of multiple instances.
		var mInstances = (T[]) FindObjectsOfType(typeof (T));
		if (mInstances.Length > 1)
		{
			Debug.LogError(typeof (T).Name + ": More than one instance exists.");
			Debug.Break();
		}

		// Found existing instance.
		if (mInstances.Length > 0)
			return mInstances[0];
		return null;
	}

	public static void EnsureExists()
	{
		if (mInstance == null)
		{
			// Find in Scene.
			mInstance = findInScene();

			// No instance found.
			if (mInstance != null) return;
			// Create a new GameObject.
			var newObj = new GameObject(typeof (T).ToString());

			// Add our MonoBehaviour as a Component and assign it to our instance.
			mInstance = newObj.EnsureComponent<T>();
		}
	}
}