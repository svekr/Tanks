﻿using UnityEngine;

namespace Utils.Extensions {
    static public class BoundsExtension {
        static public Vector3 GetRandomPoint(this Bounds bounds) {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}