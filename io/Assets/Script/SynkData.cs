using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynkData 
{
    public Vector2Int[] Position;
    public int[] Score;

    public static object Deserialyze(byte[] bytes)
    {
        SynkData data = new SynkData();

        int players = bytes.Length / 4;

        data.Score = new int[players];

        for (int i = 0; i < data.Score.Length; i++) 
        {
            data.Score[i] = BitConverter.ToInt32(bytes, 8 * players + 4 * i);
        }
        
        return data;
    }

    public static byte[] Serialyze(object obj)
    {
        SynkData data = (SynkData)obj;

        byte[] result = new byte
            [
                4 * data.Score.Length
            ];

        for(int i = 0; i < data.Score.Length; i++)
        {
            BitConverter.GetBytes(data.Score[i]).CopyTo(result, 4 * i);
        }

        return result;
    }
}
