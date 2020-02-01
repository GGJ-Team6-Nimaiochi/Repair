using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{


    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);

        var dragObj = data.pointerDrag.GetComponent<DragManage>();
        if (dragObj != null)
        {
            //dragObj.parentTransform = this.transform;
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");
        }
    }
}
