using Assets.Scripts.Content;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class SplashScreen:Menu
    {
        DeltaTimer timerToShow;


        public override void Start()
        {
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            Image background = canvas.transform.Find("Image").gameObject.GetComponent<Image>();
                background.rectTransform.sizeDelta = new Vector2(Camera.main.pixelRect.width, Camera.main.pixelRect.height);

            background.sprite = ContentManager.Instance.loadSprite(Path.Combine("ContentPacks", "BaseGame", "Graphics", "Menus", "SplashScreen", "ACoolFlowerThing.png"));

            Menu.ActiveMenu = this;

            ScreenTransitions.StartSceneTransition(3, "", Color.white, ScreenTransitions.TransitionState.FadeIn,new VoidDelegate(fadeInFinished));
            timerToShow = new DeltaTimer(3, Enums.TimerType.CountDown, false, new VoidDelegate(fadeToMainMenu));

            this.menuCursor = GameInput.GameCursor.Instance;
        }

        public override void Update()
        {
            timerToShow.Update();
        }

        private void fadeInFinished()
        {
            timerToShow.start();
        }
        private void fadeToMainMenu()
        {
            Scripts.Utilities.ScreenTransitions.StartSceneTransition(3, "MainMenu", Color.white, Utilities.ScreenTransitions.TransitionState.FadeOut);
        }

        public override void exitMenu()
        {
            base.exitMenu();
        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void setUpForSnapping()
        {
            //do nothing.
        }


    }
}
