using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject defaultPlatformPrefab;
    
    [SerializeField]
    private GameObject boostPlatformPrefab;

    [SerializeField] 
    private GameObject brokenPlatformPrefab;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        CreatePlatforms(collision);
    }

    private void CreatePlatforms(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("DefaultPlatform"))
        {
            if (Random.Range(0, 100) <= 5)
            {
                if (Random.Range(0, 2) == 0)
                {
                    Destroy(collision.gameObject);
                    CreatePlatform(collision, boostPlatformPrefab);
                }
                else
                {
                    Destroy(collision.gameObject);
                    CreatePlatform(collision, brokenPlatformPrefab);
                }
            }
            else
            {
                TransformPlatform(collision);
            }
        }
        else if (collision.gameObject.name.StartsWith("BoostPlatform"))
        {
            Destroy(collision.gameObject);
            CreatePlatform(collision, defaultPlatformPrefab);
        }        
        else if (collision.gameObject.name.StartsWith("BrokenPlatform"))
        {
            Destroy(collision.gameObject);
            CreatePlatform(collision, defaultPlatformPrefab);
        }
    }
    
    public void CreatePlatform(Collision2D collision, GameObject kindOfPlatform)
    {
        Instantiate(kindOfPlatform, 
            new Vector2(
                Random.Range(-3.75f, 3.75f), 
                player.transform.position.y + 3f + Random.Range(-0.25f, 0.25f)), 
            Quaternion.identity);
    }

    private void TransformPlatform(Collision2D collision)
    {
        collision.gameObject.transform.position = new Vector2(
            Random.Range(-3.75f, 3.75f), 
            player.transform.position.y + 3f + Random.Range(-0.25f, 0.25f));
    }
}
