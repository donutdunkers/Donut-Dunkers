using System;
using UnityEngine;

public class BallSkin : MonoBehaviour {
	
    private static BallSkin _Instance;
    public static BallSkin Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<BallSkin>();
            }
            return _Instance;
        }
    }
	
	[SerializeField]
	private float followSpeed = 5f;
	
	private void Start() {
		
	}
	
	private void Update() {
		this.transform.position = Vector3.Lerp(this.transform.position, BallController.Instance.transform.position, Time.deltaTime * this.followSpeed);
	}
}
