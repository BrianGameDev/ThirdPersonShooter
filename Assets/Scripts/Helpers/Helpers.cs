using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
public static class Helpers
{
    public enum Ternary
    {
        Negative,
        Neutral,
        Positive
    }

    public static T Spawn<T>(GameObject prefabToSpawn) where T : MonoBehaviour
	{
		return Spawn<T>(prefabToSpawn,
		                Vector3.zero,
		                Quaternion.identity);
	}

	/// <summary>
	/// Spawns a prefab and returns the monoBehaviour script reference.
	/// </summary>
	public static T Spawn<T>(GameObject prefabToSpawn,
	                         Vector3 location,
	                         Quaternion rotation,
	                         Transform parentTo = null) where T : MonoBehaviour
	{
		// Create.
		var newGO = Object.Instantiate (prefabToSpawn,
										location,
										rotation) as GameObject;
		// Set Parent.
		newGO.transform.parent = parentTo;
		
		// Ensure Component.
		var newComponent = EnsureComponent<T> (newGO);
		return newComponent;
	}
	
	/// <summary>
	/// Extension Method: Ensures that a MonoBehaviour exists on a gameObject.
	/// </summary>
	public static T EnsureComponent<T>(this GameObject newGO) where T : MonoBehaviour
	{
		var newComponent = newGO.GetComponent (typeof(T)) as T;
		if (newComponent == null)
		{
			newComponent = newGO.AddComponent (typeof(T)) as T;
		}
		return newComponent;
	}

	/// <summary>
	/// Strips a Vector of its x, y and or z value(s).
	/// </summary>
	public static Vector3 StripVector(Vector3 vector, string strip) {
		strip = strip.ToUpper();
		Vector3 returnVector;
		switch (strip)
		{
			case "XY":
				returnVector = new Vector3(0, 0, vector.z);
				break;
			case "YZ":
				returnVector = new Vector3(vector.x, 0, 0);
				break;
			case "ZX":
				returnVector = new Vector3(0, vector.y, 0);
				break;
			case "X":
				returnVector = new Vector3(0, vector.y, vector.z);
				break;
			case "Y":
				returnVector = new Vector3(vector.x, 0, vector.z);
				break;
			case "Z":
				returnVector = new Vector3(vector.x, vector.y, 0);
				break;
			default:
				Debug.LogError("StripVector: The strip text is invalid.");
				return vector;
		}
		return returnVector;
	}

	/// <summary>
	/// Calculates the position of where a target will be.
	/// </summary>
	/// <param name="targetPos">target Position</param>
	/// <param name="targetDir">target Direction</param>
	/// <param name="selfPos">Self Pos</param>
	/// <param name="selfDir">Self Dir</param>
	/// <param name="targetSpeed">target speed</param>
	/// <param name="projectileSpeed">Projectile speed</param>
	/// <returns>The Angle of where you should aim to hit the target.</returns>
	public static float CalculateTargetAngle(Vector2 targetPos, Vector2 targetDir, Vector2 selfPos, Vector2 selfDir, float targetSpeed, float projectileSpeed)
	{
		float x;
		float y;
		var impactTime = targetSpeed / (projectileSpeed / 2);

		if (targetPos.x > selfPos.x)
			x = -impactTime;
		else
			x = impactTime;

		if (targetPos.y > selfPos.y)
			y = -impactTime;
		else
			y = impactTime;
		
		var impactVector = new Vector2(targetDir.x * x, targetDir.y * y);
		// Debug.Log($"X: {x}, Y: {y}, iV {impactVector}");

		Vector2 calculatedPosition = targetPos + impactVector;
		Vector2 calculatedDirection = calculatedPosition - selfPos;
		calculatedDirection.Normalize();

		float angle = Mathf.Atan2(calculatedDirection.y, calculatedDirection.x);
		angle = Mathf.Rad2Deg * angle;
		return angle;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="no">The Number you want to make sure is positive</param>
	/// <returns>A positive number</returns>
	public static float Positive(float no)
	{
		if (no < 0)
		{
			return no * (-1);
		}
		return no;
	}


    /// <summary>
    /// Returns the 'Sign' of the given value.
    /// </summary>
    public static int ReturnSign(float number)
    {
        if (number < 0)
            return -1;
        return 1;
    }
}