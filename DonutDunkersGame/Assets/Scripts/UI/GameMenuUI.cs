 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour {
    
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField, Tooltip("Set this for each level")]
    private string nextLevelSceneName;

    private void Start() {
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);
    }



    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            ShowEndScreen();
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TogglePauseMenu();
    }
    
    public void NextLevel() {
        if (nextLevelSceneName != "") {
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }

    public void ExitToMain() {
        SceneManager.LoadScene("Menu_Main");
    }

    public void TogglePauseMenu() {
        bool isPaused = pauseMenu.activeSelf;
        if (isPaused) {
            //this is probably not how we want to handle this
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        } else {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        
        }
    
    }

    public void ShowEndScreen() {
        endGameMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadNextScene(string sceneToLoad)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}