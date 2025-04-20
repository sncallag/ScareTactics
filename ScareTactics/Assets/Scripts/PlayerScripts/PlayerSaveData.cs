using System;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData 
{
    public string characterId;
    public float health;
    public int level;
    public int experience;
    public Vector3 savedPosition;

    public PlayerSaveData(string id, float hp, int lvl, int xp, Vector3 pos)
    {
        characterId = id;
        health = hp;
        level = lvl;
        experience = xp;
        savedPosition = pos;
  
    }
}
