using System;
using UnityEngine;
using UnityEngine.UI;

namespace Moduls
{
    public class EncyclopediaCell : MonoBehaviour
    {
        [Header("RefsUI")]
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

        void Update()
        {
            if (!currentInsect.isUnlocked)
            {
                icon.material = new Material(icon.material);
                icon.material.SetFloat("_GrayscaleAmount", 1);
            }
            else
            {
                icon.material = new Material(icon.material);
                icon.material.SetFloat("_GrayscaleAmount", 0);
            }
        }
        public void SetupInsectEncyclopedia(InsectData bug)
        {
            currentInsect = bug;

            if (icon != null)
                icon.sprite = bug.insectIcon;
        }

        private void HandleClick()
        {
            if (currentInsect != null)
                OnCellClicked?.Invoke(currentInsect);
        }
    }
}