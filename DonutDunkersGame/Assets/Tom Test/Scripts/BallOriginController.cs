using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOriginController : MonoBehaviour
{
    public float speed;

    public Vector2 HorizontalRange = new Vector2(-4, 4);
    public Vector2 VerticalRange = new Vector2(-4, 4);

    Vector2 movement = new Vector2(0, 0);
    //translates the ball on the x, y plane
    public void HandleMove(Vector2 input) {
        movement = input;
    }

    private void FixedUpdate() {
        MoveOnPlane(movement);
    }

    private void MoveOnPlane(Vector2 movement) {
        Vector3 newPosition = transform.position;
        
        newPosition.x += movement.x * Time.deltaTime * speed;
        newPosition.y += movement.y * Time.deltaTime * speed;
        //clamp inside range
        newPosition.x = Mathf.Clamp(newPosition.x, HorizontalRange.x, HorizontalRange.y);
        newPosition.y = Mathf.Clamp(newPosition.y, VerticalRange.x, VerticalRange.y);
        
        transform.position = newPosition;
    }

    public void HandleReset() {
        transform.localPosition = new Vector3(0, 0, transform.position.z);
    }
}


