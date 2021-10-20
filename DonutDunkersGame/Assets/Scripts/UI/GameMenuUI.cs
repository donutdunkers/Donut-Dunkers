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

    private void Start() {
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);
    }



    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void RestartLevel() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void NextLevel() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitToMain() {
        //SceneManager.LoadScene(0);
    }

    public void TogglePauseMenu() {
        bool isPaused = pauseMenu.activeSelf;
        if (isPaused) {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        } else {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        
        }
    
    }

    public void ShowEndScreen() {
        endGameMenu.SetActive(true);
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