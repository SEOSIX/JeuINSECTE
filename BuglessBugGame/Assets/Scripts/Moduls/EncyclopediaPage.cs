using TMPro;
using UnityEngine;

public class EncyclopediaPage : MonoBehaviour
{
    public const int MaxInsectsPerPage = 9;

    [Header("Grille")]
    [SerializeField] private GameObject cellsContainer;

    [Header("Panneau détail")]
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private TextMeshProUGUI detailName;
    [SerializeField] private TextMeshProUGUI detailDescription;

    public Transform Container => cellsContainer.transform;

    public void ShowDetail(InsectData insect)
    {
        if (cellsContainer != null) cellsContainer.SetActive(false);
        if (detailPanel != null) detailPanel.SetActive(true);

        if (detailName != null) detailName.text = insect.insectName;
        if (detailDescription != null) detailDescription.text = insect.insectDescription;
    }

    public void ShowGrid()
    {
        if (cellsContainer != null) cellsContainer.SetActive(true);
        if (detailPanel != null) detailPanel.SetActive(false);
    }
}