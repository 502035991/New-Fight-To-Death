using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSaveData
{
    public string characterName = "Character";
    [Header("��Ϸʱ��")]
    public float secondsPlayed;
    [Header("λ������")]
    public float xPosition = 0;
    public float yPosition = 0;
    public float zPosition = 0;

    [Header("��ǰ״̬")]
    public int currentHealth;
    public float currentStamina;

    [Header("״̬")]
    public int vitality;//Ѫ��
    public int endurance = 100;//����
}
