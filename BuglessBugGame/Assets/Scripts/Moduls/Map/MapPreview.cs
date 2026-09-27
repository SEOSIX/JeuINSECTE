using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Moduls.Map
{
    public class MapPreview : MonoBehaviour
    {
        [SerializeField]private Image mapBg;
        [SerializeField]private TextMeshProUGUI mapName;
        [SerializeField]private TextMeshProUGUI mapDescription;

        public void SetUp(MapData mapData)
        {
            if (mapData != null)
            {
                mapBg.sprite = mapData.mapImage;
                mapName.text = mapData.mapName;
                mapDescription.text = mapData.mapDescription;
            }
        }
    }
}