using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pixelplacement;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI levelNumberText;
    [SerializeField]
    private TextMeshProUGUI moveCountText;



    [Header("Star Animation Settings")]
    [SerializeField]
    private Image[] stars;

    [SerializeField]
    private float starAnimationTime = 0.5f; 
    [SerializeField]
    private float delayBetweenStars = 0.15f;
    [SerializeField]
    private AnimationCurve easeCurve = Tween.EaseSpring;

    void SetTitleText(bool isOutOfTurns)
    {
        if(isOutOfTurns)
        {
            this.titleText.SetText("You Ran Out Of Moves!");
            
        }
        else
        {
            this.titleText.SetText("Nice Job!");
        }
    }
    
    void SetStars(int numStars) {
        for(int i = 0; i < numStars; i++) {
            stars[i].gameObject.SetActive(true);
            Tween.LocalScale(stars[i].rectTransform, Vector3.zero, Vector3.one, starAnimationTime, ((i + 1) * delayBetweenStars), easeCurve, Tween.LoopType.None, null, null, false);
        }
    }


    void SetNumMoves(int value) {
        this.moveCountText.SetText($"You took {value} moves");
    }

    void SetLevelNumber(int levelNum, bool didWin) {
        this.levelNumberText.SetText($"Level {levelNum}" + (didWin ? " Complete!" : " Failed"));
    }

    public void SetLevelEndUI(bool isOutOfTurns, int ringsCollected, int ringsTotal, int numMoves) {
        LevelSettings currLevel = LevelInfo.Instance.currLevel;

        SetTitleText(isOutOfTurns);
        SetLevelNumber(currLevel.levelIndex + 1, !isOutOfTurns);
        SetNumMoves(numMoves);
        SetStars(currLevel.GetNumStars(numMoves));
    }
}
