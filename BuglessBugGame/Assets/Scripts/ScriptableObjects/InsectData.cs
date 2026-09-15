using UnityEngine;

[CreateAssetMenu(fileName = "Insect", menuName = "Data/Insect Data")]
public class InsectData : ScriptableObject
{
    [Header("Infos générales")]
    public string insectName;
    [TextArea(3, 5)] public string insectDescription;
    public Sprite insectIcon;
    public GameObject insectPrefab;
    public int maxStackable;
}
