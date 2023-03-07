using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Path
{
    public class PointController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private List<PointController> nextPoints;

        #endregion

        #region Helper

        // Get next point by random
        public PointController GetNextPoint() =>
            nextPoints.Count.Equals(0) ? null : nextPoints[Random.Range(0, nextPoints.Count)];

        #endregion
    }
}