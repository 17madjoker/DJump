using System;

[Serializable]
public struct PlayerData
{
    public string playerName;
    public int playerScore;

    public PlayerData(Player player)
    {
        playerName = player.Name;
        playerScore = (int) player.MaxJumpScore;
    }
}
