using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelDonutController : MonoBehaviour
{
    public MeshRenderer renderer;
    Material outlineShader;
    private void Start()
    {
        renderer.materials[1].EnableKeyword("_Amount");
    }

    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            renderer.materials[1].SetFloat("_Amount", 10);
            Debug.Log("Hovering");
        }
        {
            renderer.materials[1].SetFloat("_Amount", 0);
        }
    }
    public void FocusDonut()
    {
        outlineShader.SetFloat("_OutlineThickness", 10);
    }

    public void UnfocusDonut()
    {
        outlineShader.SetFloat("_OutlineThickness", 0);
    }
}
