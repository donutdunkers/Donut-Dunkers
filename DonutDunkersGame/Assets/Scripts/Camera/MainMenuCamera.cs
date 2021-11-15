using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class MainMenuCamera : MonoBehaviour {

    public bool showingLevelSelect = false;
    public Transform mainCamPos, levelSelectCamPos;

    public float transitionTime = 1f;


    void Start() {
        LerpTransform(mainCamPos);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void GoToLevelSelect() {
        showingLevelSelect = true;
        LerpTransform(levelSelectCamPos);
    }

    public void GoToMainMenu() {
        showingLevelSelect = false;
        LerpTransform(mainCamPos);
    }


    void LerpTransform(Transform target) {
        Tween.Position(this.transform, target.position, transitionTime, 0, Tween.EaseInOut);
        Tween.Rotation(this.transform, target.rotation, transitionTime, 0, Tween.EaseInOut);
    }
}
