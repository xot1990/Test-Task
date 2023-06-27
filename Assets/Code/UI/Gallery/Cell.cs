using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class Cell : MonoBehaviour
{
    public int id;
    public bool isActive;
    private Image image;
    private RectTransform rectTransform;

    [SerializeField] private Image progressBar;

   

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        
    }

    private IEnumerator SetIcon(UnityWebRequest unityWeb)
    {
        var asyncOperation = unityWeb.SendWebRequest();

        progressBar.color = new Color(progressBar.color.r, progressBar.color.g, progressBar.color.b, 1);

        while (true)
        {
            yield return new WaitForEndOfFrame();

            progressBar.fillAmount = asyncOperation.progress;

            if (asyncOperation.isDone)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(unityWeb);
                image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                progressBar.color = new Color(progressBar.color.r, progressBar.color.g, progressBar.color.b, 0);
                yield break;
            }
        }
    }

    public void OnLine(string url)
    {
        gameObject.SetActive(true);

        var www = UnityWebRequestTexture.GetTexture(url + id + ".jpg");
        StartCoroutine(SetIcon(www));
        isActive = true;        
    }

    public void OffLine()
    {
        image.sprite = null;
        isActive = false;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        gameObject.SetActive(false);
    }

    public Vector3 LeftMin()
    {
        Vector3[] V = new Vector3[4];

        rectTransform.GetWorldCorners(V);

        return V[0];
    }

    public Vector3 RightMax()
    {
        Vector3[] V = new Vector3[4];

        rectTransform.GetWorldCorners(V);

        return V[2];
    }

    public Vector2 GetRealSize()
    {
        Vector3[] V = new Vector3[4];

        rectTransform.GetWorldCorners(V);

        Vector2 F = V[0] - V[2];

        return new Vector2(Mathf.Abs(F.x), Mathf.Abs(F.y));
    }
}
