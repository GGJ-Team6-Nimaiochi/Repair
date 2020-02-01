using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

[RequireComponent(typeof(BookRenderer))]
public class BookPageChanger : MonoBehaviour
{
    [SerializeField]
    Texture2D[] pageTextures = null;

    [SerializeField]
    Transform pageTransform = null;

    [SerializeField]
    UGUIButton rightButton = null;

    BookRenderer bookRenderer = null;

    int maxPageIndex = 3;
    int currentPageIndex = 0;
    bool isAnimating;

    void Awake()
    {
        bookRenderer = GetComponent<BookRenderer>();
    }

    void Start()
    {
        rightButton.OnClickEvent.AddListener(() => NextPage());

        Init();
    }

    public void Init()
    {
        UpdateTexture(0);
    }

    public void ChangePage(int pageIndex)
    {
        if(currentPageIndex == pageIndex) return;
        if(isAnimating) return;

        StartCoroutine(ChangePageAnimation(pageIndex));
    }

    IEnumerator ChangePageAnimation(int next)
    {
        isAnimating = true;
        yield return KKUtilities.FloatLerp(1.0f, (t) =>
        {
            pageTransform.SetRotationZ(Mathf.Lerp(-89.0f, 0.0f, t));
        });

        yield return KKUtilities.FloatLerp(1.0f, (t) =>
        {
            pageTransform.SetRotationZ(Mathf.Lerp(0.0f, 90.0f, t));
        });

        currentPageIndex = next;
        pageTransform.SetRotationZ(-89.0f);
        UpdateTexture(currentPageIndex);
        isAnimating = false;
    }

    void UpdateTexture(int index)
    {
        var temp = Mathf.Min(index + 1, pageTextures.Length -1);

        bookRenderer.SetTextrue(pageTextures[index], pageTextures[index], pageTextures[temp], pageTextures[temp]);
    }

    void NextPage()
    {
        var index = ClampPageIndex(currentPageIndex + 1);
        ChangePage(index);
    }

    void BackPage()
    {
        var index = ClampPageIndex(currentPageIndex - 1);
        ChangePage(index);
    }

    int ClampPageIndex(int index)
    {
        return Mathf.Clamp(index, 0, maxPageIndex);
    }
}
