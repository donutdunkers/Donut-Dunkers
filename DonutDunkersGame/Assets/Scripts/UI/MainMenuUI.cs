using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    private GameObject activeUI;
    private Stack<GameObject> uiStack;

	private void Awake() {
		if (SoundManager.Instance == null) {
			GameManager gameManager = Resources.Load<GameManager>("Game Manager");
			GameObject.Instantiate(gameManager.soundManager, Vector3.zero, Quaternion.identity);
		}
	}

    private void Start()
    {
		// Currently there is no main menu music- hence why it is set to play 'null'
		SoundManager.Instance.CrossFade(null, 0f, 1f);
		
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
