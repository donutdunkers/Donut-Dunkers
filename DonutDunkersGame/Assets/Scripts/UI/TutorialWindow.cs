using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindow : MonoBehaviour {
	
    private static TutorialWindow _Instance;
    public static TutorialWindow Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<TutorialWindow>();
            }
            return _Instance;
        }
    }
	
	[SerializeField]
	private GameObject[] tutorialWindows;
	
	[SerializeField]
	private GameObject HUD;

    private void Start()
    {
        if(LevelInfo.Instance.currLevel.isLevelComplete)
        {
            DeactivateTutorial();
			HUD.SetActive(true);
        }
    }

    public bool IsTutorialActive() {
		bool flag = false;
		for (int i = 0; i < this.tutorialWindows.Length; i++) {
			flag = this.tutorialWindows[i].activeSelf;
			if (flag) {
				return true;
			}
		}
		return false;
	}

	public void DeactivateTutorial()
    {
		foreach(Transform child in this.transform)
        {
			child.gameObject.SetActive(false);
        }
		StartCoroutine(SendDelayedDisabled(1f));
    }

	IEnumerator SendDelayedDisabled(float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(false);
	}
}
