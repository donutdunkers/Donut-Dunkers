using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[SelectionBaseAttribute]
public class AimController : MonoBehaviour{
    enum AimState {
        AimingBallOrigin,
        AimingArrowOrigin,
        Locked
    }

    AimState currAimState = AimState.AimingBallOrigin;


    public ArrowOriginController arrowOriginController;
    public BallOriginController ballOriginController;

    [SerializeField]
    private float aimSpeed = 1.0f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void OnMove(InputValue value){
        Vector2 input = value.Get<Vector2>();
        if (currAimState == AimState.AimingBallOrigin){
           ballOriginController.HandleMove(input);
        }
        else if (currAimState == AimState.AimingArrowOrigin){
           arrowOriginController.HandleMove(input);
        }
    }

    public void OnRotate(InputValue value){
        Vector2 input = value.Get<Vector2>();
        arrowOriginController.HandleMove(input); 
    }

     public void OnReset(){
           ballOriginController.HandleReset();
           arrowOriginController.HandleReset();
        
    }
    

    public void OnSwitchModes(InputValue value) {
        Debug.Log("Switching mode");
        // toggle aim state
        if (currAimState == AimState.AimingBallOrigin) {
            currAimState = AimState.AimingArrowOrigin;
        } else {
            currAimState = AimState.AimingBallOrigin;
        }
    }

    public void OnEscape() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
    
}
