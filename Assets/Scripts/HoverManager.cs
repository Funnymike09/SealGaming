using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HoverManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    private void Start()
    {
        HideTip();
    }

    private void ShowTip(string tip, Vector2 Pos)
    {
        text.text = tip;
        tipWindow.sizeDelta = new Vector2(text.preferredWidth > 200 ? 200 : text.preferredWidth, text.preferredHeight);
        if (Pos.x + 200 >= Screen.width)
        {

        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(Pos.x - tipWindow.sizeDelta.x / 1, Pos.y);
        }
        else
        {
            tipWindow.gameObject.SetActive(true);
            tipWindow.transform.position = new Vector2(Pos.x + tipWindow.sizeDelta.x / 1, Pos.y);
        }
    }

    private void HideTip()
    {
        text.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
