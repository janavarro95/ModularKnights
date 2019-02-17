using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utilities.Math
{
    public static class IntExtensions
    {
        /// <summary>
        /// Clamps the int between two values.
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Clamp(this int Value, int min, int max)
        {
            int value = Value;
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }

    }
}
