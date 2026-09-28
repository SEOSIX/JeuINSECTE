using System.Collections.Generic;
using Moduls.Map;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct MapButtonEntry
{
    public Button button;
    public MapData mapPreviewData;
}
public class JourneyMapManage : MonoBehaviour
{
    [SerializeField] private List<MapButtonEntry> mapEntries = new List<MapButtonEntry>();
    [SerializeField] private GameObject mapPreviewPrefab;
    [SerializeField] private Transform previewContainer;
    [SerializeField] private Button returnToLobbyButton;
    
    private GameObject currentPreviewInstance;
    private MapData currentSelectedMapData;
    
    private void Awake()
    {
        foreach (var entry in mapEntries)
        {
            MapData data = entry.mapPreviewData;
            entry.button.onClick.AddListener(() => OnMapButtonClicked(data));
        }

        returnToLobbyButton.onClick.AddListener(() => ReturnToLobby());
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
            preview.startMap.onClick.AddListener(() => LoadMap());
            preview.backToMapSelection.onClick.AddListener(() => ClosePreview());
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
    
    private void ClosePreview()
    {
        if (currentPreviewInstance != null)
        {
            Destroy(currentPreviewInstance);
            currentPreviewInstance = null;
        }

        currentSelectedMapData = null;
    }

    private void ReturnToLobby()
    {
        GameManager.instance.M_UI.lobby._parentMapUI.SetActive(false);
        GameManager.instance.M_UI.lobby._parentLobbyUI.SetActive(true);
    }
}
