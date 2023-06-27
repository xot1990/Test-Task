using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class LoadingBarControler : MonoBehaviour
{
    [SerializeField] private Image BackBar;
    [SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        yield return new WaitForSeconds(1);

        while(true)
        {
            yield return new WaitForEndOfFrame();
            BackBar.fillAmount += Time.deltaTime / 2;

            if (BackBar.fillAmount >= 1)
            {
                canvasGroup.alpha -= Time.deltaTime*2;
                if (canvasGroup.alpha <= 0)
                {
                    EventBus.FinishLoading();
                    yield break;
                }
            }
        }
    }
}
