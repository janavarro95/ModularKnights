using Assets.Scripts.Utilities.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameInformation
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager Manager;

        public Serializer serializer;
        /// <summary>
        /// Initializing the game manager.
        /// </summary>
        private void Awake()
        {
            Manager = this;
            DontDestroyOnLoad(this.gameObject);
            initializeGame();
            serializer = new Serializer();
        }

        // Start is called before the first frame update
        void Start()
        {

        }


        /// <summary>
        /// Initializes the game.
        /// </summary>
        private void initializeGame()
        {
            if (Serializer.JSONSerializer == null) Serializer.JSONSerializer = new Assets.Scripts.Utilities.Serialization.Serializer();

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;


        }

        /// <summary>
        /// When a scene is unloaded it calls this info here.
        /// </summary>
        /// <param name="arg0"></param>
        private void OnSceneUnLoaded(Scene arg0)
        {

        }

        /// <summary>
        /// When a scene is loaded it does some set up stuff here.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="arg1"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {

        }

        // Update is called once per frame
        void Update()
        {


        }
    }
}