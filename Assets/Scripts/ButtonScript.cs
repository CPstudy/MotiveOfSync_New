using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour {


	public void ClickExit()
    {
        Application.Quit();
    }

    public void ClickStart()
    {
        SceneManager.LoadScene("Mode", LoadSceneMode.Single);
    }

    public void ClickLicense()
    {
        SceneManager.LoadScene("License", LoadSceneMode.Single);
    }

    public void ClickTutorial()
    {
        GameManager gm = GameManager.instance;
        gm.isTutorial = true;
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void ClickExit1()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ClickExit2()
    {

    }

    public void Mode1()
    {
        GameManager gm = GameManager.instance;
        gm.Mode = 0;
        gm.Level = 0;
        SceneManager.LoadScene("level", LoadSceneMode.Single);
    }

    public void Mode2()
    {
        GameManager gm = GameManager.instance;
        gm.Mode = 1;
        gm.Level = 0;
        SceneManager.LoadScene("level", LoadSceneMode.Single);
    }

    public void Mode3()
    {
        GameManager gm = GameManager.instance;
        gm.Mode = 2;
        gm.Level = 0;
        SceneManager.LoadScene("level", LoadSceneMode.Single);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackMenu()
    {
        GameManager gm = GameManager.instance;
        gm.Level = 0;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
