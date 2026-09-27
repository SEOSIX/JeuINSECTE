using Moduls.Map;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JourneyMapManage : MonoBehaviour
{
    public class MapButtonEntry
    {
        public Button button;
        public MapData mapPreviewData;
    }
    
    [SerializeField] private MapButtonEntry[] mapEntries;
    
    [SerializeField] private GameObject mapPreviewPrefab;
    [SerializeField] private Transform previewContainer;
    
    private GameObject currentPreviewInstance;
    private MapData currentSelectedMapData;
    
    private void Awake()
    {
        foreach (var entry in mapEntries)
        {
            MapData data = entry.mapPreviewData;
            entry.button.onClick.AddListener(() => OnMapButtonClicked(data));
        }
    }

    private void OnMapButtonClicked(MapData mapData)
    {
        currentSelectedMapData = mapData;
        SpawnCurrentSelectedMapPreview(mapData);
    }
    
    private void SpawnCurrentSelectedMapPreview(MapData mapData)
    {
        if (mapData == null) return;
        
        if (currentPreviewInstance != null)
        {
            Destroy(currentPreviewInstance);
        }

        currentPreviewInstance = Instantiate(mapPreviewPrefab, previewContainer);

        Moduls.Map.MapPreview preview = currentPreviewInstance.GetComponent<Moduls.Map.MapPreview>();
        if (preview != null)
        {
            preview.SetUp(mapData);
        }
    }
    
    private void LoadMap()
    {
        if (currentSelectedMapData == null)
        {
            Debug.LogWarning("Aucune map sélectionnée.");
            return;
        }
        SceneManager.LoadScene(currentSelectedMapData.mapName);
    }
}
