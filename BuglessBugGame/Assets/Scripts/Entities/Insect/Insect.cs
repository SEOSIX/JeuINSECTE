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
            if (GameManager.instance.player.isInteracted && isInside)
            {
                GameManager.instance.M_MiniGameManager.DetectForInteraction(this);
            }
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Player player = GameManager.instance.player;
            player.isInteracted = false;
            isInside=true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return; 
            isInside = false;
        }
    }
}