using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEditor;

public static class Database
{
    /// <summary>
    /// Save data each map
    /// </summary>
    public static int[] Level = new int[14];
    //--------------------
    public static Vector2[] PositionHoles { get { return positionHoles; } }
    
    static Vector2[] positionHoles = new Vector2[11]
    {
         //new Vector2(-4,1),   new Vector2(0,1), new Vector2(4,1),
         new Vector2(-5,-0.7f),   new Vector2(-1,-0.7f),  new Vector2(3,-0.7f),
         new Vector2(-7,-2.15f), new Vector2(-3,-2.15f),  new Vector2(1,-2.15f), new Vector2(5,-2.15f),
         new Vector2(-5,-3.7f),    new Vector2(-1,-3.7f),new Vector2(3,-3.7f), new Vector2(7,-3.7f)
    };
    public static Vector3Int Target(int level) {
        if (level <= target.Length) return target[level];
        else return Vector3Int.zero;
    }
    static Vector3Int[] target = new Vector3Int[14]
    {
        new Vector3Int(10,0,0),   new Vector3Int(11,1,0),
        new Vector3Int(12,2,0),  new Vector3Int(13,3,0),  new Vector3Int(14,2,1),
        new Vector3Int(15,5,1),  new Vector3Int(16,6,4),  new Vector3Int(18,6,4),
        new Vector3Int(18,7,5),  new Vector3Int(19,7,6),  new Vector3Int(19,8,7),
        new Vector3Int(19,8,7),  new Vector3Int(19,9,8),  new Vector3Int(20,10,9),
    };

    public static Vector2 TimeHole(int level)
    {
        if (level > timeHoles.Length) return timeHoles[timeHoles.Length];
        else
            return timeHoles[level];
    }
    static Vector2[] timeHoles = new Vector2[14]
    {
        new Vector2(3,2), new Vector2(1f, 0.8f), new Vector2(1.2f, 0.5f),
        new Vector2(1.8f, 0.8f), new Vector2(1.4f, 0.6f), new Vector2(2,3),
        new Vector2(1.8f, 2),
        new Vector2(3,2), new Vector2(1f, 0.8f), new Vector2(1.2f, 0.5f),
        new Vector2(1.8f, 0.8f), new Vector2(1.4f, 0.6f), new Vector2(2,3),
        new Vector2(1.8f, 2),
    };
    //--------------------
    static string[] starMap;
    
    public static void Savedata()
    {
        for(int i = 0; i < 14; i++)
        {
            string star = "start" + (i);
            PlayerPrefs.SetInt(star, Level[i]);
        }
    }

    public static void Loaddata()
    {
        bool start = false;
        for (int i = 0; i < 14; i++)
        {
            string star = "start" + (i);
            Level[i] = PlayerPrefs.GetInt(star);
        }
        for(int i = 0; i < 14; i++)
        {
            if (Level[i] != 0)
            {
                start = true; break;
            }
        }
        if (!start) Level[0] = 1;
    }
}
