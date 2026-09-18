using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DG.Tweening.Core;

public class UIJourney : MonoBehaviour
{
    [Header("RefUI")]
    [SerializeField] public GameObject _parentJourney;
    [SerializeField] public GameObject _parentJourneySum;
    [SerializeField] private Transform bugSumContainer;
    [SerializeField] private Transform _bugBannerSpawn;
    [SerializeField] private Button _returnLobbyButton;
    [SerializeField] private Button _leaveButton;
    
    [Header("RefPrefab")]
    [SerializeField] private GameObject bugSumPrefab;
    [SerializeField] private GameObject bugSumBannerPrefab;
    
    [Header("Banner Settings")]
    [SerializeField] private float bannerLifetime = 3f;
    [SerializeField] private float bannerOffscreenOffsetX = -500f;
    [SerializeField] private float bannerMoveInDuration = 0.4f;
    [SerializeField] private Ease bannerMoveInEase = Ease.OutCubic;
    
    
    
    [Header("SummaryRevealSettings")]
    [SerializeField] private float summaryRevealDelay = 0.3f;
    [SerializeField] private float scaleUpDuration = 0.35f;
    [SerializeField] private Ease scaleUpEase = Ease.OutBack;
    [SerializeField] private float startRotationZ = -15f;
    [SerializeField] private float punchRotationStrength = 8f;
    [SerializeField] private float punchRotationDuration = 0.25f;
    
    private bool _summaryParentScaleCached = false;
    private string bannerVisualChildName = "Visual";
    
    private Coroutine _summaryRoutine;
    private Vector3 _summaryParentOriginalScale;
    
    private List<GameObject> _summaryRows = new List<GameObject>();
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
        _leaveButton.onClick.AddListener(SummaryJourney);
        _returnLobbyButton.onClick.AddListener(ReturnLobby);
    }

    public void RefreshUI()
    {
        foreach (Transform child in bugSumContainer)
        {
            Destroy(child.gameObject);
        }
        
        //Instanciate IN UI BugsCollected
        foreach (InsectSlot slot in GameManager.instance.player.playerData.playerInventoryData.insects)
        {
            if (slot.insect == null) continue;

            GameObject row = Instantiate(bugSumPrefab, bugSumContainer);
            BugSlotUI slotUI = row.GetComponent<BugSlotUI>();

            if (slotUI != null)
                slotUI.Setup(slot.insect, slot.count);
            
            
            row.transform.localScale = Vector3.zero;
            row.SetActive(false);
            _summaryRows.Add(row);
        }
    }

    #region Banner
    
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

                ShowBanner(row);
                
                InsectSlot slotData = new InsectSlot { insect = insect, count = count };
                _insects.Add(slotData);

                _bannerCoroutines[row] = StartCoroutine(RemoveBannerAfterDelay(row, bannerLifetime));
            }
        }

        private IEnumerator RemoveBannerAfterDelay(GameObject banner, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (banner == null) yield break;
            yield return HideBanner(banner);

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

        private IEnumerator HideBanner(GameObject banner)
        {
            Transform visual = banner.transform.Find(bannerVisualChildName);
            if (visual == null) visual = banner.transform;

            RectTransform rt = visual.GetComponent<RectTransform>();
            if (rt == null) yield break;

            Vector2 startPos = rt.anchoredPosition;
            Vector2 targetPos = startPos;
            targetPos.x = startPos.x + bannerOffscreenOffsetX;

            rt.DOKill();
            yield return rt.DOAnchorPosX(targetPos.x, bannerMoveInDuration)
                .SetEase(bannerMoveInEase)
                .WaitForCompletion();
        }

        private void ShowBanner(GameObject banner)
        {
            Transform visual = banner.transform.Find(bannerVisualChildName);
            if (visual == null) visual = banner.transform;

            RectTransform rt = visual.GetComponent<RectTransform>();
            if (rt == null) return;

            Vector2 targetPos = rt.anchoredPosition;
            Vector2 startPos = targetPos;
            startPos.x = targetPos.x + bannerOffscreenOffsetX;

            rt.DOKill();
            rt.anchoredPosition = startPos;
            rt.DOAnchorPosX(targetPos.x, bannerMoveInDuration).SetEase(bannerMoveInEase);
        }
    
    #endregion
    
    
    private void ReturnLobby()
    {
        _parentJourneySum.gameObject.SetActive(false);
        GameManager.instance.player.enabled = true;
        GameManager.instance.player.playerData.playerInventoryData.insects.Clear();
        SceneManager.LoadScene(GameManager.instance._lobbySceneName);
    }
    
    private void SummaryJourney()
    {
        GameObject parent = GameManager.instance.M_UI.journey._parentJourneySum;

        if (!_summaryParentScaleCached)
        {
            _summaryParentOriginalScale = parent.transform.localScale;
            _summaryParentScaleCached = true;
        }

        parent.transform.localScale = Vector3.zero;
        parent.SetActive(true);
        parent.transform.DOScale(_summaryParentOriginalScale, scaleUpDuration).SetEase(scaleUpEase);

        if (_summaryRoutine != null)
            StopCoroutine(_summaryRoutine);

        _summaryRoutine = StartCoroutine(ShowSummary());
    }
    
    private IEnumerator ShowSummary()
    {
        yield return new WaitForSeconds(summaryRevealDelay);
        
        foreach (GameObject row in _summaryRows)
        {
            if (row == null) continue;

            Transform t = row.transform;

            row.SetActive(true);
            t.localScale = Vector3.zero;
            t.localRotation = Quaternion.Euler(0f, 0f, startRotationZ);

            Sequence seq = DOTween.Sequence();
            seq.Join(t.DOScale(Vector3.one, scaleUpDuration).SetEase(scaleUpEase));
            seq.Join(t.DOLocalRotate(Vector3.zero, scaleUpDuration).SetEase(scaleUpEase));
            seq.Append(t.DOPunchRotation(new Vector3(0f, 0f, punchRotationStrength), punchRotationDuration, 6, 0.8f));

            seq.SetTarget(row);

            yield return new WaitForSeconds(summaryRevealDelay);
        }
        _summaryRoutine = null;
    }
}