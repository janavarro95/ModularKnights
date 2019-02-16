using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class OSChecker
    {
        private static Enums.OperatingSystem os=GetOS();
        public static Enums.OperatingSystem OS
        {
            get
            {
                return os;
            }
        }


        public static Enums.OperatingSystem GetOS()
        {
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Linux) return Enums.OperatingSystem.Linux;
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) return Enums.OperatingSystem.Mac;
            if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows) return Enums.OperatingSystem.Windows;
            throw new Exception("Unexpected operating system detected! Are you not playing on pc???");
        }


    }
}
