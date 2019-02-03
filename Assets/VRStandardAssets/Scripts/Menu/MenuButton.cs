using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Menu
{
    // This script is for loading scenes from the main menu.
    // Each 'button' will be a rendering showing the scene
    // that will be loaded and use the SelectionRadial.
    public class MenuButton : MonoBehaviour
    {
        GameManager gm;
        public event Action<MenuButton> OnButtonSelected;                   // This event is triggered when the selection of the button has finished.
        public string name;

        [SerializeField] private string m_SceneToLoad;                      // The name of the scene to load.
        [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.
        [SerializeField] private Material m_NormalMaterial;
        [SerializeField] private Material m_ClickedMaterial;

        private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.

        public GameObject menu;
        public GameObject mode;
        public GameObject stage;
        public GameObject license;

        private void OnEnable ()
        {
            menu = GameObject.Find("Menu");
            mode = GameObject.Find("Mode");
            stage = GameObject.Find("Stage");
            license = GameObject.Find("License");
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
        }


        private void OnDisable ()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
        }

        private void HandleOver()
        {
            // When the user looks at the rendering of the scene, show the radial.

            m_GazeOver = true;
        }

        private void HandleOut()
        {
            m_GazeOver = false;
        }

        private void HandleClick()
        {
            gm = GameManager.instance;
            switch(name)
            {
                case "start":
                    SceneManager.LoadScene("Mode", LoadSceneMode.Single);
                    break;

                case "tutorial":
                    gm.isTutorial = true;
                    SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
                    break;

                case "license":
                    SceneManager.LoadScene("License", LoadSceneMode.Single);
                    break;

                case "exit":
                    Application.Quit();
                    break;

                case "exit2":
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                    break;

                case "animal":
                    //gm.gameObjects[1].SetActive(false);
                    //gm.gameObjects[2].SetActive(true);
                    gm.Mode = 0;
                    gm.Level = 0;
                    SceneManager.LoadScene("level", LoadSceneMode.Single);
                    break;

                case "proverb":
                    gm.Mode = 1;
                    gm.Level = 0;
                    SceneManager.LoadScene("level", LoadSceneMode.Single);
                    break;

                case "flag":
                    gm.Mode = 2;
                    gm.Level = 0;
                    SceneManager.LoadScene("level", LoadSceneMode.Single);
                    break;

                case "box_back":
                    gm.Level = 0;
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                    break;


                case "box_exit":
                    Application.Quit();
                    break;

                case "exit_license":
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                    break;

                case "box_restart":
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
            }

        }


        private void HandleSelectionComplete()
        {
            // If the user is looking at the rendering of the scene when the radial's selection finishes, activate the button.
            if(m_GazeOver)
            {
                SceneManager.LoadScene("Stage1");
            }
        }


        private void ActivateButton()
        {

            // If anything is subscribed to the OnButtonSelected event, call it.
            if (OnButtonSelected != null)
                OnButtonSelected(this);

            // Load the level.
            SceneManager.LoadScene("New Scene");
        }
    }
}