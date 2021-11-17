using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using TMPro;

public class WorldBoxController : MonoBehaviour
{

    public WorldSettings worldSettings;
    

    public Transform cameraTopViewTransform;
    public Transform cameraInnerViewTransform;
    public Transform boxGeo;
    public Vector3 unfocusedScale;
    public Canvas LevelUICanvas;
    public Canvas WorldUICanvas;
    public Animator boxAnimator;

    public TextMeshProUGUI worldNameText;

    private Vector3 standardScale;
    private Vector3 standardRotation;
    private LevelSelectController selectionController;
    void Start()
    {
        selectionController = GetComponentInChildren<LevelSelectController>();
        standardScale = boxGeo.localScale;
        standardRotation = boxGeo.localEulerAngles;
        boxGeo.localScale = unfocusedScale;
        worldNameText.SetText(worldSettings.worldName);
    }

    public void BoxGainFocus()
    {
        Tween.LocalScale(boxGeo, standardScale, 0.75f, 0, Tween.EaseSpring);
    }

    public void BoxLoseFocus()
    {
        Tween.LocalScale(boxGeo, unfocusedScale, 0.75f, 0, Tween.EaseSpring);
    }

    public void Open()
    {
        StartCoroutine(EnabledUIOnDelay(1.1f, LevelUICanvas));
        selectionController.SelectWorld();
        WorldUICanvas.gameObject.SetActive(false);
        Tween.LocalRotation(boxGeo, Vector3.zero, 0.75f, 0, Tween.EaseInOut);
        boxAnimator.SetBool("isOpen", true);
    }

    public void Close()
    {
        StartCoroutine(EnabledUIOnDelay(1.1f, WorldUICanvas));
        selectionController.DeselectWorld();

        LevelUICanvas.gameObject.SetActive(false);
        Tween.LocalRotation(boxGeo, standardRotation, 0.75f, 0, Tween.EaseInOut);
        boxAnimator.SetBool("isOpen", false);
    }

    IEnumerator EnabledUIOnDelay(float time, Canvas ui)
    {
        yield return new WaitForSeconds(time);
        ui.gameObject.SetActive(true);
    }
}
