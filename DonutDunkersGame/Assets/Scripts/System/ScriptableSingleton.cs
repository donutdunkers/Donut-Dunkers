using System;
using System.Linq;
using UnityEngine;

public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject {
	private static T _instance = null;
	
	public static T Instance {
		get {
			if (ScriptableSingleton<T>._instance == null) {
				var instances = Resources.LoadAll<T>(string.Empty);
				ScriptableSingleton<T>._instance = instances.FirstOrDefault();
				if (ScriptableSingleton<T>._instance == null) {
					Debug.LogErrorFormat("[ScriptableSingeton] could not find instance of {0}", new object[] { typeof(T).ToString() });
				} else {
					try {
						(ScriptableSingleton<T>._instance as ScriptableSingleton<T>).OnLoadInstance();
					}
					catch {
						Debug.LogError("Cast Error");
					}
				}
			}
			return ScriptableSingleton<T>._instance;
		}
	}
	
	protected virtual void OnLoadInstance() {
	}		
}
