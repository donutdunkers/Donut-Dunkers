using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

    public bool showingLevelSelect = false;
    public Transform mainCamPos, levelSelectCamPos;


    void Start() {
        this.transform.SetPositionAndRotation( mainCamPos);
    }

    // Update is called once per frame
    void Update() {
        
    }


    void LerpTransform(Transform target) {
        
    }
}
