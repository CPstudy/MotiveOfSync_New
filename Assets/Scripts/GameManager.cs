using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private Quiz[] quizes;

    private GameManager()
    {
        //임의로 객체 생성 못 하게 막음
    }

    public Text itemText;
    public Text questionText;
    ArrayList list = new ArrayList();
    public ArrayList clones = new ArrayList();
    public Dictionary<string, string> dictionary = new Dictionary<string, string>();
    Quiz[] array = new Quiz[3];
    public static GameManager instance = null;
    public int answerCount = 0;
    public int Mode { get; set; }
    public string Stage { get; set; }
    public int Level { get; set; }

    public bool isTutorial = false;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            quizes = new Quiz[9];
            for (int i = 0; i < quizes.Length; i++)
            {
                quizes[i] = new Quiz();
            }

            quizes[0].Question = "육지 활동이 불가능한 것을 @개 고르시오.";
            quizes[0].Answers = new string[] { "whale", "squid", "manta", "stingray", "shark", "octopus" };

            quizes[1].Question = "이빨이 없는 동물을 @개 고르시오. ";
            quizes[1].Answers = new string[] { "pheas", "crow" };

            quizes[2].Question = "포유류가 아닌 것을 @개 고르시오.";
            quizes[2].Answers = new string[] { "crow", "penguin", "pheas", "turtle", "squid", "manta", "stingray", "chrocodile", "octopus" };

            quizes[3].Question = "'낮말은 A가 듣고 밤말은 쥐가 듣는다.' 여기에 들어갈 A, @개를 모두 고르시오.";
            quizes[3].Answers = new string[] { "pheas", "crow", "penguin" };

            quizes[4].Question = "'A 싸움에 새우등 터진다.'에서 A에 관련된 동물을 고르시오.";
            quizes[4].Answers = new string[] { "whale" };

            quizes[5].Question = "'궁지에 물린 쥐는 A를 문다.'에서 A에 관련된 동물을고르시오. ";
            quizes[5].Answers = new string[] { "cat" };

            quizes[6].Question = "토사구팽(兎死狗烹) 에서 토(兎)에 해당하는 동물을 찾으시오.";
            quizes[6].Answers = new string[] { "rabbit" };

            quizes[7].Question = "반포지효(反哺之孝) 와 관련된 동물을 찾으시오.";
            quizes[7].Answers = new string[] { "crow" };

            quizes[8].Question = "오비이락(烏飛梨落) 와 관련된 동물을 찾으시오. ";
            quizes[8].Answers = new string[] { "crow" };

            dictionary.Add("squirrel", "다람쥐");
            dictionary.Add("rat", "쥐");
            dictionary.Add("rabbit", "토끼");
            dictionary.Add("crow", "까마귀");
            dictionary.Add("penguin", "펭귄");
            dictionary.Add("whale", "고래");
            dictionary.Add("pheas", "꿩");
            dictionary.Add("turtle", "거북이");
            dictionary.Add("squid", "오징어");
            dictionary.Add("manta", "큰가오리");
            dictionary.Add("stingray", "가오리");
            dictionary.Add("hippos", "하마");
            dictionary.Add("shark", "상어");
            dictionary.Add("crocodile", "악어");
            dictionary.Add("cat", "고양이");
            dictionary.Add("octopus", "문어");
            dictionary.Add("rhino", "코뿔소");
        }
        DontDestroyOnLoad(this);
    }

    public void InitTutorial()
    {
        questionText = GameObject.Find("Question").GetComponent<Text>();
        itemText = GameObject.Find("Canvas/Panel/answers").GetComponent<Text>();
        list.Clear();
        questionText.text = "부리를 가진 동물 3마리를 고르시오.";
        itemText.text = "";
    }

    private void MakeQuiz()
    {
        questionText = GameObject.Find("Question").GetComponent<Text>();
        itemText = GameObject.Find("Canvas/Panel/answers").GetComponent<Text>();
    }

    public Quiz[] GetQuiz()
    {
        MakeQuiz();
        array[0] = quizes[Mode * 3];
        array[1] = quizes[Mode * 3 + 1];
        array[2] = quizes[Mode * 3 + 2];

        ResetQuiz();

        return array;
    }

    public void SetQuiz()
    {
        array[0] = quizes[Mode * 3];
        array[1] = quizes[Mode * 3 + 1];
        array[2] = quizes[Mode * 3 + 2];
    }

    public string GetCurrentQuestion()
    {
        string txt = "";

        if(Mode == 0)
        {
            txt = "질문에 해당하는 것을 모두 고르세요.";
        } else if(Mode == 1)
        {
            txt = "A에 들어갈 것을 모두 고르세요.";
        } else
        {
            txt = "사자성어에 관련된 것을 모두 고르세요.";
        }
        return txt;
    }

    public void ResetQuiz()
    {
        questionText.text = array[Level].Question.Replace("@", "" + answerCount);
        itemText.text = "";
        list.Clear();
    }

    public string[] GetAnswerArray()
    {
        return array[Level].Answers;
    }

    public string GetRandomAnswer()
    {
        string[] arr = array[Level].Answers;
        int random = Random.Range(0, arr.Length);
        Debug.Log("Random Asnwer = " + arr[random]);
        return arr[random];
    }

    public void AddText(string str)
    {
        itemText.text = "";
        if (!list.Contains(str)) // 리스트에 텍스트가 없으면 str은 넣는다.
        {
            list.Add(str);
            questionText.text = dictionary[str] + "을(를) 등록했습니다.";
        }
        else
        {
            list.Remove(str);
            questionText.text = dictionary[str] + "을(를) 제거했습니다.";
        }
        Invoke("ReturnQuestion", 2);

        foreach (string s in list)
        {   // 리스트값을 순서대로 출력
            itemText.text = itemText.text + dictionary[s] + "\n";
        }

    }

    private void ReturnQuestion()
    {
        if (!isTutorial)
        {
            questionText.text = array[Level].Question.Replace("@", "" + answerCount);
        } else
        {
            questionText.text = "부리를 가진 동물 3마리를 고르시오.";
        }
    }

    public bool CheckAnswer()
    {
        string[] arr = array[Level].Answers;    //문제의 정답
        ArrayList answers = new ArrayList();    //정답이 들어간 리스트

        if (list.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < arr.Length; i++) //정답 배열을 리스트에 넣음
        {
            answers.Add(arr[i]);
            Debug.Log("answers = " + answers[i]);
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (!answers.Contains(list[i]))
            {
                return false;
            }
        }
        return true;
    }

    public int GetListCount()
    {
        return list.Count;
    }

    public string[] GetListItems()
    {
        string[] arr = new string[list.Count];

        for(int i = 0; i < list.Count; i++)
        {
            arr[i] = list[i].ToString();
        }

        return arr;
    }

    public void ClickMode()
    {

    }
}
