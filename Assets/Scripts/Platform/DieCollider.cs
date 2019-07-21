using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieCollider : MonoBehaviour
{
    private float maxPlayerPositionY;
    public Player player;
    public GameObject playerInputName;

    private void Update()
    {
        maxPlayerPositionY = player.GetComponent<Player>().MaxJumpScore;
        
        DieColliderPosition(maxPlayerPositionY);
        CheckGameOver(player);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {       
        if (collider.gameObject.name == "Player")
        {
            player.IsLive = false;
        }
    }

    private void CheckGameOver(Player player)
    {
        if (player.IsLive == false)
        {         
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            player.GetComponent<SpriteRenderer>().enabled = false;
            
            if (SaveLoadManager.isPlayerBeatRecord(player))
            {
                playerInputName.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    player.Name = GameObject.Find("InputName").GetComponent<Text>().text;
                    
                    SaveLoadManager.SavePlayer(player);
                    SceneManager.LoadScene("Statistics");
                }
            }
            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    
    private void DieColliderPosition(float maxPlayerPositionY)
    {
        transform.position = new Vector3(
            transform.position.x,
            maxPlayerPositionY - 16f,
            transform.position.z);
    }
}
