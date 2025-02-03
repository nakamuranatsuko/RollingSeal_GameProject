using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(CanvasGroup))]
public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 1f;

    private CanvasGroup canvasGroup;

    private static FadeManager instance;
    public static FadeManager Inctance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FadeManager>();

                if (instance == null)
                {
                    var obj = new GameObject("FadeManager");
                    instance = obj.AddComponent<FadeManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    public async UniTask FadeIn()
    {
        this.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        float alpha = 1;
        while (canvasGroup.alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Max(alpha, 0f);
            await UniTask.Yield();
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    public async UniTask FadeOut()
    {
        this.gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        float alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Min(alpha, 1f);
            await UniTask.Yield();
        }
    }
}
