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
}
