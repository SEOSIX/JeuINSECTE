using System.Collections.Generic;
using Moduls;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EncyclopediaData data;

    [Header("ParentsUI")]
    [SerializeField] private GameObject encyclopediaUI;

    [Header("Buttons")]
    [SerializeField] private Button openBookButton;
    [SerializeField] private Button closeBookButton;
    [SerializeField] private Button nextPageBookButton;
    [SerializeField] private Button previousPageBookButton;

    [Header("Pages")]
    [SerializeField] private EncyclopediaPage leftPage;
    [SerializeField] private EncyclopediaPage rightPage;

    [Header("Prefab")]
    [SerializeField] private GameObject insectPrefab;

    [Header("Pool")]
    [SerializeField] private Transform cellPool;

    public EncyclopediaData Data => data;

    private const int InsectsPerSpread = EncyclopediaPage.MaxInsectsPerPage * 2;
    private int currentSpread = 0;

    private readonly List<EncyclopediaCell> allCells = new List<EncyclopediaCell>();

    private void OnEnable()
    {
        PlayerInsectPickUp.OnInsectPicked += AddToBook;
    }

    private void OnDisable()
    {
        PlayerInsectPickUp.OnInsectPicked -= AddToBook;
    }

    void Awake()
    {
        openBookButton.onClick.AddListener(OpenBook);
        closeBookButton.onClick.AddListener(CloseBook);
        nextPageBookButton.onClick.AddListener(NextSpread);
        previousPageBookButton.onClick.AddListener(PreviousSpread);
    }

    void OpenBook()
    {
        if (encyclopediaUI != null)
            encyclopediaUI.SetActive(true);

        leftPage.ShowGrid();
        rightPage.ShowGrid();
        RefreshCurrentSpread();
    }

    void CloseBook()
    {
        if (encyclopediaUI != null)
            encyclopediaUI.SetActive(false);
    }

    void AddToBook(InsectData insectData, int count = 1)
    {
        if (data == null) return;

        if (!data.encyclopediaData.Contains(insectData))
        {
            data.encyclopediaData.Add(insectData);

            GameObject newInsect = Instantiate(insectPrefab, cellPool); // parent = pool au départ
            EncyclopediaCell cell = newInsect.GetComponent<EncyclopediaCell>();
            cell.SetupInsectEncyclopedia(insectData);
            cell.OnCellClicked += (insect) => HandleCellClicked(insect, cell);

            allCells.Add(cell);

            if (encyclopediaUI != null && encyclopediaUI.activeInHierarchy)
                RefreshCurrentSpread();
        }
    }

    private void RefreshCurrentSpread()
    {
        leftPage.ShowGrid();
        rightPage.ShowGrid();

        int startIndex = currentSpread * InsectsPerSpread;

        for (int i = 0; i < allCells.Count; i++)
        {
            EncyclopediaCell cell = allCells[i];

            if (i >= startIndex && i < startIndex + EncyclopediaPage.MaxInsectsPerPage)
            {
                cell.transform.SetParent(leftPage.Container, false);
                cell.gameObject.SetActive(true);
            }
            else if (i >= startIndex + EncyclopediaPage.MaxInsectsPerPage && i < startIndex + InsectsPerSpread)
            {
                cell.transform.SetParent(rightPage.Container, false);
                cell.gameObject.SetActive(true);
            }
            else
            {
                cell.transform.SetParent(cellPool, false);
                cell.gameObject.SetActive(false);
            }
        }
    }

    private void HandleCellClicked(InsectData insect, EncyclopediaCell cell)
    {
        if (cell.transform.parent == leftPage.Container)
            rightPage.ShowDetail(insect);
        else if (cell.transform.parent == rightPage.Container)
            leftPage.ShowDetail(insect);
    }

    public void NextSpread()
    {
        int maxSpread = Mathf.Max(0, (allCells.Count - 1) / InsectsPerSpread);
        if (currentSpread < maxSpread)
        {
            currentSpread++;
            RefreshCurrentSpread();
        }
    }

    public void PreviousSpread()
    {
        if (currentSpread > 0)
        {
            currentSpread--;
            RefreshCurrentSpread();
        }
    }
}