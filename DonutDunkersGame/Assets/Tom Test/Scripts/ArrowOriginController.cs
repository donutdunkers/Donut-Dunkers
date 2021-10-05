using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOriginController : MonoBehaviour {
    //rotates the aiming direction

    public float speed;

    public Vector2 maxAngles = new Vector2(45, 45);
    Vector2 movement = new Vector2(0, 0);
    public void HandleMove(Vector2 input) {
        //rotation axes must be flipped
        movement = new Vector2(-input.y, input.x);
    }

    private void FixedUpdate() {
        Rotate(movement);
    }
    private void Rotate(Vector2 movement) {

        Vector3 newRotation = transform.eulerAngles;
    
        
        newRotation.y += movement.y * speed * Time.deltaTime;
        newRotation.x += movement.x * speed * Time.deltaTime;

        // TODO: this breaks the rotation, need to find a solution using only Quaternions?
        // newRotation.x = Mathf.Clamp(newRotation.x, -maxAngles.x, maxAngles.x);
        // newRotation.y = Mathf.Clamp(newRotation.y, -maxAngles.y, maxAngles.y);
    
        transform.rotation = Quaternion.Euler(newRotation);
    }

    public void HandleReset() {
        transform.rotation = Quaternion.identity;
    }
}

        
