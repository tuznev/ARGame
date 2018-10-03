using UnityEngine;

public class SingeltonMono<T> : MonoBehaviour where T : MonoBehaviour {

	protected virtual void Awake () {
		Initialize ();
	}

	private static T _instance;
	public static T Instance {
		get {
			if (_instance == null)
				_instance = FindObject ();
			return _instance;
		}
	}

	public static bool IsExist { get { return Instance != null; } }

	private T Initialize () {
		return Instance;
	}

	private static T FindObject () {
		T[] allObj = FindObjectsOfType<T> ();

		if (allObj.Length == 0)
			return null;

		if (allObj.Length == 1)
			return allObj[0];

		for (int i = 1; i < allObj.Length; i++)
			Destroy (allObj[i].gameObject);

		return allObj[0];
	}
}
