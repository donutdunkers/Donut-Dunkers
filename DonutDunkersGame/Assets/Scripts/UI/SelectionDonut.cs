using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

[SelectionBase]
public class SelectionDonut : MonoBehaviour {

    Material[] materials;

    public float baseWidth, highlightWidth, selectedWidth;
    public int levelId;
    public bool isSelected;
    void Awake() {
        materials = GetComponentInChildren<Renderer>().materials;

    }

    public void Initialize(int id) {
        levelId = id;
        isSelected = false;
        SetOutlineWidth(baseWidth);
    }

    private void Update() {
        // SetOutlineWidth(baseWidth);
    }

    void OnMouseEnter() {
        SetOutlineWidth(highlightWidth);
        Debug.Log("Mouse Enter");
    }

    void OnMouseDown() {
        OnSelected();
        SetOutlineWidth(selectedWidth);
    }

    void OnMouseExit() {
        if (!isSelected) {
            SetOutlineWidth(baseWidth); 
        }
       
        Debug.Log("Mouse Exit");
    }

    void OnSelected() {
        LevelSelectManager.Instance.SelectLevel(levelId);
    }

    

    // assumes outline is in second material slot
    void SetOutlineWidth(float width) {
        materials[1].SetFloat("_Amount", width);
    }
}
