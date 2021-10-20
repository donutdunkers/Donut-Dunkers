using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WelcomeUI : MonoBehaviour
{
    public Transform pressAnyKeyText;
    public float osciallationOffset = 0.1f;
    public string sceneToLoad = "Menu_Main";

    void Start()
    {
        //Check in here about past save state.
        //Bool isReturning = false
        //if isReturning sceneToLoad = mainMenu, else sceneToLoad = tutorial etc.
    }
    void Update()
    {
        UpdateText();
        if(CheckForInput())
        {
            LoadNextScene();
        }
    }

    void UpdateText()
    {
        float newScale;
        newScale = Mathf.Sin(Time.time) * osciallationOffset;
        pressAnyKeyText.localScale = new Vector3(1 + newScale, 1 + newScale, 1 + newScale);
    }

    void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    bool CheckForInput()
    {
        if(Input.anyKey)
        {
            return true;
            Debug.Log("Move to Start Scene");
        }

        return false;
    }
}
