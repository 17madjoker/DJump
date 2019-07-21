using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SavePlayer(Player currentPlayer)
    {
        string pathToFile = Application.persistentDataPath + "/statistics.djump";
        
        if (File.Exists(pathToFile))
        {
            List<PlayerData> playersData = LoadPlayers();
            List<PlayerData> tempPlayersData = LoadPlayers();

            foreach (PlayerData player in playersData)
            {
                if (currentPlayer.MaxJumpScore >= player.playerScore)
                {
                    SwapPlayer(pathToFile, playersData, player, currentPlayer, tempPlayersData);
                    break;
                }
            }
        }
        else
        {
            CreateEmptyPlayers();
        }
    }

    public static void SwapPlayer(
        string pathToFile, 
        List<PlayerData> playersData, 
        PlayerData player, 
        Player currentPlayer, 
        List<PlayerData> tempPlayersData)
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = new FileStream(pathToFile, FileMode.Create);

        int indexSwap = playersData.IndexOf(player);
                    
        PlayerData newRecordPlayer = new PlayerData();
        newRecordPlayer.playerName = currentPlayer.Name;
        newRecordPlayer.playerScore = (int) currentPlayer.MaxJumpScore;
                    
        //tempPlayersData[indexSwap] = newRecordPlayer;
        tempPlayersData.Insert(indexSwap, newRecordPlayer);  
        tempPlayersData.Remove(tempPlayersData.Last());
               
        bFormatter.Serialize(fStream, tempPlayersData);
        fStream.Close();
    }

    public static List<PlayerData> LoadPlayers()
    {
        string pathToFile = Application.persistentDataPath + "/statistics.djump";

        if (File.Exists(pathToFile))
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            FileStream fStream = new FileStream(pathToFile, FileMode.Open);

            List<PlayerData> playersData = (List<PlayerData>) bFormatter.Deserialize(fStream);
            fStream.Close();

            return playersData;
        }
        CreateEmptyPlayers();
        
        return null;
    }

    public static Nullable<PlayerData> LoadBestPlayer()
    {
        string pathToFile = Application.persistentDataPath + "/statistics.djump";

        if (File.Exists(pathToFile))
        {
            List<PlayerData> playersData = LoadPlayers();

            return playersData[0];
        }
        
        CreateEmptyPlayers();
        
        return null;
    }

    public static List<PlayerData> CreateEmptyPlayers()
    {
        string pathToFile = Application.persistentDataPath + "/statistics.djump";
        
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream fStream = new FileStream(pathToFile, FileMode.Create);
        
        List<PlayerData> playersData = new List<PlayerData>();

        for (int i = 0; i < 10; i++)
        {
            PlayerData playerData = new PlayerData();
            playerData.playerName = "unknownPlayer" + (i + 1);
            playerData.playerScore = 10 - i;
            playersData.Add(playerData);
        }
        
        bFormatter.Serialize(fStream, playersData);
        fStream.Close();

        return playersData;
    }

    public static bool isDataFileExists()
    {
        string pathToFile = Application.persistentDataPath + "/statistics.djump";

        if (File.Exists(pathToFile))
        {
            return true;
        }
        
        return false;
    }

    public static bool isPlayerBeatRecord(Player currentPlayer)
    {
        List<PlayerData> playersData = LoadPlayers();
        bool isBeatRecord = false;
        
        foreach (PlayerData player in playersData)
        {
            if (currentPlayer.MaxJumpScore >= player.playerScore)
            {
                isBeatRecord = true;
                break;
            }
        }

        if (isBeatRecord)
            return true;

        return false;
    }
}
