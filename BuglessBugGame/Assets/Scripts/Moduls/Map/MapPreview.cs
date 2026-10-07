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
        [Header("Insectes")]
        [SerializeField] private Transform insectIconContainer;
        [SerializeField] private GameObject insectIconPrefab;
        
        public Button startMap;
        public Button backToMapSelection;

        public void SetUp(MapData mapData)
        {
            if (mapData != null)
            {
                mapBg.sprite = mapData.mapImage;
                mapName.text = mapData.mapName;
                mapDescription.text = mapData.mapDescription;
                DisplayInsectIcons(mapData);
            }
        }
        
        private void DisplayInsectIcons(MapData mapData)
        {
            foreach (Transform child in insectIconContainer)
            {
                Destroy(child.gameObject);
            }

            if (mapData.insectsAvailable == null) return;

            foreach (InsectData insect in mapData.insectsAvailable)
            {
                if (insect == null || insect.insectIcon == null) continue;

                GameObject iconInstance = Instantiate(insectIconPrefab, insectIconContainer);
                iconInstance.GetComponent<Image>().sprite = insect.insectIcon;
            }
        }
        
    }
}