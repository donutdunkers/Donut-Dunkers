using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour {
	
    private static GameMenuUI _Instance;
    public static GameMenuUI Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameMenuUI>();
            }
            return _Instance;
        }
    }

    
    
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject endGameMenu;
    [SerializeField, Tooltip("Set this for each level")]
    private string nextLevelSceneName;

    private bool gameEnded = false;
	
	public bool IsPauseMenuActive {
		get {
			return this.pauseMenu.activeSelf;
		}
	}
	
	public bool IsEndGameMenuActive {
		get {
			return this.endGameMenu.activeSelf;
		}
	}

    

    private void Start() {
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);
        Time.timeScale = 1;
    }



    private void Update() {
        bool isOutOfTurns = LevelData.Instance.Turns <= 0 && BallController.Instance.CanAct;

        if (isOutOfTurns || LevelData.Instance.RingsInLevel > 0 && LevelData.Instance.RingsCollected == LevelData.Instance.RingsInLevel)
        {
            ShowEndScreen(isOutOfTurns);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel() {
			HidePauseMenu();
	     	HideEndScreen();
			gameEnded = false;
	    	LevelData.Instance.ResetLevel();
    }
    
    public void NextLevel() {
        if (nextLevelSceneName != "") {
            LoadNextScene(LevelInfo.Instance.currWorld.GetNextLevel());
        }
    }

    public void ExitToMain() {
        LoadNextScene("Menu_Main");
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
		PlayerGridSelection.Instance.CanSelect = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void HidePauseMenu() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
		if (this.pauseRoutine != null) {
			this.StopCoroutine(this.pauseRoutine);
		}
		this.pauseRoutine = this.StartCoroutine(this.PauseRoutine());
    }
	
	private Coroutine pauseRoutine;
	
	private IEnumerator PauseRoutine() {
		yield return new WaitForSeconds(0.05f);
		PlayerGridSelection.Instance.CanSelect = true;
		yield return null;
	}
		

    public void ShowEndScreen(bool isOutOfTurns) {
        HidePauseMenu();
        endGameMenu.SetActive(true);
        if (nextLevelSceneName.Equals(""))
        {
            GameObject.Find("NextLevelButton").GetComponent<Button>().interactable = false;
        }
        endGameMenu.GetComponent<LevelEndUI>().SetLevelEndUI(isOutOfTurns, LevelData.Instance.RingsCollected, LevelData.Instance.RingsInLevel, LevelData.Instance.TurnsTaken);
        gameEnded = true;
        Time.timeScale = 0;
    }
	
	public void HideEndScreen() {
		endGameMenu.SetActive(false);
		gameEnded = false;
		Time.timeScale = 1f;
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