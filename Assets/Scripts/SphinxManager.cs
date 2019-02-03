using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphinxManager : MonoBehaviour
    {
        GameManager gm;
        private AudioSource correctSound;
        private AudioSource wrongSound;
        public AudioClip correct;
        public AudioClip wrong;

        private void Awake()
        {
            gm = GameManager.instance;
        }

        // Use this for initialization
        void Start()
        {
           /* this.correctSound = this.gameObject.AddComponent<AudioSource>();
            this.correctSound.clip = this.correct;
            this.wrongSound.clip = this.wrong;
            this.correctSound.loop = false; **/

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void correctAnswer()
        {
            Debug.Log("right!!");
           // this.correctSound.Play();
        }

        public void wrongAnswer()
        {
            Debug.Log("wrong!!");
            //this.wrongSound.Play();
        }

        public void TimeOut()
        {
            Debug.Log("TimeOut!!");
        }


}