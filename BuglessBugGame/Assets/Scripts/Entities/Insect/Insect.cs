using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlayCore
{
    public class Insect : MonoBehaviour
    {
        [Header("Item")]
        public InsectSpot spotInsect;
        
        [Header("Visual")]
        public TextMeshProUGUI pickupTitle;

        private bool isInside;
        private bool isAimedAt;
        
        [HideInInspector] public InsectData _insectPicked;


        void Update()
        {
            CheckForInteract();
        }

        public InsectData PickRandomInsect()
        {
            var spots = spotInsect.insectSpots;
            var candidates = new List<InsectData>();

            for (int i = 0; i < spots.Count; i++)
            {
                if (Random.value < spots[i].insectChanceSpawn)
                {
                    candidates.Add(spots[i].insectData);
                }
            }

            if (candidates.Count == 0) return null;
            
            _insectPicked = candidates[Random.Range(0, candidates.Count)];
            Debug.Log(_insectPicked.insectName);
            return _insectPicked;
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