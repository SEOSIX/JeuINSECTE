using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GamePlayCore
{
    public class Insect : MonoBehaviour
    {
        [Header("Item")]
        public InsectData bug;
        public int count = 1;
        
        [Header("Visual")]
        public TextMeshProUGUI pickupTitle;

        private bool isInside;
        private bool isAimedAt;


        void Update()
        {
            CheckForInteract();
        }

        private void CheckForInteract()
        {
            if (Player.Instance.isInteracted &&  isInside)
            {
                List<InsectSlot> insect = Player.Instance.playerData.playerInventoryData.insects;
                InsectSlot existingSlot = insect.Find(slot => slot.insect == bug);
                if (existingSlot != null && existingSlot.count == bug.maxStackable) return;
            
                Player.Instance.insectPickUp.AddItem(bug, count);
                Destroy(gameObject);
                Player player = GameManager.instance.player;
                player.isInteracted = false;
                Debug.Log($"{bug.name} has been picked up");
            }
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            isInside=true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return; 
            isInside = false;
        }
    }
}