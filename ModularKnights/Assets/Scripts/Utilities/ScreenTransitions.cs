using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities.Delegates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utilities
{
    public class ScreenTransitions:MonoBehaviour
    {

        public static ScreenTransitions CurrentTransition;

        /// <summary>
        /// The image to overlay the screen. This is typically just a blank image.
        /// </summary>
        Image transitionOverlay;

        /// <summary>
        /// The timer which handles timing for the transition.
        /// </summary>
        Utilities.Timers.DeltaTimer timer;

        /// <summary>
        /// The scene to load after the transition.
        /// </summary>
        private string sceneToLoad;

        /// <summary>
        /// The original color to lerp from.
        /// </summary>
        private Color originalColor;
        /// <summary>
        /// The color to lerp to.
        /// </summary>
        private Color targetColor;
        /// <summary>
        /// The currently lerped color.
        /// </summary>
        private Color currentColor;

        public static bool shouldFadeInAfterWarp;
        public static float targetFadeInTime;

        /// <summary>
        /// The last color that was faded out to.
        /// </summary>
        public static Color lastFadeInColor;

        /// <summary>
        /// Deal with transition state.
        /// </summary>
        public enum TransitionState
        {
            Off,
            FadeIn,
            FadeOut
        }

        /// <summary>
        /// The current transition state.
        /// </summary>
        public TransitionState currentState;

        public bool IsTransitioning
        {
            get
            {
                if (currentState == TransitionState.FadeIn || currentState == TransitionState.FadeOut) return true;
                else return false;
            }
        }

        /// <summary>
        /// A fraction of the current time divided by the maximum time.
        /// </summary>
        private float transparencyLerp
        {
            get
            {
                return (float)(timer.currentTime / timer.maxTime);
            }
        }

        public void Awake()
        {
            currentState = TransitionState.Off;
        }

        public void Start()
        {
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            GameObject imgObj = canvas.transform.Find("Image").gameObject;
            transitionOverlay = imgObj.GetComponent<Image>();

            transitionOverlay.rectTransform.localScale = new Vector3(100, 100, 1);
            
            ScreenTransitions.CurrentTransition = this;
        }

        /// <summary>
        /// Initializes all the parameters necessary to make a scene transition.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="sceneToLoad"></param>
        /// <param name="fadeInColor"></param>
        /// <param name="State"></param>
        public void startNewSceneTransition(float seconds,string sceneToLoad,Color fadeInColor, TransitionState State)
        {
            timer = new Timers.DeltaTimer((decimal)seconds, Enums.TimerType.CountDown,false,new Delegates.VoidDelegate(transitionToNextScene));
            timer.start();
            this.sceneToLoad = sceneToLoad;
            currentState = State;

            if (currentState == TransitionState.FadeIn)
            {
                this.originalColor = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0);
                this.targetColor = fadeInColor;
            }
            else if(currentState == TransitionState.FadeOut)
            {
                this.originalColor = fadeInColor;
                this.targetColor = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0);
                lastFadeInColor = fadeInColor;
            }

            setTransitionColor(fadeInColor);
        }

        /// <summary>
        /// Starts a new scene transition while allowing for code to be ran when that transition finishes.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="sceneToLoad"></param>
        /// <param name="fadeInColor"></param>
        /// <param name="State"></param>
        /// <param name="OnTransitionFinish"></param>
        public void startNewSceneTransition(float seconds, string sceneToLoad, Color fadeInColor, TransitionState State, VoidDelegate OnTransitionFinish)
        {
            timer = new Timers.DeltaTimer((decimal)seconds, Enums.TimerType.CountDown, false, OnTransitionFinish);
            timer.start();
            this.sceneToLoad = sceneToLoad;
            currentState = State;

            if (currentState == TransitionState.FadeIn)
            {
                this.originalColor = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0);
                this.targetColor = fadeInColor;
                this.currentColor = this.originalColor;
            }
            else if (currentState == TransitionState.FadeOut)
            {
                this.originalColor = fadeInColor;
                this.targetColor = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0);
                lastFadeInColor = fadeInColor;
                this.currentColor = this.originalColor;
            }

            setTransitionColor(fadeInColor);
            
        }

        /// <summary>
        /// Updates the sceen transition.
        /// </summary>
        public void Update()
        {
            if (timer != null)
            {
                timer.Update();
                updateLerp();
            }
            updateColor();
        }

        /// <summary>
        /// Sets the color of the transition to be the proper lerp color.
        /// </summary>
        private void updateColor()
        {
            transitionOverlay.color = currentColor;
        }
        /// <summary>
        /// Loads the next scene as specified by the sceneToLoad field.
        /// </summary>
        private void transitionToNextScene()
        {
            lastFadeInColor = targetColor;
            if (String.IsNullOrEmpty(this.sceneToLoad))
            {
                if (this.currentState == TransitionState.FadeIn) Destroy(this.gameObject);
                return;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(this.sceneToLoad);
            }
            if (this.currentState == TransitionState.FadeIn) Destroy(this.gameObject);
        }

        /// <summary>
        /// Updates the color lerp.
        /// </summary>
        private void updateLerp()
        {
            currentColor = Color.Lerp(originalColor, targetColor, transparencyLerp);
        }
        public void makeTransparent()
        {
            currentColor = new Color(transitionOverlay.color.r, transitionOverlay.color.g, transitionOverlay.color.b, 0);
        }
        /// <summary>
        /// Makes the transition completely opaque.
        /// </summary>
        public void makeOpaque()
        {
           currentColor = new Color(transitionOverlay.color.r, transitionOverlay.color.g, transitionOverlay.color.b, 1);
        }
        /// <summary>
        /// Sets the transition color to be a specific color.
        /// </summary>
        /// <param name="c"></param>
        public void setTransitionColor(Color c)
        {
            currentColor = c;
        }
        /// <summary>
        /// Sets the transition color to be black.
        /// </summary>
        public void setBlackTransition()
        {
            setTransitionColor(Color.black);
        }
        /// <summary>
        /// Sets the transition color to be white.
        /// </summary>
        public void setWhiteTransition()
        {
            setTransitionColor(Color.white);
        }

        public void OnDestroy()
        {
            ScreenTransitions.CurrentTransition = null;
        }


        /// <summary>
        /// Starts a screen transition for the scene.
        /// </summary>
        /// <param name="Seconds">The number of seconds (as a float) it takes for the transition to occur.</param>
        /// <param name="SceneToLoad">The scene to load (if any) after the transition finishes. If empty no scene is loaded.</param>
        /// <param name="FadeColor">The color to fade in the screen.</param>
        /// <param name="TypeOfTransition">The type of transition to occur.</param>
        public static void StartSceneTransition(float Seconds, string SceneToLoad, Color FadeColor, TransitionState TypeOfTransition)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "ScreenTransitions"), "ScreenTransition");
            GameObject obj=Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
            ScreenTransitions transition=obj.GetComponent<ScreenTransitions>();
            transition.startNewSceneTransition(Seconds, SceneToLoad, FadeColor, TypeOfTransition);
        }

        public static void StartSceneTransition(float Seconds, string SceneToLoad, Color FadeColor, TransitionState TypeOfTransition,VoidDelegate OnTransitionFinish)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "ScreenTransitions"), "ScreenTransition");
            GameObject obj = Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
            ScreenTransitions transition = obj.GetComponent<ScreenTransitions>();
            transition.startNewSceneTransition(Seconds, SceneToLoad, FadeColor, TypeOfTransition,OnTransitionFinish);
            transition.currentState = TypeOfTransition;
        }

        /// <summary>
        /// Sets up the flags necessary to do a scene transition.
        /// </summary>
        /// <param name="TransitionTime"></param>
        /// <param name="FadeColor"></param>
        public static void PrepareForSceneFadeIn(float TransitionTime, Color FadeColor)
        {
            shouldFadeInAfterWarp = true;
            lastFadeInColor = FadeColor;
            targetFadeInTime = TransitionTime;
        }

    }
}
