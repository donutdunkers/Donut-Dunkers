using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float killRadius = 25;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.magnitude > killRadius) {
            Destroy(gameObject);
        }
    }
}
