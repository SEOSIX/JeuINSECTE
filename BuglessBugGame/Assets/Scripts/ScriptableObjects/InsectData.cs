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
    [TextArea(3, 5)] public string insectDescription;
    public Sprite insectIcon;
    public GameObject insectPrefab;
    public int maxStackable;
    
    [Header("CatchMiniGame")]
    public GameObject catchMiniGamePrefabUI;
    public float bugSpeed;
    public float hidePercentage;
    public float hideTime;
    public float stopTime;
    public int tryCount;
    public TypeCatch typeCatch;
}
