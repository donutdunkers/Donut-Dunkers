using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Pixelplacement;

public class LevelStatsController : MonoBehaviour {

    public TextMeshProUGUI worldNameText;
    public TextMeshProUGUI levelNumText;
    public TextMeshProUGUI minNumMovesText;
    public TextMeshProUGUI numStarsText;


    [Header("Stars Settings")]
    [SerializeField]
    private Image[] stars;
    [SerializeField]
    private float starAnimationTime, starAnimationDelay = 0.5f; 
    [SerializeField]
    private AnimationCurve easeCurve = Tween.EaseSpring;


    public void UpdateStats() {
        LevelSettings currLevel = LevelInfo.Instance.currLevel;

        worldNameText.SetText(LevelInfo.Instance.currWorld.worldName);
        levelNumText.SetText("Level " + (currLevel.levelIndex + 1));
        minNumMovesText.SetText(currLevel.minTurns.ToString());
        // numStarsText.SetText(currLevel.maxStars.ToString());
        UpdateStars(currLevel.maxStars);
    }

    public void LoadLevel() {
        Debug.Log("Loading level: " + LevelInfo.Instance.currLevel.sceneName);
        StartCoroutine(LoadSceneAsync(LevelInfo.Instance.currLevel.sceneName));
    }

    public void UpdateStars(int numStars) {
        ClearStars();
        for(int i = 0; i < numStars; i++) {
            stars[i].gameObject.SetActive(true);
            Tween.LocalScale(stars[i].rectTransform, Vector3.zero, Vector3.one, starAnimationTime, starAnimationDelay, easeCurve, Tween.LoopType.None, null, null, false);
        }
    }

    public void ClearStars() {
        for(int i = 0; i < stars.Length; i++) {
            stars[i].gameObject.SetActive(false);
        }
    }

    IEnumerator LoadSceneAsync(string sceneToLoad){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
