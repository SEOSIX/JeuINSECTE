using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIJourney : MonoBehaviour
{
    [Header("RefUI")]
    [SerializeField] public GameObject _parentJourney;
    [SerializeField] public GameObject _parentJourneySum;
    [SerializeField] private Transform bugSumContainer;
    [SerializeField] private Transform _bugBannerSpawn;
    [SerializeField] private Button _returnLobbyButton;
    
    [Header("RefPrefab")]
    [SerializeField] private GameObject bugSumPrefab;
    [SerializeField] private GameObject bugSumBannerPrefab;
    
    [Header("Banner Settings")]
    [SerializeField] private float bannerLifetime = 3f;
    
    private List<InsectSlot> _insects = new List<InsectSlot>();
    private List<GameObject> _bugBanners = new List<GameObject>();
    private Dictionary<GameObject, Coroutine> _bannerCoroutines = new Dictionary<GameObject, Coroutine>();

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
        _parentJourneySum.gameObject.SetActive(false);
        GameManager.instance.player.playerData.playerInventoryData.insects.Clear();
        SceneManager.LoadScene(GameManager.instance._lobbySceneName);
    }

    private void SpawnInsectSumBanner(InsectData insect, int count)
    {
        int existingIndex = _insects.FindIndex(s => s.insect == insect);

        if (existingIndex != -1)
        {
            InsectSlot slot = _insects[existingIndex];
            slot.count += count;
            _insects[existingIndex] = slot;

            GameObject existingBanner = _bugBanners[existingIndex];
            BugSlotUI slotUI = existingBanner.GetComponent<BugSlotUI>();
            if (slotUI != null)
                slotUI.Setup(slot.insect, slot.count);
            if (_bannerCoroutines.TryGetValue(existingBanner, out Coroutine activeCoroutine))
            {
                if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            }
            _bannerCoroutines[existingBanner] = StartCoroutine(RemoveBannerAfterDelay(existingBanner, bannerLifetime));
        }
        else
        {
            int maxBannersVisibles = 3;
            if (_bugBanners.Count >= maxBannersVisibles)
            {
                GameObject oldBanner = _bugBanners[0];
                _bugBanners.RemoveAt(0);
                if (_insects.Count > 0) _insects.RemoveAt(0);

                if (oldBanner != null)
                {
                    if (_bannerCoroutines.TryGetValue(oldBanner, out Coroutine oldCoroutine))
                    {
                        if (oldCoroutine != null) StopCoroutine(oldCoroutine);
                        _bannerCoroutines.Remove(oldBanner);
                    }
                    Destroy(oldBanner);
                }
            }

            GameObject row = Instantiate(bugSumBannerPrefab, _bugBannerSpawn);
            _bugBanners.Add(row);

            BugSlotUI slotUI = row.GetComponent<BugSlotUI>();
            if (slotUI != null)
                slotUI.Setup(insect, count);

            InsectSlot slotData = new InsectSlot { insect = insect, count = count };
            _insects.Add(slotData);

            _bannerCoroutines[row] = StartCoroutine(RemoveBannerAfterDelay(row, bannerLifetime));
        }
    }

    private IEnumerator RemoveBannerAfterDelay(GameObject banner, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (banner == null) yield break;

        int index = _bugBanners.IndexOf(banner);
        if (index != -1)
        {
            _bugBanners.RemoveAt(index);
            if (index < _insects.Count) _insects.RemoveAt(index);
        }

        if (_bannerCoroutines.ContainsKey(banner)) _bannerCoroutines.Remove(banner);

        Destroy(banner);
    }
}