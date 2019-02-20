using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.Components
{
    public class DropDownComponent:MenuComponent
    {

        public int currentPosition
        {
            get
            {
                return (this.unityObject as Dropdown).value;
            }
            set
            {
                (this.unityObject as Dropdown).value = value;
            }
        }

        public string selectedOptionName
        {
            get
            {
                return (this.unityObject as Dropdown).options[currentPosition].text;
            }
        }

        public List<Dropdown.OptionData> options
        {
            get
            {
                return (this.unityObject as Dropdown).options;
            }
        }


        public DropDownComponent(MonoBehaviour UnityObject) : base(UnityObject)
        {
            
        }

        public void clearOptions()
        {
            (this.unityObject as Dropdown).ClearOptions();
        }

        public void addOptions(List<Dropdown.OptionData> Options)
        {
            (this.unityObject as Dropdown).AddOptions(Options);
        }
        public void addOptions(List<string> Options)
        {
            (this.unityObject as Dropdown).AddOptions(Options);
        }
        public void addOptions(List<Sprite> Options)
        {
            (this.unityObject as Dropdown).AddOptions(Options);
        }

        /// <summary>
        /// Override by disallowing 
        /// </summary>
        /// <param name="NextDirection"></param>
        /// <returns></returns>
        public override MenuComponent snapToNextComponent(Enums.FacingDirection NextDirection)
        {
            if (this.Selected)
            {
                if (NextDirection == Enums.FacingDirection.Down)
                {
                    if (this.currentPosition < this.options.Count-1)
                    {
                        this.currentPosition = this.currentPosition + 1;
                    }
                    return this;
                }
                if (NextDirection == Enums.FacingDirection.Up)
                {
                    if (this.currentPosition > 0)
                    {
                        this.currentPosition = this.currentPosition - 1;
                    }
                    return this;
                }
            }
            return base.snapToNextComponent(NextDirection);        
        }

        public override void select()
        {
            (this.unityObject as Dropdown).Select();
            (this.unityObject as Dropdown).Show();
        }

    }
}
