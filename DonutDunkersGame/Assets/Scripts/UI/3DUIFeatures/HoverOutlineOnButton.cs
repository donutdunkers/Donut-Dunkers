using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOutlineOnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public LevelDonutController donutController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        donutController.FocusDonut();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        donutController.UnfocusDonut();
    }
}
