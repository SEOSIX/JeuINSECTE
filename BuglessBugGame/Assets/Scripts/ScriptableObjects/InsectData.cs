using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Insect", menuName = "Data/Insect Data")]
public class InsectData : ScriptableObject
{
    public enum TypeCatch
    {
        Swipe,
        DrawCircle
    }

    public enum TypeMiniGame
    {
        Hider,
        Line,
        Jumper,
        Block
    }
    
    [Header("Infos générales")]
    public string insectName;
    [TextArea(3, 5)] public List<string> insectDescription;
    public Sprite insectIcon;
    public GameObject insectPrefab;
    public int catchCount;
    public bool isUnlocked;
    public int coinValueOnResell = 5;
    
    
    [Header("CatchMiniGame")]
    public GameObject catchMiniGamePrefabUI;
    public float bugSpeed;
    public int tryCount;
    public TypeCatch typeCatch;
    public TypeMiniGame typeMiniGame;
    
    public InsectHideData insectHideData;
    public InsectBlockingData insectBlockingData;
}

[Serializable]
public class InsectHideData
{
    [Range(0f, 1f)]public float changeDirectionPercentage;
    public float hideTime;
    public float hideChance;
    public float stopTime;
}

[Serializable]
public class InsectBlockingData
{
    [Header("MiniGameBlocking")]
    public float rayDistance = 80f;
    public float bounceConeAngle = 45f;
    public float staminaInsect = 7f;

    [Header("Smooth ZigZag Settings")]
    public float zigzagAmplitude = 22f;
    public float zigzagFrequency = 3f;
    public float organicVariation = 0.35f;

    [Header("Rotation Settings")]
    public float rotationOffset = -90f;
    public float rotationSpeed = 12f;
}
