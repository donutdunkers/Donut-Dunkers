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
    public float sceneLoadDelayTime = 0.0f; //Ryan Pumo Temp WebGL Build Code!!
	
	private bool gotInput = false;

    void Start()
    {
        //Check in here about past save state.
        //Bool isReturning = false
        //if isReturning sceneToLoad = mainMenu, else sceneToLoad = tutorial etc.
		this.gotInput = false;
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
		this.gotInput = true;
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(sceneLoadDelayTime); //Ryan Pumo Temp WebGL Build Code!!
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    bool CheckForInput()
    {
		if (this.gotInput) {
			return false;
		}
        if(Input.anyKey)
        {
            //ResumeAudio();
            return true;
            Debug.Log("Move to Start Scene");
        }

        return false;
    }

    bool audioResumed = false;

    /*public void ResumeAudio() {
        if (!audioResumed) {
            var result = RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);
            result = RuntimeManager.CoreSystem.mixerResume();
            Debug.Log(result);
            audioResumed = true;
        }
    }*/
}
