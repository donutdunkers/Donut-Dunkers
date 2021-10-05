using System;
using UnityEngine;

public class ObjRing : MonoBehaviour {
    [SerializeField]
	private GameObject ringObject;
	
	private void OnTriggerEnter(Collider other) {
		this.ringObject.SetActive(false);
	}
}
