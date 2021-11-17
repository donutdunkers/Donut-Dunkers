using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatsController : MonoBehaviour {

    public void UpdateStats() {

    }

    public void LoadLevel() {
        Debug.Log("Loading level: " + LevelInfo.Instance.currLevel.sceneName);
        StartCoroutine(LoadSceneAsync(LevelInfo.Instance.currLevel.sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneToLoad){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
