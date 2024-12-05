using System;
using UnityEngine;

namespace Techart
{
    public class FootMover : MonoBehaviour
    {
        public Vector3 NewTarget { get; set; }
        
        [SerializeField] private Transform targetPoint;
        [SerializeField] private float distance;
        [SerializeField] private float maxHeightDistance;
    
        [SerializeField] private float countLerpPos = 0.4f;
        [SerializeField] private float countLerpHeight = 0.5f;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float amplitude = 0.4f;
        
        private float _currentTime = 1f;

        private void Start()
        {
            NewTarget = targetPoint.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -transform.up, out hit)) // Исправлено направление луча
            {
                if (Vector3.Distance(hit.point, NewTarget) > distance)
                {
                    _currentTime = 0;
                    NewTarget = hit.point;
                }

                if (_currentTime < 1)
                {
                    Vector3 footPosition = Vector3.Lerp(targetPoint.position, NewTarget, countLerpPos);

                    footPosition.y = Mathf.Lerp(footPosition.y, NewTarget.y, countLerpHeight) +
                                     (Mathf.Sin(_currentTime * Mathf.PI) * amplitude);

                    targetPoint.position = footPosition;
                    _currentTime += Time.deltaTime * speed;
                }
            }
        }
    }
}