using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] public Text text;

    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);

        var dragObj = data.pointerDrag.GetComponent<PageContent>();
        var baseDragObj = data.pointerDrag.GetComponent<DragManage>();
        if (dragObj != null && baseDragObj != null)
        {
            //dragObj.parentTransform = this.transform;
            Debug.Log(gameObject.name + "に" + data.pointerDrag.name + "をドロップ");

            baseDragObj.DestroyDragObject();
            Destroy(dragObj.gameObject);
        }
    }
}
