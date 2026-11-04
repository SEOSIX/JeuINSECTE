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
        Jumper
    }
    
    [Header("Infos générales")]
    public string insectName;
    [TextArea(3, 5)] public List<string> insectDescription;
    public Sprite insectIcon;
    public GameObject insectPrefab;
    public int catchCount;
    public bool isUnlocked;
    
    [Header("CatchMiniGame")]
    public GameObject catchMiniGamePrefabUI;
    public float bugSpeed;
    [Range(0f, 1f)]public float changeDirectionPercentage;
    public float hideTime;
    public float hideChance;
    public float stopTime;
    public int tryCount;
    public TypeCatch typeCatch;
    public TypeMiniGame typeMiniGame;
}
