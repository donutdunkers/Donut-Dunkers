using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour {


    public float speed = 10f;
    public float cooldown = 0.5f;
    float currCooldown = 0f;
    public GameObject projectile;
    public Transform projectileParent;


    public void Update() {
        if (currCooldown > 0) {
            currCooldown -= Time.deltaTime;
        }
    }
    public void OnFire(InputValue value) {
        if (currCooldown <= 0f) {
            GameObject ball = Instantiate(projectile, transform.position, Quaternion.identity, projectileParent);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            currCooldown = cooldown;
            rb.AddForce(speed * transform.forward, ForceMode.Impulse);
        }
    }

}