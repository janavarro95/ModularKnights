using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class Screen
    {

        public static Vector2 ViewportSize()
        {
            return new Vector2(Camera.main.pixelRect.width, Camera.main.pixelRect.height);
        }

    }
}
