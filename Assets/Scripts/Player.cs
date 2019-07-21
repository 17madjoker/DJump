using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRB2d;
    private float playerSpeed = 15f;
    private float horizontalAxes;
    private float maxJumpScore = 0f;
    private string name = "Jumper";
    private bool isPaused = false;
    private bool isLive = true;

    public float MaxJumpScore
    {
        get { return maxJumpScore; }
        private set { maxJumpScore = value; }
    }
    
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public bool IsLive
    {
        get { return isLive; }
        set { isLive = value; }
    }

    [SerializeField] 
    private Sprite playerUpMovement;
    [SerializeField] 
    private Sprite playerLeftMovement;
    [SerializeField] 
    private Text playerScore;
    [SerializeField] 
    private Text playerHighScore;
    
    private void Start()
    {
        LoadHighScore();
        
        playerRB2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckPause();
        
        CalculateScore(playerRB2d);
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().sprite = playerLeftMovement;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
        else if (Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().sprite = playerLeftMovement;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        else
        {
            GetComponent<SpriteRenderer>().sprite = playerUpMovement;
        }
        
        horizontalAxes = Input.GetAxis("Horizontal");
        playerRB2d.velocity = new Vector2(horizontalAxes * playerSpeed, playerRB2d.velocity.y);
    }

    private void CalculateScore(Rigidbody2D playerRB2d)
    {
        if (playerRB2d.velocity.y > 0 && transform.position.y > maxJumpScore)
        {
            maxJumpScore = transform.position.y;
        }

        playerScore.text = "Score: " + Math.Round(maxJumpScore);
    }

    private void LoadHighScore()
    {
        if (SaveLoadManager.isDataFileExists())
        {
            PlayerData bestPlayer = (PlayerData) SaveLoadManager.LoadBestPlayer();
            playerHighScore.text = "High Score: \n" + "<color=green>" + bestPlayer.playerName + " | " + bestPlayer.playerScore + "</color>";
        }
        else
        {
            SaveLoadManager.CreateEmptyPlayers();
        }
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPaused)
        {
            Time.timeScale = 0;
            GameObject.Find("pauseText").GetComponent<Text>().text = "Press <space> to play";
            
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPaused)
        {
            Time.timeScale = 1;
            GameObject.Find("pauseText").GetComponent<Text>().text = "Press <space> to pause";
            
            isPaused = false;
        }
    }
}
