using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlayCore
{
    public class Insect : MonoBehaviour
    {
        [Header("Item")]
        public InsectSpot spotInsect;
        
        [Header("Visual")]
        public GameObject vfx;

        [Header("Detection")]
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private LayerMask playerLayer;

        private bool isInside;
        private bool isAimedAt;
        private bool wasPlayerInSphere;
        
        [HideInInspector] public InsectData _insectPicked;
        private static readonly List<InsectData> _candidates = new List<InsectData>();
        private static readonly Collider[] _overlapBuffer = new Collider[8];

        void Update()
        {
            CheckForInteract();
            DetectionInsect();
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
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Player player = GameManager.instance.player;
            player.isInteracted = false;
            isInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return; 
            isInside = false;
        }

        private void DetectionInsect()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, _overlapBuffer, playerLayer);
            bool playerInSphere = false;

            for (int i = 0; i < hitCount; i++)
            {
                if (_overlapBuffer[i].CompareTag("Player"))
                {
                    playerInSphere = true;
                    break;
                }
            }

            if (playerInSphere && !wasPlayerInSphere)
            {
#if UNITY_ANDROID || UNITY_IOS
                Handheld.Vibrate();
#endif
            }

            wasPlayerInSphere = playerInSphere;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}