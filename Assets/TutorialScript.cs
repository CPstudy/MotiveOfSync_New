using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public static TutorialScript instance = null;
    private GameManager gm;
    private AudioSource soundOpen, soundClose;

    private string[] scripts =
    {
        "지금부터 튜토리얼을 시작합니다.",
        "마커를 바라봐 선택지를 확인해보세요.",
        "",
        "잘 하셨습니다.",
        "마커를 가운데 조준점에 가져다대서",
        "마커를 인식시켜주세요.",
        "조준점의 게이지가 다 차면",
        "값을 등록할 수 있습니다.",
        "",
        "잘 하셨습니다.",
        "같은 마커를 다시 한 번 인식시키면",
        "등록한 값을 지울 수 있습니다.",
        "등록된 값을 모두 지우세요.",
        "",
        "잘 하셨습니다.",
        "이제 문제를 풀어볼 시간입니다.",
        "화면 상단에 있는 문제를 읽고",
        "답이라고 생각되는 값을 저장시키면 됩니다.",
        "이 문제의 답은 '펭귄', '꿩', '까마귀'입니다.",
        "'펭귄', '꿩', '까마귀' 모두 찾아주세요.",
        "",
        "잘 하셨습니다.",
        "이제 정답을 확인받을 차례입니다.",
        "스핑크스 마커를 찾아 인식시키면",
        "정답을 확인받을 수 있습니다.",
        "스핑크스를 찾아 정답을 맞춰보세요.",
        "",
        "잘 하셨습니다.",
        "이제 모든 튜토리얼이 끝났습니다.",
        "메인 화면으로 돌아갑니다.",
        ""
    };

    private GameObject panel;
    private Text txtExp;
    private Animator animator;

    private float timeSpan;
    private float checkTime;
    public int num = 0;
    public bool isLoop = true;
    public bool isClicked = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            gm = GameManager.instance;
            gm.InitTutorial();
            gm.isTutorial = true;

            txtExp = GameObject.Find("explanation").GetComponent<Text>();
            panel = GameObject.Find("panel_dialog");
            soundOpen = GameObject.Find("SoundOpen").GetComponent<AudioSource>();
            soundClose = GameObject.Find("SoundClose").GetComponent<AudioSource>();

            if (panel != null)
            {
                animator = panel.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("open", true);
                    soundOpen.Play();
                }
            }

            timeSpan = 0.0f;
            checkTime = 2.0f;

            DisplayText();
        }
    }

    private void Update()
    {
        if (isLoop)
        {
            timeSpan += Time.deltaTime;
            if (timeSpan > checkTime)
            {
                NextScript();
                timeSpan = 0;
            }
        }
    }

    private string GetScript(int n)
    {
        return scripts[n];
    }

    private void DisplayText()
    {
        txtExp.text = scripts[num];
    }

    public void NextScript()
    {
        num++;
        if (num < scripts.Length)
        {
            if (num == 2 || num == 8 || num == 13 || num == 20 || num == 26)
            {
                animator.SetBool("open", false);
                soundClose.Play();
                PauseScript();
            } else if(num == 30)
            {
                PauseScript();
                gm.isTutorial = false;
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
            else
            {
                DisplayText();
            }
        }
    }

    private void PauseScript()
    {
        isLoop = false;
        isClicked = true;
        timeSpan = 0.0f;
    }

    public void ResumeScript()
    {
        Debug.Log("ResultScript");
        animator.SetBool("open", true);
        soundOpen.Play();
        isLoop = true;
        isClicked = false;
        NextScript();
    }
}
