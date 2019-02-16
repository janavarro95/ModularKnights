using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// Deals with the main menu.
    /// </summary>
    public class MainMenu:Menu
    {
        [SerializeField]
        MenuComponent startButton;
        [SerializeField]
        MenuComponent quitButton;
        [SerializeField]
        MenuComponent optionsButton;

        [SerializeField]
        MenuComponent creditsButton;
        [SerializeField]
        MenuComponent saveLoadButton;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {
            GameObject canvas=this.transform.Find("Canvas").gameObject;
            startButton = new MenuComponent(canvas.transform.Find("StartButton").gameObject.GetComponent<Button>());
            quitButton = new MenuComponent(canvas.transform.Find("QuitButton").gameObject.GetComponent<Button>());
            optionsButton =new MenuComponent(canvas.transform.Find("OptionsButton").gameObject.GetComponent<Button>());

            creditsButton = new MenuComponent(canvas.transform.Find("CreditsButton").gameObject.GetComponent<Button>());
            saveLoadButton =new MenuComponent(canvas.transform.Find("SaveLoadButton").gameObject.GetComponent<Button>());

            menuCursor = GameCursor.Instance;
            Menu.ActiveMenu = this;

            setUpForSnapping();

        }

        public override void setUpForSnapping()
        {
            startButton.setNeighbors(null, optionsButton, null, null);
            quitButton.setNeighbors(saveLoadButton, creditsButton, null, null);
            optionsButton.setNeighbors(startButton, saveLoadButton, null, null);
            saveLoadButton.setNeighbors(optionsButton, quitButton, null, null);
            creditsButton.setNeighbors(quitButton, null, null, null);
            this.selectedComponent = startButton;
            menuCursor.snapToCurrentMenuComponent();

        }

        public override bool snapCompatible()
        {
            return true;
        }

        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            if (menuCursor == null)
            {
                Debug.Log("Cursor is null");
            }

            if (GameCursor.SimulateMousePress(startButton))
            {
                this.startButtonClick();
                return;
            }

            if (GameCursor.SimulateMousePress(quitButton))
            {
                this.exitButtonClick();
                return;
            }

            if (GameCursor.SimulateMousePress(optionsButton))
            {
                this.optionsButtonClick();
                return;
            }

            if (GameCursor.SimulateMousePress(saveLoadButton))
            {
                this.openSaveLoadSelectMenu();
                return;
            }

            if (GameCursor.SimulateMousePress(creditsButton))
            {
                this.creditsButtonClick();
                return;
            }
        }
        /// <summary>
        /// Close the active menu.
        /// </summary>
        public override void exitMenu()
        {
            Destroy(this.gameObject);
            Menu.ActiveMenu = null;
        }   

        /// <summary>
        /// What happens when the start button is clicked.
        /// </summary>
        public void startButtonClick()
        {
            Debug.Log("ADD in start button click!!");
            //SceneManager.LoadScene("preloadScene");
        }

        /// <summary>
        /// What happens when the quit button is clicked.
        /// </summary>
        public void exitButtonClick()
        {
            Application.Quit();
        }

        /// <summary>
        /// What happens when the option button is clicked.
        /// </summary>
        public void optionsButtonClick()
        {
            Debug.Log("Add in options!");
            //Destroy(this.gameObject); //necessary to remove the main menu from the screen.
        }

        public void creditsButtonClick()
        {
            Debug.Log("ADD IN CREDITS!");
        }

        public void openSaveLoadSelectMenu()
        {
            Debug.Log("ADD IN Save/Load system!");
        }
    }
}
