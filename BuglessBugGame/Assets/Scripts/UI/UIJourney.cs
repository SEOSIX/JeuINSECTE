using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIJourney : MonoBehaviour
{
    [Header("RefUI")] [SerializeField] public GameObject _parentJourneySum;
    [SerializeField] private Transform bugSumContainer;
    [SerializeField] private Transform _bugBannerSpawn;
    [SerializeField] private Button _returnLobbyButton;
    
    [Header("RefPrefab")]
    [SerializeField] private GameObject bugSumPrefab;
    [SerializeField] private GameObject bugSumBannerPrefab;
    
    private List<InsectSlot> _insects = new List<InsectSlot>();
    private List<GameObject> _bugBanners = new List<GameObject>();
    
    private void OnEnable()
    {
        PlayerInsectPickUp.OnInsectPicked += SpawnInsectSumBanner;
    }

    private void OnDisable()
    {
        PlayerInsectPickUp.OnInsectPicked -= SpawnInsectSumBanner;
    }

    private void Start()
    {
        _returnLobbyButton.onClick.AddListener(ReturnLobby);
    }

    public void RefreshUI()
    {
        foreach (Transform child in bugSumContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (InsectSlot slot in GameManager.instance.player.playerData.playerInventoryData.insects)
        {
            if (slot.insect == null) continue;

            GameObject row = Instantiate(bugSumPrefab, bugSumContainer);
            BugSlotUI slotUI = row.GetComponent<BugSlotUI>();

            if (slotUI != null)
                slotUI.Setup(slot.insect, slot.count);
        }
    }
    private void ReturnLobby()
    {
        _parentJourneySum.SetActive(false);
        GameManager.instance.player.playerData.playerInventoryData.insects.Clear();
        SceneManager.LoadScene(GameManager.instance._lobbySceneName);
    }

    private void SpawnInsectSumBanner(InsectData insect, int count)
    {
        int maxBannersVisibles = 3;
        
        
        
        foreach (Transform child in _bugBannerSpawn)
        {
            Destroy(child.gameObject);
        }
        
        foreach (InsectSlot slot in GameManager.instance.player.playerData.playerInventoryData.insects)
        {
            if (slot.insect == null) continue;
            
            if (_insects.Count >= maxBannersVisibles)
            {
                GameObject oldBanner = _bugBanners[0];
                _bugBanners.RemoveAt(0);
                _insects.RemoveAt(0);
                Destroy(oldBanner);
            }
            
            GameObject row = Instantiate(bugSumBannerPrefab, _bugBannerSpawn);
            
            _bugBanners.Add(row);
            
            BugSlotUI slotUI = row.GetComponent<BugSlotUI>();

            if (slotUI != null)
                slotUI.Setup(slot.insect, slot.count);
            
            _insects.Add(slot);
        }
    }
}
