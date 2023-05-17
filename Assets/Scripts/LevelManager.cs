using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    LevelSelect ls;
    LevelManager instance;

    private void Start()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        ls = FindObjectOfType<LevelSelect>();
    }

    public void LoadGame()
    {
        switch (ls.levelSelectedNumber)
        {
            case 1:
                SceneManager.LoadScene("game");
                break;
            case 2:
                SceneManager.LoadScene("ohio");
                break;
            default:
                Debug.LogWarning("Could not get level index from level select!");
                SceneManager.LoadScene("main menu");
                break;
        }
    }

    public void RetryGame()
    {
        if (FindObjectOfType<TowerInfo>() != null) //only work if towerinfo exists
        {
            switch (FindObjectOfType<TowerInfo>().levelindex)
            {
                case 1:
                    SceneManager.LoadScene("game");
                    break;
                case 2:
                    SceneManager.LoadScene("ohio");
                    break;
                default:
                    Debug.LogWarning("Could not get level index from towerinfo!");
                    SceneManager.LoadScene("main menu");
                    break;
            }
        } else {
            Debug.LogWarning("Towerinfo does not exist!");
                    SceneManager.LoadScene("main menu");
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

