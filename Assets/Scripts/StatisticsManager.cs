using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    private Text playersStatistics;
    
    private void Start()
    {
        playersStatistics = GetComponent<Text>();

        LoadStatistics();
    }

    private void LoadStatistics()
    {
        if (SaveLoadManager.isDataFileExists())
        {
            List<PlayerData> playersData = SaveLoadManager.LoadPlayers();

            playersStatistics.text = "<color=#821D1B>Player name | score</color> \n";
        
            foreach (PlayerData player in playersData)
            {
                playersStatistics.text += player.playerName + " ------ " + player.playerScore + "\n";
            }
        }
        else
        {
            SaveLoadManager.CreateEmptyPlayers();
        }
    }
}
