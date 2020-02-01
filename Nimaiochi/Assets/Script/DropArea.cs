using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] public Text text;

    private int arrayNo;
    private Action endAction;

    public void SetData(int arrayNo,string text,Action endAction)
    {
        this.arrayNo = arrayNo;
        this.text.text = text;
        this.endAction = endAction;
    }

    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);

        var dragObj = data.pointerDrag.GetComponent<DragManage>();
        if (dragObj != null)
        {
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");
            text.text += dragObj.text.text;
            SelectStoryData.Instance.SetData(arrayNo, text.text, dragObj.pageContentData.id);
            dragObj.DestroyDragObject();
            Destroy(dragObj.gameObject);
            endAction();
            this.enabled = false;
        }
    }
}
