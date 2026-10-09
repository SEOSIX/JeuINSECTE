using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data / Terrarium", fileName = "Terrarium Data")]
public class TerrariumData : ScriptableObject
{
    [Header("Components")] 
    [Range(0, 4)] public int terrariumLevel;
    public List<InsectData> insects = new List<InsectData>();

    [Header("ShopValue")] 
    public int price;
}
