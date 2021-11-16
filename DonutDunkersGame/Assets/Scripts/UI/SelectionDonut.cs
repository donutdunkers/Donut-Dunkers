using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

[SelectionBase]
public class SelectionDonut : MonoBehaviour {

    Material[] materials;

    public float baseWidth, highlightWidth, selectedWidth;
    void Awake() {
        materials = GetComponentInChildren<Renderer>().materials;

    }

    private void Update() {
        // SetOutlineWidth(baseWidth);
    }

    void OnMouseEnter() {
        SetOutlineWidth(highlightWidth);
        Debug.Log("Mouse Enter");
    }

    void OnMouseExit() {
        SetOutlineWidth(baseWidth);
        Debug.Log("Mouse Exit");
    }

    // assumes outline is in second material slot
    void SetOutlineWidth(float width) {
        materials[1].SetFloat("_Amount", width);
    }
}
