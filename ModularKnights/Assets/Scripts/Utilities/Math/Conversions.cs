using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utilities.Math
{
    public class Conversions
    {
        public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
        {
            if ((UnityEngine.Object)cam == (UnityEngine.Object)null)
                return new Vector2(worldPoint.x, worldPoint.y);
            return (Vector2)cam.WorldToScreenPoint(worldPoint);
        }
    }
}
