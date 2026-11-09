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
        private static readonly List<InsectData> _candidates = new List<InsectData>();

        void Update()
        {
            CheckForInteract();
        }

        public InsectData PickRandomInsect()
        {
            var spots = spotInsect.insectSpots;
            _candidates.Clear();

            for (int i = 0; i < spots.Count; i++)
            {
                if (Random.value < spots[i].insectChanceSpawn)
                    _candidates.Add(spots[i].insectData);
            }

            if (_candidates.Count == 0) return null;

            _insectPicked = _candidates[Random.Range(0, _candidates.Count)];
            return _insectPicked;
        }

        private void CheckForInteract()
        {
            if (GameManager.instance.player.isInteracted && isInside)
            {
                GameManager.instance.M_MiniGameManager.DetectForInteraction(this);
                
                print("telaviv");
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