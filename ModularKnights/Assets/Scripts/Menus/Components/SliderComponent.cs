using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.Components
{
    public class SliderComponent : MenuComponent
    {

        public float value
        {
            get
            {
                return (this.unityObject as Slider).value;
            }
            set
            {
                (this.unityObject as Slider).value = value;
            }
        }

        public float maxValue
        {
            get
            {
                return (this.unityObject as Slider).maxValue;
            }
            set
            {
                (this.unityObject as Slider).maxValue = value;
            }
        }

        public float minValue
        {
            get
            {
                return (this.unityObject as Slider).minValue;
            }
            set
            {
                (this.unityObject as Slider).minValue = value;
            }
        }

        public GameObject gameObject
        {
            get
            {
                return this.unityObject.gameObject;
            }
        }


        public SliderComponent(MonoBehaviour UnityObject) : base(UnityObject)
        {

        }

        public override void select()
        {
            (this.unityObject as Slider).Select();
        }


        
    }
}
