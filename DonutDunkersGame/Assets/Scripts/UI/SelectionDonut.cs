using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

[SelectionBase]
public class SelectionDonut : MonoBehaviour {

    Material[] materials;

    public float baseWidth, highlightWidth, selectedWidth;
    public int levelIndex;
    public bool isSelected;

    private LevelSelectController selectionController;
    void Awake() {
        materials = GetComponentInChildren<Renderer>().materials;
        selectionController = GetComponentInParent<LevelSelectController>();
    }

    public void Initialize(int index) {
        levelIndex = index;
        isSelected = false;
        SetOutlineWidth(baseWidth);
    }

    private void Update() {
        // SetOutlineWidth(baseWidth);
    }

    void OnMouseEnter() {
        if (isSelected) return;
        SetOutlineWidth(highlightWidth);
    }

    void OnMouseDown() {
        if (isSelected) return;
            
        OnSelected();
        
    }

    void OnMouseExit() {
        if (isSelected) return;
        SetOutlineWidth(baseWidth); 
    }

    public void OnSelected() {
        if (selectionController.SelectLevel(levelIndex)) {
            isSelected = true;
            SetOutlineWidth(selectedWidth);
        }
    }

    public void ClearSelection() {
        isSelected = false;
        SetOutlineWidth(baseWidth);
    }

    

    // assumes outline is in second material slot
    void SetOutlineWidth(float width) {
        materials[1].SetFloat("_Amount", width);
    }
}
