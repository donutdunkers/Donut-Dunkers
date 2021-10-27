using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private GameObject activeUI;
    private Stack<GameObject> uiStack;

    private void Start()
    {
        uiStack = new Stack<GameObject>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
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
        if (uiStack.Count > 0)
        {
            activeUI.SetActive(false);
            activeUI = uiStack.Pop();
            activeUI.SetActive(true);
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
