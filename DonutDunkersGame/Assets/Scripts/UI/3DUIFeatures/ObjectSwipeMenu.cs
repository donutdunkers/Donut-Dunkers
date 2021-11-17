using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class ObjectSwipeMenu : MonoBehaviour
{
    public Camera objectCamera;
    public WorldBoxController[] content;
    public float transitionTime = 1f;

    int totalObjects;
    int currentIndex;
    WorldBoxController currentContent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwipeRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwipeLeft();
        }
    }

    public void InitializeWorldSelect()
    {
        currentIndex = 0;
        totalObjects = content.Length - 1;
        currentContent = content[0];
        currentContent.BoxGainFocus();

        LerpCameraTransform(currentContent.cameraTopViewTransform);
    }

    public void SwipeRight()
    {
        if (currentIndex < totalObjects)
        {
            currentContent.BoxLoseFocus();
            currentIndex += 1;

            currentContent = content[currentIndex];
            currentContent.BoxGainFocus();
            LerpCameraTransform(currentContent.cameraTopViewTransform);
        }
    }

    public void SwipeLeft()
    {
        if (currentIndex > 0)
        {
            currentContent.BoxLoseFocus();
            currentIndex -= 1;

            currentContent = content[currentIndex];
            currentContent.BoxGainFocus();
            LerpCameraTransform(currentContent.cameraTopViewTransform);
        }
    }

    public void SelectContent()
    {
        for(int i = 0; i < content.Length; i++)
        {
            if (i != currentIndex)
            {
                content[i].gameObject.SetActive(false);
            }
        }
        currentContent.Open();
        LerpCameraTransform(currentContent.cameraInnerViewTransform);
    }

    public void ReturnFromContent()
    {
        for (int i = 0; i < content.Length; i++)
        {
            if (i != currentIndex)
            {
                content[i].gameObject.SetActive(true);
            }
        }
        currentContent.Close();
        LerpCameraTransform(currentContent.cameraTopViewTransform);
    }

    void LerpCameraTransform(Transform target)
    {
        Tween.Position(objectCamera.transform, target.position, transitionTime, 0, Tween.EaseInOut);
        Tween.Rotation(objectCamera.transform, target.rotation, transitionTime, 0, Tween.EaseInOut);
    }
}
