using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipText;
    public SealInfo sealInfo;
    private float WaitTime = 0.5f;

    private void Awake()
    {
        if (sealInfo.Injured)
        {
            tipText += "Injured<br>";
                
        }
        if (sealInfo.Malnourished)
        {
            tipText += "Malnourished<br>";
        }
        if (sealInfo.Sick)
        {
            tipText += "Sick<br>";
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartT());

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverManager.OnMouseLoseFocus();
    }

    public void Show()
    {
 
        HoverManager.OnMouseHover(tipText, Input.mousePosition);
    }
    public void ShowOver()
    {
        HoverManager.OnMouseLoseFocus();
    }

    private IEnumerator StartT()
    {
        yield return new WaitForSeconds(WaitTime);
        Show();
    }
}
