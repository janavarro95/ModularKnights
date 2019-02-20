using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.Components
{
    public class ToggleComponent:MenuComponent
    {

        public GameObject gameObject
        {
            get
            {
                return this.unityObject.gameObject;
            }
        }

        public bool isOn
        {
            get
            {
                return (this.unityObject as Toggle).isOn;
            }
            set
            {
                (this.unityObject as Toggle).isOn = value;
            }
        }


        public ToggleComponent(MonoBehaviour UnityObject) : base(UnityObject)
        {

        }

        public override void select()
        {
            (this.unityObject as Toggle).Select();
        }

    }
}
