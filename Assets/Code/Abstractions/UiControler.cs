using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UiControler : MonoBehaviour
{
    private protected CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(ShowInterface());
        onAwake();        
    }

    private protected virtual void onAwake() { }

    private protected IEnumerator StartTransition(ManagerScene.Scenes scenes)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha -= Time.deltaTime * 2;

            if (canvasGroup.alpha <= 0)
            {
                EventBus.StartLoading(scenes);
            }
        }
    }

    private protected IEnumerator ShowInterface()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha += Time.deltaTime;

            if (canvasGroup.alpha >= 1)
                yield break;
        }
    }
}
