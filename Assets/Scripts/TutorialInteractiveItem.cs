using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TutorialInteractiveItem : MonoBehaviour {

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
        Debug.Log("Enable");
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;


    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
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
        TutorialScript ts = TutorialScript.instance;

        if (ts.isClicked)
        {
            if (card == null)
                card = CardChange.instance;

            if (itemName.Equals("sphinx"))
            {
                AnswerScript answerScript = AnswerScript.instance;

                bool isAnswer = true;
                string[] arr = gm.GetListItems();

                for (int i = 0; i < arr.Length; i++)
                {
                    if (!arr[i].Equals("penguin") && !arr[i].Equals("pheas") && !arr[i].Equals("crow"))
                    {
                        isAnswer = false;
                    }
                }

                if (isAnswer && arr.Length == 3)
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
            }
            else
            {

                gm.AddText(itemName);

                if (ts.num == 8)
                {
                    ts.ResumeScript();
                } else if(ts.num == 13 && gm.GetListCount() == 0)
                {
                    ts.ResumeScript();
                } else if(ts.num == 20)
                {
                    bool isAnswer = true;
                    string[] arr = gm.GetListItems();

                    for(int i = 0; i < arr.Length; i++)
                    {
                        if(!arr[i].Equals("penguin") && !arr[i].Equals("pheas") && !arr[i].Equals("crow"))
                        {
                            isAnswer = false;
                        }
                    }

                    if(isAnswer && arr.Length == 3)
                    {
                        ts.ResumeScript();
                    }
                }
            }
        }
    }

    private void CorrectAnswer()
    {
        GameManager gm = GameManager.instance;
        AnswerScript answerScript = AnswerScript.instance;
        answerScript.correct.SetActive(false);
        NextStep();
    }

    private void WrongAnswer()
    {
        AnswerScript answerScript = AnswerScript.instance;
        answerScript.wrong.SetActive(false);
    }

    private void NextStep()
    {
        TutorialScript ts = TutorialScript.instance;
        ts.ResumeScript();
    }
}
