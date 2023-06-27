using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public sealed class GuiPointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public event Action<PointerEventData> OnDown;
    public event Action<PointerEventData> OnUp;
    public event Action<PointerEventData> OnClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDown?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp?.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(eventData);
    }

}
