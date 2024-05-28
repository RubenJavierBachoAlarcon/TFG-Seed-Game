using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void nextPage()
    {
        if (text.pageToDisplay < text.textInfo.pageCount)
        {
            text.pageToDisplay++;
        }
    }

    public void previousPage()
    {
        if (text.pageToDisplay > 1)
        {
            text.pageToDisplay--;
        }
    }
}
