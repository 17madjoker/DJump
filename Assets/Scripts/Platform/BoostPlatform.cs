using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        Rigidbody2D platformRB2d = collider.gameObject.GetComponent<Rigidbody2D>();
        float jumpForce = 900f;
        
        if (platformRB2d.velocity.y <= 0)
        {
            platformRB2d.AddForce(Vector3.up * jumpForce);
        }
    }
}
