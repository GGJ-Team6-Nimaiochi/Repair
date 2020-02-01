using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] public Text text;
    [SerializeField] RectTransform limImageRect = null;

    private int arrayNo;
    private Action endAction;

    public void SetData(int arrayNo,string text,Action endAction)
    {
        this.arrayNo = arrayNo;
        this.text.text = text;
        this.endAction = endAction;

        limImageRect.anchoredPosition = Vector2.right * ((text.Length * this.text.fontSize) + 275.0f);
    }

    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);

        var dragObj = data.pointerDrag.GetComponent<DragManage>();
        if (dragObj != null)
        {
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");
            text.text += dragObj.text.text;
            SelectStoryData.Instance.SetID(arrayNo, dragObj.pageContentData.id);
            dragObj.DestroyDragObject();
            limImageRect.gameObject.SetActive(false);
            Destroy(dragObj.gameObject);
            endAction();
            this.enabled = false;
        }
    }
}
