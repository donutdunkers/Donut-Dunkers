using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    private GameObject activeUI;
    private Stack<GameObject> uiStack;
    public Button continueButton;
    public AudioSettings audioSettings;

	private void Awake() {
        Time.timeScale = 1f;
		if (SoundManager.Instance == null) {
			GameManager gameManager = Resources.Load<GameManager>("Game Manager");
			GameObject.Instantiate(gameManager.soundManager, Vector3.zero, Quaternion.identity);
		}

        if (SaveData.Instance.GetLastCompletedLevelIndex() >= 0 && LevelInfo.Instance.allowContinue)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
		if (SoundManager.Instance == null) {
			GameManager gameManager = Resources.Load<GameManager>("Game Manager");
			GameObject.Instantiate(gameManager.soundManager, Vector3.zero, Quaternion.identity);
		}
	}

    private void Start()
    {
        audioSettings.InitializeAudio();
        LevelInfo.Instance.InitializeWorlds();

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

        LevelSettings nextLevel;
        if (LevelInfo.Instance.FindNextLevel(worldIndex, levelIndex, out nextLevel))
        {
            LevelInfo.Instance.currLevel = nextLevel;
            if (nextLevel.levelIndex < levelIndex)
            {
                LevelInfo.Instance.currWorld = LevelInfo.Instance.allWorlds[worldIndex + 1];
            }
            else
            {
                LevelInfo.Instance.currWorld = LevelInfo.Instance.allWorlds[worldIndex];
            }
            LoadNextScene(nextLevel.sceneName);
        }
        else
        {
            continueButton.interactable = false;
            LevelInfo.Instance.allowContinue = false;
        }
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
