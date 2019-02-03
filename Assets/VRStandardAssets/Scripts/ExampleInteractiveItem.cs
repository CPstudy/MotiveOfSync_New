using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private VRInteractiveItem m_InteractiveItem;

        public string itemName;
        public Animator animator;
        public Animation animationer;
        CardChange card;

        string question;
        Text txtQuestion;

        private void Awake()
        {
            //m_NormalMaterial = m_Renderer.material;
            //animator = GetComponent<Animator>();
            
            
        }


        private void OnEnable()
        {
            //Debug.Log("Enable");
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnClick += Playanima; //애니메이션
            
            
        }

        private void OnDisable()
        {
            //Debug.Log("Disable");
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnClick -= Playanima; //애니메이션
        }

        //Handle the Over event
        private void HandleOver()
        {            
            //m_Renderer.material = m_OverMaterial;
            //Debug.Log("Show over state");
        }

        //Handle the Out event
        private void HandleOut()
        {
            
            //m_Renderer.material = m_NormalMaterial;
            //Debug.Log("Show out state");
        }

        //Handle the Click event
        private void HandleClick()
        {
            txtQuestion = GameObject.Find("Question").GetComponent<Text>();
            GameManager gm = GameManager.instance;
            if (card == null)
                card = CardChange.instance;
            
            if (itemName.Equals("sphinx"))
            {
                AnswerScript answerScript = AnswerScript.instance;
                if (gm.CheckAnswer())
                {
                    GameObject.Find("SoundCorrect").GetComponent<AudioSource>().Play();
                    answerScript.correct.SetActive(true);
                    Invoke("CorrectAnswer", 2);
                } else
                {
                    GameObject.Find("SoundWrong").GetComponent<AudioSource>().Play();
                    answerScript.wrong.SetActive(true);
                    Invoke("WrongAnswer", 2);
                }
            } else
            {
                gm.AddText(itemName);
                //SphinxManager sphinx = new SphinxManager();
            }
        }

        private void CorrectAnswer()
        {
            GameManager gm = GameManager.instance;
            AnswerScript answerScript = AnswerScript.instance;
            answerScript.correct.SetActive(false);

            gm.Level = gm.Level + 1;
            SceneManager.LoadScene("level", LoadSceneMode.Single);
            if (gm.Level >= 3)
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }

        private void WrongAnswer()
        {
            AnswerScript answerScript = AnswerScript.instance;
            answerScript.wrong.SetActive(false);
        }

        private void Playanima()
        {
            animator = GetComponent<Animator>();


        }
    }
}