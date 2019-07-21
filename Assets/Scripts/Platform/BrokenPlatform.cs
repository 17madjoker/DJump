using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    [SerializeField] 
    private GameObject defaultPlatformPrefab;
    
    private void OnCollisionEnter2D(Collision2D collider)
    {
        Rigidbody2D platformRB2d = collider.gameObject.GetComponent<Rigidbody2D>();
        GameObject PlatformManager = GameObject.Find("PlatformManager");
        
        if (platformRB2d.velocity.y <= 0)
        {
            PlatformManager.GetComponent<PlatformManager>().CreatePlatform(collider, defaultPlatformPrefab);
           
            Destroy(gameObject);
        }
    }
}
