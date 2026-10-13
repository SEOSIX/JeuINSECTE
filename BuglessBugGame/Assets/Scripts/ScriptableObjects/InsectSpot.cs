using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InsectSpot", menuName = "Data")]
public class InsectSpot : ScriptableObject
{
    public List<Spot> insectSpots;
}

[Serializable]
public class Spot
{
    public InsectData insectData;
    [Range(0f, 1f)] public float insectChanceSpawn;
}
