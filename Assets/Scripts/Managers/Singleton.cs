using UnityEngine;
using System.Collections;

public abstract class Singleton<T> :MonoBehaviour where T : MonoBehaviour {
	protected static T _instance;

	public static T Instance {
		get {
			if(_instance == null) {
				GameObject go = new GameObject();
				T t = go.AddComponent<T>();
				_instance = t;
			}
			return _instance;
		}
	}
}
