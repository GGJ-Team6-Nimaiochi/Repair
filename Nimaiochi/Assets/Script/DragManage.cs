using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MyStory.StoryRepair;

public class DragManage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public Text text;

    [HideInInspector] public PageContentData pageContentData;

    public Transform canvas;
    private GameObject dragObject;


    public void SetPageContentData(PageContentData pageContentData)
    {
        this.pageContentData = null;
        this.pageContentData = new PageContentData(pageContentData);
        this.text.text = pageContentData.text;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        CreateDragObject();
        dragObject.transform.position = data.position;
    }
    public void OnDrag(PointerEventData data)
    {
        dragObject.transform.position = data.position;
    }
    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag");
        gameObject.GetComponent<Image>().color = Vector4.one;
        DestroyDragObject();
    }

    public void DestroyDragObject()
    {
        Destroy(dragObject);
    }

    // ドラッグオブジェクト作成
    private void CreateDragObject()
    {
        dragObject = new GameObject("dragObject");
        dragObject.transform.SetParent(canvas);
        dragObject.transform.SetAsLastSibling();
        dragObject.transform.localScale = Vector3.one;

        // レイキャストがブロックされないように
        CanvasGroup canvasGroup = dragObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        Image draggingImage = dragObject.AddComponent<Image>();
        Image sourceImage = GetComponent<Image>();

        draggingImage.sprite = sourceImage.sprite;
        draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        draggingImage.color = sourceImage.color;
        draggingImage.material = sourceImage.material;

        gameObject.GetComponent<Image>().color = Vector4.one * 0.6f;
    }
}
