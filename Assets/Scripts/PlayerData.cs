using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int points;
    public int level;

    public PlayerData(LevelManager level)
    {
        points = level.points;
    }
}
