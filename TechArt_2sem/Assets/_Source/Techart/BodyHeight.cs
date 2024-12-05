using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Techart
{
    public class BodyHeight : MonoBehaviour
    {
        [SerializeField] private List<FootMover> targetFootPoints;

        private void Update()
        {
            CalculateHeight();
        }

        void CalculateHeight()
        {
            float sum = 0f;

            for (int i = 0; i < targetFootPoints.Count; i++)
            {
                sum += targetFootPoints[i].NewTarget.y;
            }

            float newHeight = sum / targetFootPoints.Count;

            transform.position = new Vector3(transform.position.x, newHeight + 1.5f, transform.position.z);    
        }
    }
}