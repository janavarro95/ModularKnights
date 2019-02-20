using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.Components
{
    public class InputFieldComponent:MenuComponent
    {

        public string value
        {
            get
            {
                return (this.unityObject as InputField).text;
            }
            set
            {
                (this.unityObject as InputField).text = value;
            }
        }

        public InputFieldComponent(MonoBehaviour UnityObject) : base(UnityObject)
        {

        }

        public override void select()
        {
            (this.unityObject as InputField).Select();
        }

        


    }
}
