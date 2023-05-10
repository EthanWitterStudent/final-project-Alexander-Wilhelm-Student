using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    LevelSelect ls;
    // ScoreKeeper scoreKeeper;

    // void Awake()
    // {
    //     scoreKeeper = FindObjectOfType<ScoreKeeper>();
    // }

    private void Start() 
    {
        ls = FindObjectOfType<LevelSelect>();
    }

    public void LoadGame()
    {
        switch(ls.levelSelectedNumber)
        {
            case 1:
                SceneManager.LoadScene("game");
                break;
            case 2:
                SceneManager.LoadScene("ohio");
                break;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main menu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

