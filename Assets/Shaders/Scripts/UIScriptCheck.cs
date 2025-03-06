using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIScriptCheck : UIBehaviour
{
    [SerializeField] private UnityEvent NotifyScreenSizeChange;

    protected override void OnRectTransformDimensionsChange()
    {
        NotifyScreenSizeChange.Invoke();
    }
}
