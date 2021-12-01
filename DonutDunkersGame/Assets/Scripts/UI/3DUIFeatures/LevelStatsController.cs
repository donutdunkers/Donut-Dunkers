using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelStatsController : MonoBehaviour {

    public TextMeshProUGUI worldNameText;
    public TextMeshProUGUI levelNumText;
    public TextMeshProUGUI minNumMovesText;
    public TextMeshProUGUI numStarsText;

    public void UpdateStats() {
        LevelSettings currLevel = LevelInfo.Instance.currLevel;

        worldNameText.SetText(LevelInfo.Instance.currWorld.worldName);
        levelNumText.SetText("Level " + (currLevel.levelIndex + 1));
        minNumMovesText.SetText(currLevel.minTurns.ToString());
        numStarsText.SetText(currLevel.maxStars.ToString());
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
