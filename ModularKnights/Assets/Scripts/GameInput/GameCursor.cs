using Assets.Scripts.GameInformation;
using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GameInput
{
    /// <summary>
    /// TODO:
    /// Change this to move cursor on right stick.
    /// </summary>
    public class GameCursor:MonoBehaviour,ICanvasRaycastFilter
    {

        /// <summary>
        /// The instance of the game object controlling this mono behavior.
        /// </summary>
        public static GameCursor Instance;

        private RectTransform rect;
        DeltaTimer snapTimer;
        public float snapDelay = .2f;
        public float snapSensitivity = .5f;
        public Vector3 oldMousePos;
        DeltaTimer visibilityTimer;

        private bool movedByCursor;

        private float mouseMovementSpeed = 1.0f;

        public bool isVisible;

        public Vector3 CanvasPosition
        {
            get
            {
                return this.gameObject.transform.position;
            }
        }

        public Vector3 WorldPosition
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(this.gameObject.transform.position);
            }
        }

        bool canSnapToNextSpot
        {
            get
            {
                if (snapTimer == null) return false;
                else
                {
                    if (snapTimer.IsFinished) return true;
                    else return false;
                }
            }
        }

        private bool _justMoved;
        public bool JustMoved
        {
            get
            {
                return _justMoved;
            }
        }

        private void Awake()
        {
            GameCursor.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            oldMousePos = Camera.main.ScreenToWorldPoint((Vector2)UnityEngine.Input.mousePosition);
            visibilityTimer = new Utilities.Timers.DeltaTimer(5, Enums.TimerType.CountDown, false,new Utilities.Delegates.VoidDelegate(makeInvisible));
            visibilityTimer.start();
            rect = this.gameObject.GetComponent<RectTransform>();
            snapTimer = new DeltaTimer((decimal)snapDelay, Enums.TimerType.CountDown, false, null);
            snapTimer.start();

        }

        void Update()
        {
            //timer.tick();
            snapTimer.tick();
            //setVisibility();


            if (Menu.IsMenuUp)
            {

                if (Menu.ActiveMenu.snapCompatible() == true)
                {
                    checkForSnappyMovement();
                    checkForNonSnapMovement();

                }
                else
                {
                    checkForNonSnapMovement();
                }
            }
            else
            {
                checkForNonSnapMovement();
            }
        }

        /// <summary>
        /// Checks for snappy movement for the player controlled by the left joystick.
        /// </summary>
        private void checkForSnappyMovement()
        {
            //if (GameInput.InputControls.GetControllerType() == InputControls.ControllerType.Keyboard) return;

            Vector3 delta = new Vector3(InputControls.LeftJoystickHorizontal, InputControls.LeftJoystickVertical, 0) * mouseMovementSpeed;
            if (canSnapToNextSpot)
            {
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x < -snapSensitivity)
                    {
                        Menu.ActiveMenu.selectedComponent.snapToNextComponent(Enums.FacingDirection.Left);
                        movedByCursor = false;
                        visibilityTimer.restart();
                        isVisible = true;
                        snapTimer.restart();
                        _justMoved = true;
                        return;
                    }
                    else if (delta.x > snapSensitivity)
                    {
                        Menu.ActiveMenu.selectedComponent.snapToNextComponent(Enums.FacingDirection.Right);
                        movedByCursor = false;
                        visibilityTimer.restart();
                        isVisible = true;
                        snapTimer.restart();
                        _justMoved = true;
                        return;
                    }
                }
                else if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.y < -snapSensitivity)
                    {
                        Menu.ActiveMenu.selectedComponent.snapToNextComponent(Enums.FacingDirection.Down);
                        movedByCursor = false;
                        visibilityTimer.restart();
                        isVisible = true;
                        snapTimer.restart();
                        _justMoved = true;
                        return;
                    }
                    else if (delta.y > snapSensitivity)
                    {
                        Menu.ActiveMenu.selectedComponent.snapToNextComponent(Enums.FacingDirection.Up);
                        movedByCursor = false;
                        visibilityTimer.restart();
                        isVisible = true;
                        snapTimer.restart();
                        _justMoved = true;
                        return;
                    }
                }
                else
                {
                    _justMoved = false;
                }
            }
        }

        /// <summary>
        /// Checks for non-snappy movement on the controller.
        /// </summary>
        private void checkForNonSnapMovement()
        {
            Vector2 vec = UnityEngine.Input.mousePosition;
            if (vec.Equals(oldMousePos))
            {
                Vector3 delta = new Vector3(GameInput.InputControls.RightJoystickHorizontal, GameInput.InputControls.RightJoystickVertical, 0) * mouseMovementSpeed;
                this.rect.position += delta;
                if (delta.x == 0 && delta.y == 0)
                {
                    _justMoved = false;
                    return;
                }
                if (Mathf.Abs(delta.x) > 0 || Mathf.Abs(delta.y) > 0) visibilityTimer.restart();
                movedByCursor = false;
                isVisible = true;
                _justMoved = true;
            }
            else
            {
                if (Mathf.Abs(vec.x - oldMousePos.x) < .001 && Mathf.Abs(vec.y - oldMousePos.y) < .001)
                {
                    _justMoved = false;
                    return; //stop random mouse sliding.
                }
                oldMousePos = vec;
                this.rect.position = vec;
                movedByCursor = true;
                visibilityTimer.restart();
                isVisible = true;
                _justMoved = true;
            }
        }

        /// <summary>
        /// Forces the game cursor to snap to the current given component.
        /// </summary>
        public void snapToCurrentMenuComponent()
        {
            if (Menu.ActiveMenu.menuCursor != null)
            {
                if (Menu.ActiveMenu.selectedComponent != null)
                {
                    this.gameObject.GetComponent<RectTransform>().position = Menu.ActiveMenu.selectedComponent.unityObject.transform.position;
                }
            }
        }

        public void snapToGivenObject(GameObject UnityObject)
        {
            this.gameObject.GetComponent<RectTransform>().position = UnityObject.transform.localPosition;
        }

        public void snapToGivenPosition(Vector3 Position)
        {
            this.gameObject.GetComponent<RectTransform>().position = Position;
        }

        public void snapToGivenRectTransform(RectTransform Transform)
        {
            this.gameObject.GetComponent<RectTransform>().position = Transform.localPosition;
        }

        /// <summary>
        /// https://forum.unity.com/threads/ignoring-layers-in-unitygui.272524/
        /// </summary>
        /// <param name="screenPoint"></param>
        /// <param name="eventCamera"></param>
        /// <returns></returns>
        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            //the raycast will continue, if you want to block then set to true
            return false;
        }

        /// <summary>
        /// Sets the cursors position.
        /// </summary>
        /// <param name="position"></param>
        public void setCursorPosition(Vector2 position)
        {
            this.rect.position = position;
            visibilityTimer.restart();
            movedByCursor = false;
        }

        /// <summary>
        /// Sets the cursor's position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setCursorPosition(float x, float y)
        {
            this.rect.position = new Vector3(x,y,0);
            visibilityTimer.restart();
            movedByCursor = false;
        }

        /// <summary>
        /// Sets the visibility of the mouse.
        /// </summary>
        /// <param name="visible"></param>
        public void setVisibility(bool visible)
        {
            visibilityTimer.stop();
            isVisible = visible;
        }

        /// <summary>
        /// Sets the visibility automatically for the game cursor.
        /// </summary>
        private void setVisibility()
        {
            this.GetComponent<Image>().enabled = isVisible;
        }

        /// <summary>
        /// Makes the game cursor object invisible.
        /// </summary>
        private void makeInvisible()
        {
            isVisible = false;
        }
        #region
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
        //              Static Methods                //
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        /// <summary>
        /// Checks to see if the game's cursor intersects with the ui element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool CursorIntersectsRect(MonoBehaviour behavior) 
        {
            if (GetWorldSapceRect(behavior.GetComponent<RectTransform>()).Overlaps(GetWorldSapceRect(Menu.ActiveMenu.menuCursor.rect)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks of the game cursor intersects a given transform.
        /// </summary>
        /// <param name="behavior">The transform to check against.</param>
        /// <returns></returns>
        public static bool CursorIntersectsRect(GameObject behavior)
        {
            if (GetWorldSapceRect(behavior.GetComponent<RectTransform>()).Overlaps(GetWorldSapceRect(Menu.ActiveMenu.menuCursor.rect)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the worldspace position of the rect transform.
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        static Rect GetWorldSapceRect(RectTransform rt)
        {
            var r = rt.rect;
            r.center = rt.TransformPoint(r.center);
            r.size = rt.TransformVector(r.size);
            return r;
        }

        /// <summary>
        /// Checks to see if the computers mouse cursor intersects with the ui element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool MouseIntersectsRect(MonoBehaviour behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the hardware mouse intersects a rect transform.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool MouseIntersectsRect(GameObject behavior)
        {
            if (UnityEngine.RectTransformUtility.RectangleContainsScreenPoint(behavior.GetComponent<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the game's cursor can interact with the UI Element.
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public static bool CanCursorInteract(MonoBehaviour behavior)
        {
            if (GameCursor.CursorIntersectsRect(behavior))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Simulates a mouse press for the cursor if the A button is pressed and the cursor intersects the mono behavior.
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static bool SimulateMousePress(MonoBehaviour behavior,bool useHardwareMouse=false)
        {
            if (behavior == null)
            {
                Debug.Log("BEHAVIOR IS NULL");
            }
            if (GameCursor.CursorIntersectsRect(behavior) && GameInput.InputControls.APressed)
            {
                return true;
            }
            
            
            else if (GameCursor.MouseIntersectsRect(behavior) && Input.GetMouseButtonDown(0))
            {
                return true;
            }
            else
            {             
                return false;
            }
            
        }
        /// <summary>
        /// Simulates a mouse press on the controller using the game cursor.
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static bool SimulateMousePress(MenuComponent behavior)
        {

            return SimulateMousePress(behavior.unityObject);
        }

        /// <summary>
        /// Simulates a mouse press on the controller using the game cursor.
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static bool SimulateMousePress(GameObject behavior)
        {
            if (behavior == null)
            {
                Debug.Log("BEHAVIOR IS NULL");
            }
            if (GameCursor.CursorIntersectsRect(behavior) && GameInput.InputControls.APressed)
            {
                return true;
            }


            else if (GameCursor.MouseIntersectsRect(behavior) && Input.GetMouseButtonDown(0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Simulates a mouse hover action on a controller using the game cursor.
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static bool SimulateMouseHover(MonoBehaviour behavior)
        {
            if (GameCursor.CursorIntersectsRect(behavior))
            {
                return true;
            }
            else if (GameCursor.MouseIntersectsRect(behavior))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Simulates a mouse hover action on a controller using the game cursor.
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static bool SimulateMouseHover(MenuComponent behavior)
        {
            return SimulateMouseHover(behavior.unityObject);
        }

        /// <summary>
        /// Simulates a mouse hover action on a controller using the game cursor.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="useHardwareMouse"></param>
        /// <returns></returns>
        public static bool SimulateMouseHover(GameObject obj)
        {
            if (GameCursor.CursorIntersectsRect(obj))
            {
                return true;
            }
            else if (GameCursor.MouseIntersectsRect(obj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sets the game's cursor to a specific position.
        /// </summary>
        /// <param name="position"></param>
        public static void SetCursorPosition(Vector2 position)
        {
            GameCursor.Instance.setCursorPosition(position);
        }

        /// <summary>
        /// Sets the game's cursor to a specific position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetCursorPosition(float x, float y)
        {
            GameCursor.Instance.setCursorPosition(x,y);
        }

        #endregion
    }
}
