using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    private GameObject activeUI;
    private Stack<GameObject> uiStack;
    public Button continueButton;

	private void Awake() {
        Time.timeScale = 1f;
		if (SoundManager.Instance == null) {
			GameManager gameManager = Resources.Load<GameManager>("Game Manager");
			GameObject.Instantiate(gameManager.soundManager, Vector3.zero, Quaternion.identity);
		}

        LevelInfo.Instance.InitializeWorlds();

        if(SaveData.Instance.GetLastCompletedLevelIndex() != -1)
        {
            continueButton.interactable = true;
        }
		if (SoundManager.Instance == null) {
			GameManager gameManager = Resources.Load<GameManager>("Game Manager");
			GameObject.Instantiate(gameManager.soundManager, Vector3.zero, Quaternion.identity);
		}
	}

    private void Start()
    {
		Sound music = ScriptableSingleton<MusicEvent>.Instance.MainMenu;
		SoundManager.Instance.CrossFade(music.audioClip, AudioSettings.MusicVol, 2.5f);
		
        uiStack = new Stack<GameObject>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && uiStack.Count > 0)
        {
            EnableFromStack();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AddToStack(GameObject previous)
    {
        uiStack.Push(previous);
    }

    public void SetActiveCanvas(GameObject newActive)
    {
        activeUI = newActive;
    }

    public void EnableFromStack()
    {
        activeUI.SetActive(false);
        activeUI = uiStack.Pop();
        activeUI.SetActive(true);
    }

    public void ContinueFromLastLevel()
    {
        int levelIndex = SaveData.Instance.GetLastCompletedLevelIndex();
        int worldIndex = SaveData.Instance.GetLastCompletedWorldIndex();
        LoadNextScene(LevelInfo.Instance.FindNextLevel(worldIndex, levelIndex));
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
