using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSaveData
{
    public string characterName = "Character";
    [Header("游戏时间")]
    public float secondsPlayed;
    [Header("位置坐标")]
    public float xPosition = 0;
    public float yPosition = 0;
    public float zPosition = 0;

    [Header("当前状态")]
    public int currentHealth;
    public float currentStamina;

    [Header("状态")]
    public int vitality;//血量
    public int endurance = 100;//耐力
}
