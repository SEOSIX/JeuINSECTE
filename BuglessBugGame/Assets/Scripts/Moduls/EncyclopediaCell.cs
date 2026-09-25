using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Moduls
{
    public class EncyclopediaCell : MonoBehaviour
    {
        [Header("RefsUI")]
        [SerializeField] private TextMeshProUGUI insectName;
        [SerializeField] private TextMeshProUGUI insectDescription;
        [SerializeField] private Image icon;
        [SerializeField] private Button button;

        private InsectData currentInsect;

        public event Action<InsectData> OnCellClicked;

        private void Awake()
        {
            if (button == null)
                button = GetComponent<Button>();

            if (button != null)
                button.onClick.AddListener(HandleClick);
        }

        public void SetupInsectEncyclopedia(InsectData bug)
        {
            currentInsect = bug;

            if (icon != null)
                icon.sprite = bug.insectIcon;

            if (insectName != null)
                insectName.text = bug.insectName;

            if (insectDescription != null)
                insectDescription.text = bug.insectDescription;
        }

        private void HandleClick()
        {
            if (currentInsect != null)
                OnCellClicked?.Invoke(currentInsect);
        }
    }
}