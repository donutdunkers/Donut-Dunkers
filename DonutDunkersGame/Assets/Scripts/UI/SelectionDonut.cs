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

    private LevelSelectManager selectionManager;
    void Awake() {
        materials = GetComponentInChildren<Renderer>().materials;
        selectionManager = GetComponentInParent<LevelSelectManager>();
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
        Debug.Log("Mouse Enter");
    }

    void OnMouseDown() {
        if (isSelected) return;
            
        OnSelected();
        SetOutlineWidth(selectedWidth);
    }

    void OnMouseExit() {
        if (isSelected) return;
        SetOutlineWidth(baseWidth); 
    }

    void OnSelected() {
        if (selectionManager.SelectLevel(levelIndex)) {
            isSelected = true;
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
