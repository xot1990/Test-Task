using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class EventBus 
{
    public static Action<ManagerScene.Scenes> startLoading;
    public static Action finishLoading;

    public static void StartLoading(ManagerScene.Scenes scenes)
    {
        startLoading?.Invoke(scenes);
    }

    public static void FinishLoading()
    {
        finishLoading?.Invoke();
    }
}
