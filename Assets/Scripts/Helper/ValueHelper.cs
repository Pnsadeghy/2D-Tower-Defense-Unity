using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class ValueHelper
    {
        public static Quaternion LookAt2D(Vector3 source, Vector3 target) =>
            Quaternion.LookRotation(Vector3.forward, target - source);
    }
}
