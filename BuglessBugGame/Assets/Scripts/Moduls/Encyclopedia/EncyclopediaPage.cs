using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaPage : MonoBehaviour
{
    public const int MaxInsectsPerPage = 1;

    [Header("Grille")]
    [SerializeField] private GameObject cellsContainer;

    [Header("Panneau détail")]
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private TextMeshProUGUI detailName;
    [SerializeField] private TextMeshProUGUI detailDescription;
    [SerializeField] private Button closeButton;

    public Transform Container => cellsContainer.transform;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => { CloseDetailPanel();});
    }

    public void ShowDetail(InsectData insect)
    {
        if (detailName != null) detailName.text = null;
        if (detailDescription != null) detailDescription.text = null;
        
        if (cellsContainer != null) cellsContainer.SetActive(false);
        if (detailPanel != null) detailPanel.SetActive(true);

        if (detailName != null) detailName.text = insect.insectName;
        if (detailDescription != null) detailDescription.text = insect.insectDescription[0];
    }

    public void ShowGrid()
    {
        if (cellsContainer != null) cellsContainer.SetActive(true);
        if (detailPanel != null) detailPanel.SetActive(false);
    }

    private void CloseDetailPanel()
    {
        if (detailPanel != null) detailPanel.SetActive(false);
        if (cellsContainer != null) cellsContainer.SetActive(true);
    }
}