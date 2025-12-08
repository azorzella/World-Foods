using UnityEngine;

public class ResourceLoader : MonoBehaviour {
	static ResourceLoader _i;
	
	/// <summary>
	/// Returns the static instance of ResourceLoader. If the instance
	/// has not been set, then it instantiates a new instance of
	/// ResourceLoader using the resource loader, caches it, and returns it
	/// </summary>
	public static ResourceLoader i {
		get {
			if (_i == null) {
				ResourceLoader x = Resources.Load<ResourceLoader>("ResourceLoader");

				_i = Instantiate(x);
			}
			return _i;
		}
	}
	
	void Start() {
		DontDestroyOnLoad(gameObject);
	}
	
	/// <summary>
	/// Returns an object loaded from the Resources folder
	/// </summary>
	/// <param name="objectName"></param>
	/// <returns></returns>
	public static GameObject LoadObject(string objectName) {
		GameObject result = Resources.Load<GameObject>(objectName);
		return result;
	}

	/// <summary>
	/// Loads the object of the passed name and then instantiates it at the passed position
	/// with the passed rotation
	/// </summary>
	/// <param name="objectName"></param>
	/// <param name="position"></param>
	/// <param name="rotation"></param>
	/// <returns></returns>
	public static GameObject InstantiateObject(string objectName, Vector2 position, Quaternion rotation) {
		GameObject loadedObject = LoadObject(objectName);

		if (loadedObject == null) {
			return null;
		}

		return Instantiate(loadedObject, position, rotation);
	}
}