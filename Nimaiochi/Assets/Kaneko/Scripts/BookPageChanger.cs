using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

[RequireComponent(typeof(BookRenderer))]
public class BookPageChanger : MonoBehaviour
{
    [System.Serializable]
    class PageTexture
    {
        public Texture2D LeftTexture;
        public Texture2D RightTexture;
    }

    [SerializeField]
    PageTexture[] pageTextures = null;

    [SerializeField]
    Transform pageTransform = null;

    [SerializeField]
    float swipeSpeed = 0.02f;

    BookRenderer bookRenderer = null;

    int maxPageIndex = 3;
    int currentPageIndex = 0;
    float currentRate;
    bool isAnimating;

    void Awake()
    {
        bookRenderer = GetComponent<BookRenderer>();
    }

    void Start()
    {
        Init();

        SwipeGetter.Instance.onTouchStart.AddListener((_) =>
        {
            currentRate = 0.0f;
        });

        SwipeGetter.Instance.onSwipe.AddListener((vec) =>
        {
            currentRate += vec.x * swipeSpeed * Time.deltaTime * 0.01f;
            currentRate = Mathf.Clamp01(currentRate);
            UpdateMovePage(currentRate);
        });

        SwipeGetter.Instance.onTouchEnd.AddListener((_) =>
        {
            if(currentRate < 0.2f)
                StartCoroutine(ChangePageAnimation(0.0f, currentPageIndex));
            else
                StartCoroutine(ChangePageAnimation(1.0f, currentPageIndex + 1));
        });
    }

    public void Init()
    {
        UpdateTexture(0);
    }

    IEnumerator ChangePageAnimation(float targetRate, int pageIndex)
    {
        isAnimating = true;
        var start = currentRate;
        //t = s / d;
        var duration =  Mathf.Abs(start - targetRate) / 1.0f;

        yield return KKUtilities.FloatLerp(duration, (t) =>
        {
            UpdateMovePage(Mathf.Lerp(start, targetRate, t));
        });

        currentPageIndex = pageIndex;
        UpdateTexture(currentPageIndex);
        UpdateMovePage(0.0f);

        isAnimating = false;
    }

    void UpdateMovePage(float rate)
    {
        pageTransform.SetRotationZ(Mathf.Lerp(-89.0f, 90.0f, rate));
        // 紙芝居アニメーションしてます
        if (StorySimulator.Chapter && rate > 0) StorySimulator.Chapter.transform.GetChild(0).localScale = new Vector3(1 - 1 * rate, 1 - 1 * rate, 1 - 1 * rate);
        if (StorySimulator.Chapter && rate > 0) StorySimulator.Chapter.transform.GetChild(0).transform.localPosition = new Vector3(0 - 10f * rate, StorySimulator.Chapter.transform.GetChild(0).transform.localPosition.y, StorySimulator.Chapter.transform.GetChild(0).transform.localPosition.z);
    }

    void UpdateTexture(int index)
    {
        var temp = Mathf.Min(index + 1, pageTextures.Length - 1);
        // 紙芝居を消す
        if (StorySimulator.Chapter && StorySimulator.Chapter.transform.GetChild(0).localRotation.x <= 0) Destroy(StorySimulator.Chapter);
        bookRenderer.SetTextrue(pageTextures[index].LeftTexture, pageTextures[index].RightTexture, pageTextures[temp].LeftTexture, pageTextures[temp].RightTexture);
    }

    int ClampPageIndex(int index)
    {
        return Mathf.Clamp(index, 0, maxPageIndex);
    }
}
