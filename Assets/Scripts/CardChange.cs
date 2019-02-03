using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChange : MonoBehaviour {

    public static CardChange instance = null;
    GameManager gm;
    GameObject[] card;
    ArrayList animals;
    static int[] answers;
    Quiz[] quizes;

    CardChange() { }

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }

        gm = GameManager.instance;
        quizes = gm.GetQuiz();

        animals = new ArrayList();
        card = new GameObject[10];
        answers = new int[10];
        for (int i = 0; i < card.Length; i++)
        {
            card[i] = GameObject.Find("select" + i);
        }

        foreach (GameObject gameObject in FindObjectsOfType(typeof(GameObject)))
        {
            if(gameObject.name.Contains("animal"))
            {
                animals.Add(gameObject);
                //Instantiate((GameObject)animals[j], card[i].transform.position, Quaternion.identity, card[i].transform);
            }
        }

        ResetItems();
    }

    public void ResetItems()
    {
        answers = GetRandomArray(answers.Length, 0, animals.Count);

        for(int i = 0; i < gm.clones.Count; i++)
        {
            Destroy(GameObject.Find((string)gm.clones[i]));
        }

        gm.clones.Clear();

        for (int i = 0; i < card.Length; i++)
        {
            GameObject clone = Instantiate((GameObject)animals[answers[i]], card[i].transform.position, Quaternion.identity, card[i].transform);
            clone.layer = 0;
            gm.clones.Add(clone.name);
        }

        for(int i = 0; i < animals.Count; i++)
        {
            GameObject g = (GameObject)animals[i];
            g.SetActive(false);
        }
    }

    public int[] GetRandomArray(int length, int min, int max)
    {
        int[] randArray = new int[length];
        int count = 0;
        while (count == 0)
        {
            randArray = new int[length];
            int random = Random.Range(0, 10);
            bool isSame;
            bool isAnswer = false;

            while (isAnswer)
            {
                string tempAnswer = gm.GetRandomAnswer();

                for (int i = 0; i < animals.Count; i++)
                {
                    GameObject obj = (GameObject)animals[i];
                    if (obj.name.Contains(tempAnswer))
                    {
                        randArray[random] = i;
                        isAnswer = true;
                    }
                }
            }
            for (int i = 0; i < length; i++)
            {
                while (true)
                {
                    int r = Random.Range(min, max);
                    randArray[i] = r;
                    isSame = false;

                    for (int j = 0; j < i; j++)
                    {
                        if (randArray[j] == randArray[i])
                        {
                            isSame = true;
                            break;
                        }
                    }

                    if (!isSame) break;
                }
            }

            string[] answers = gm.GetAnswerArray();

            for (int i = 0; i < randArray.Length; i++)
            {
                string objectName = ((GameObject)animals[randArray[i]]).name;

                for (int j = 0; j < answers.Length; j++)
                {
                    if (objectName.Contains(answers[j]))
                    {
                        count++;
                    }
                }
            }
        }

        gm.answerCount = count;
        gm.ResetQuiz();
        return randArray;
    }

}
