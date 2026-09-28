using System.Collections.Generic;
using GamePlayCore;
using UnityEngine;


[CreateAssetMenu(fileName = "Map", menuName = "Data/Map Data")]
public class MapData : ScriptableObject
{
    public Sprite mapImage;
    public string mapName;
    [TextArea(3, 4)]public string mapDescription;
    public List<InsectData> insectsAvailable;
}
