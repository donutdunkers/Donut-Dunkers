using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI collectionText;

    [SerializeField]
    private TextMeshProUGUI moveCountText;

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

    void SetDonutsCollected(int collected, int total)
    {
        this.collectionText.SetText($"You collected {collected} / {total} donuts");
    }

    void SetNumMoves(int value)
    {
        this.moveCountText.SetText($"You took {value} moves");
    }

    public void SetLevelEndUI(bool isOutOfTurns, int ringsCollected, int ringsTotal, int numMoves)
    {
        SetTitleText(isOutOfTurns);
        SetDonutsCollected(ringsCollected, ringsTotal);
        SetNumMoves(numMoves);
    }
}
