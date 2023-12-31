using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UiControler : MonoBehaviour
{
    private protected CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(ShowInterface());
        SetSceneOrientation();
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
                if (scenes == ManagerScene.Scenes.Quit)
                    Application.Quit();

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

    private protected virtual void SetSceneOrientation()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
