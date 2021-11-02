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

    private bool gameEnded = false;

    private void Start() {
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);
        Time.timeScale = 1;
    }



    private void Update() {
        bool isOutOfTurns = LevelData.Instance.Turns <= 0;

        if (isOutOfTurns && !BallController.Instance.IsMoving || LevelData.Instance.RingsCollected == LevelData.Instance.RingsInLevel)
        {
            ShowEndScreen(isOutOfTurns);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    public void RestartLevel() {
        HidePauseMenu();
        gameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        if (gameEnded) {
            return;
        }

        bool isPaused = pauseMenu.activeSelf;
        if (isPaused) {
            HidePauseMenu();
        } else {
            ShowPauseMenu();
        }  
    }
    //time scale doesn't really do what we want here, need to block input seperately
    private void ShowPauseMenu() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void HidePauseMenu() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowEndScreen(bool isOutOfTurns) {
        HidePauseMenu();
        endGameMenu.SetActive(true);
        endGameMenu.GetComponent<LevelEndUI>().SetLevelEndUI(isOutOfTurns, LevelData.Instance.RingsCollected, LevelData.Instance.RingsInLevel, LevelData.Instance.TurnsTaken);
        gameEnded = true;
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