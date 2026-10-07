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
    
    [Header("Infos générales")]
    public string insectName;
    [TextArea(3, 5)] public List<string> insectDescription;
    public Sprite insectIcon;
    public GameObject insectPrefab;
    public int catchCount;
    
    [Header("CatchMiniGame")]
    public GameObject catchMiniGamePrefabUI;
    public float bugSpeed;
    [Range(0f, 1f)]public float changeDirectionPercentage;
    public float hideTime;
    public float stopTime;
    public int tryCount;
    public TypeCatch typeCatch;
}
