using Assets.Scripts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameInput
{
    /// <summary>
    /// Checks input for Unity.
    /// </summary>
    public class InputControls : MonoBehaviour
    {

	    private static bool _DPadReleased;

        private static bool _DLeftPressed;
        private static bool _DRightPressed;
        private static bool _DUpPressed;
        private static bool _DDownPressed;
	
        /// <summary>
        /// The types of input controllers supported.
        /// </summary>
        public enum ControllerType
        {
            Keyboard,
            XBox360,
            DualShock
        }

        /// <summary>
        /// Property to check if the "A" button is pressed.
        /// </summary>
        public static bool APressed
        {
            get
            {
                ControllerType controller = GetControllerType();
                if (controller == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire1");
                }
                else if (controller == ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac)
                    {
                        return Input.GetButtonDown("Fire1_Mac");
                    }
                    return Input.GetButtonDown("Fire1");
                }
                else if(controller== ControllerType.Keyboard)
                {
                    return Input.GetButtonDown("Fire1");
                }
                else
                {
                    return Input.GetButtonDown("Fire1");
                }
            }
        }

        /// <summary>
        /// Property to check if the "B" button is pressed.
        /// </summary>
        public static bool BPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire2");
                }
                else if (GetControllerType() == ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac)
                    {
                        return Input.GetButtonDown("Fire2_Mac");
                    }
                    return Input.GetButtonDown("Fire2");
                }
                else
                {
                    return Input.GetButtonDown("Fire2");
                }
            }
        }

        /// <summary>
        /// Property to check if the "X" button is pressed.
        /// </summary>
        public static bool XPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire3");
                }
                else if (GetControllerType() == ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac) return Input.GetButtonDown("Fire3_Mac");
                    return Input.GetButtonDown("Fire3");
                }
                else
                {
                    return Input.GetButtonDown("Fire3");
                }
            }
        }

        /// <summary>
        /// Property to check if the "Y" button is pressed.
        /// </summary>
        public static bool YPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.DualShock)
                {
                    return Input.GetButtonDown("Fire4");
                }
                else if (GetControllerType() == ControllerType.XBox360)
                {
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("Fire4_Mac");
                    return Input.GetButtonDown("Fire4");
                }
                else
                {
                    return Input.GetButtonDown("Fire4");
                }
            }
        }

        /// <summary>
        /// Property to check if the start button is pressed.
        /// </summary>
        public static bool StartPressed
        {
            get
            {
                if(GetControllerType()== ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Mac) return Input.GetButtonDown("Start_Mac");
                    return Input.GetButtonDown("Start");
                }
                else
                {
                    return Input.GetButtonDown("Start");
                }
               
            }
        }

        /// <summary>
        /// Property to check if the select button is pressed.
        /// </summary>
        public static bool SelectPressed
        {
            get
            {
                if (GetControllerType() == ControllerType.XBox360)
                {
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("Select_Mac");
                    return Input.GetButtonDown("Select");
                }
                else
                {
                    return Input.GetButtonDown("Select");
                }
            }
        }

        /// <summary>
        /// Get the input for the left trigger.
        /// </summary>
        public static float LeftTrigger
        {
            get
            {
                if(GetControllerType()== ControllerType.XBox360)
                {
                    if(OSChecker.OS== Enums.OperatingSystem.Windows) return Input.GetAxis("LeftTrigger_Windows");
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("LeftTrigger_Mac");
                    if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("LeftTrigger_Linux");
                }
                if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetAxis("LeftTrigger_Windows");
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("LeftTrigger_Mac");
                if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("LeftTrigger_Linux");
                return Input.GetAxis("LeftTrigger_Windows");
            }
        }

        /// <summary>
        /// Get the input for the right trigger.
        /// </summary>
        public static float RightTrigger
        {
            get
            {
                if (GetControllerType() == ControllerType.XBox360)
                {
                    if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetAxis("RightTrigger_Windows");
                    if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("RightTrigger_Mac");
                    if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("LeftTrigger_Linux");
                }
                if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetAxis("RightTrigger_Windows");
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetAxis("RightTrigger_Mac");
                if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetAxis("RightTrigger_Linux");
                return Input.GetAxis("RightTrigger_Windows");
            }
        }

        /// <summary>
        /// Get the input for the left bummper.
        /// </summary>
        public static bool LeftBumperPressed
        {
            get
            {
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("LeftBumper_Mac");
                return Input.GetButtonDown("LeftBumper");
            }
        }

        /// <summary>
        /// Get the input for the rightBummper
        /// </summary>
        public static bool RightBumperPressed
        {
            get
            {
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("RightBumper_Mac");
                return Input.GetButtonDown("RightBumper");
            }
        }
		
		/// <summary>
        /// Checks to see if the LeftDPad is being held down.
        /// </summary>
        public static bool LeftDPadDown
        {
            get
            {
                return Input.GetAxis("DPad_Horizontal")<0;
            }
        }

        /// <summary>
        /// Checks to see if the Right DPad is being held down.
        /// </summary>
        public static bool RightDPadDown
        {
            get
            {
                return Input.GetAxis("DPad_Horizontal") > 0;
            }
        }

        /// <summary>
        /// Checks to see if the Up DPad is being held down.
        /// </summary>
        public static bool UpDPadDown
        {
            get
            {
                return Input.GetAxis("DPad_Vertical") > 0;
            }
        }

        /// <summary>
        /// Checks to see if the Down DPad is being held down.
        /// </summary>
        public static bool DownDPadDown
        {
            get
            {
                return Input.GetAxis("DPad_Vertical") < 0;
            }
        }


        /// <summary>
        /// Checks to see if the LeftDPad was pressed.
        /// </summary>
        public static bool LeftDPadPressed
        {
            get
            {
                float input = Input.GetAxis("DPad_Horizontal");
                if (_DLeftPressed == false)
                {
                    if (input < 0)
                    {
                        _DLeftPressed = true;
                        return true;
                    }
                    else
                    {
                        _DLeftPressed = false;
                    }
                }
                else
                {
                    if (input >= 0)
                    {
                        _DLeftPressed = false;
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the Right DPad is being held down.
        /// </summary>
        public static bool RightDPadPressed
        {
            get
            {

                float input = Input.GetAxis("DPad_Horizontal");
                if (_DRightPressed == false)
                {
                    if (input > 0)
                    {
                        _DRightPressed = true;
                        return true;
                    }
                    else
                    {
                        _DRightPressed = false;
                    }
                }
                else
                {
                    if (input <= 0)
                    {
                        _DRightPressed = false;
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the Up DPad is being held down.
        /// </summary>
        public static bool UpDPadPressed
        {
            get
            {

                float input = Input.GetAxis("DPad_Vertical");
                if (_DUpPressed == false)
                {
                    if (input > 0)
                    {
                        _DUpPressed = true;
                        return true;
                    }
                    else
                    {
                        _DUpPressed = false;
                    }
                }
                else
                {
                    if (input <= 0)
                    {
                        _DUpPressed = false;
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the Down DPad is being held down.
        /// </summary>
        public static bool DownDPadPressed
        {
            get
            {
                float input = Input.GetAxis("DPad_Vertical");
                if (_DDownPressed == false)
                {
                    if (input < 0)
                    {
                        _DDownPressed = true;
                        return true;
                    }
                    else
                    {
                        _DDownPressed = false;
                    }
                }
                else
                {
                    if (input >= 0)
                    {
                        _DDownPressed = false;
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the dpad has been released.
        /// </summary>
        public static bool DPadReleased
        {
            get
            {
                return Input.GetAxis("DPad_Vertical") == 0 && Input.GetAxis("DPad_Horizontal") == 0;
            }
        }

        /// <summary>
        /// Get the right horizontal value on the joystick.
        /// </summary>
        public static float RightJoystickHorizontal
        {
            get
            {
                if(OSChecker.OS== Enums.OperatingSystem.Mac)
                {
                    return Input.GetAxis("RightJoystickHorizontal_Mac");
                }
                return Input.GetAxis("RightJoystickHorizontal");
            }
        }

        /// <summary>
        /// Get the right vertical value on the joystick.
        /// </summary>
        public static float RightJoystickVertical
        {
            get
            {
                if (OSChecker.OS == Enums.OperatingSystem.Mac)
                {
                    return Input.GetAxis("RightJoystickVertical_Mac");
                }
                return Input.GetAxis("RightJoystickVertical");
            }
        }

        public static float LeftJoystickHorizontal
        {
            get
            {
                return Input.GetAxis("Horizontal");
            }
        }

        public static float LeftJoystickVertical
        {
            get
            {
                return Input.GetAxis("Vertical");
            }
        }

        public static Vector2 LeftJoystickDelta
        {
            get
            {
                return new Vector2(LeftJoystickHorizontal, LeftJoystickVertical);
            }
        }

        public static bool LeftJoystickMoved
        {
            get
            {
                Vector2 delta = LeftJoystickDelta;
                if (delta.x == 0 && delta.y == 0) return false;
                else return true;
            }
        }

        public static Vector2 MouseDelta
        {
            get
            {
                Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                return delta;
            }
        }

        public static bool MouseMoved
        {
            get
            {
                Vector2 delta = MouseDelta;
                if (delta.x == 0 && delta.y == 0) return false;
                else return true;
            }
        }

        /// <summary>
        /// Checks if L3 on the joystick has been clicked down.
        /// </summary>
        public static bool L3Down
        {
            get{
                if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetButtonDown("L3_Windows");
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("L3_Mac");
                if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetButtonDown("L3_Linux");
                return Input.GetButtonDown("L3_Windows");
            }
        }

        /// <summary>
        /// Checks if R3 on the joystick has been clicked down.
        /// </summary>
        public static bool R3Down
        {
            get
            {
                if (OSChecker.OS == Enums.OperatingSystem.Windows) return Input.GetButtonDown("R3_Windows");
                if (OSChecker.OS == Enums.OperatingSystem.Mac) return Input.GetButtonDown("R3_Mac");
                if (OSChecker.OS == Enums.OperatingSystem.Linux) return Input.GetButtonDown("R3_Linux");
                return Input.GetButtonDown("R3_Windows");
            }
        }


        /// <summary>
        /// Used to determine if the user is using a PS3 or XBox controller so that buttons can be mapped properly to inputs.
        /// </summary>
        /// <returns></returns>
        public static ControllerType GetControllerType()
        {
            Debug.Log(Input.GetJoystickNames()[0].ToLower());

            
            if (Input.GetJoystickNames().Length == 0) return ControllerType.Keyboard;
            try
            {
                if (Input.GetJoystickNames().ElementAt(0).Contains("DualShock"))
                {
                    return ControllerType.DualShock;
                }
                else if (Input.GetJoystickNames()[0].Contains("XBOX 360"))
                {
                    Debug.Log("I AM XBOX");
                    return ControllerType.XBox360;
                }
                else if (Input.GetJoystickNames().ElementAt(0).Contains("XBOX One"))
                {
                    throw new Exception("Xbox One controllers not supported yet. Please contact Josh!");
                }
                else
                {
                    Debug.Log("KEYBOARD???");
                    return ControllerType.Keyboard;
                }
            }
            catch (Exception err)
            {
                return ControllerType.Keyboard;
            }
        }



    }
}
