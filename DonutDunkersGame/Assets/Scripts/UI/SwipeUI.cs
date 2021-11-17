using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipeMenu : MonoBehaviour
{
    public Scrollbar scrollbar;

    float scrollPosition = 0;
    float[] positions;

    void Update()
    {
        positions = new float[transform.childCount];
        float distance = 1f / (positions.Length - 1f);

        for(int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrollPosition = scrollbar.value;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && scrollPosition < 1f)
        {
            scrollPosition += distance;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && scrollPosition > 0f)
        {
            scrollPosition -= distance;
        }
        else
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if(scrollPosition < positions[i] + (distance / 2) && scrollPosition > positions[i] - (distance / 2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, positions[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if (scrollPosition < positions[i] + (distance / 2) && scrollPosition > positions[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(2f, 2f), 0.1f);
            }
            for (int j = 0; j < positions.Length; j++)
            {
                if (j != i)
                {
                    transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(1f, 1f), 0.1f);
                }
            }
        }
    }
}
