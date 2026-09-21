using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BugSlotUI : MonoBehaviour
{
    [Header("References UI")]
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;

    public void Setup(InsectData bug, int count)
    {
        if (icon != null)
            icon.sprite = bug.insectIcon;

        if (nameText != null)
            nameText.text = bug.insectName;

        if (countText != null)
            countText.text = count.ToString();
    }
}
