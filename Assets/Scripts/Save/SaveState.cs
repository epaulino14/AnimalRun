using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    [NonSerialized] private const int Accessories_Count = 16;
   public int HighScore { set; get; }
    public int Apples { set; get; }
    public DateTime LastSaveTime { set; get; }
    public int CurrentAccessoriesIndex { set; get; }
    public byte[] UnlockAccessoriesFlag { set; get; }

    public SaveState()
    {
        HighScore = 0;
        Apples = 0;
        LastSaveTime = DateTime.Now;
        CurrentAccessoriesIndex = 0;
        UnlockAccessoriesFlag = new byte[Accessories_Count];
        UnlockAccessoriesFlag[0] = 1;
    }
}


