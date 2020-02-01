using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

[RequireComponent(typeof(CustomLine))]
public class LinePainter : MonoBehaviour
{
    [SerializeField]
    float maxLineLength = 10.0f;

    [SerializeField]
    float nearDistanceThreshold = 0.3f;

    public CustomLine Line { get; private set; }
    public float MaxLineLength { get { return maxLineLength; } }
    public bool IsDrawing { get; private set; }

    public event System.Action OnDrawStart;
    public event System.Action OnDrawEnd;

    Camera mainCamera;
    Vector3 cameraDistance;

    void Awake()
    {
        Line = GetComponent<CustomLine>();
        mainCamera = Camera.main;
        cameraDistance = Vector3.forward *  (transform.position.z - mainCamera.transform.position.z);
    }

    void Start()
    {
        SwipeGetter.Instance.onTouchStart.AddListener(OnTouchStart);
        SwipeGetter.Instance.onTouching.AddListener(OnTouching);
        SwipeGetter.Instance.onTouchEnd.AddListener(OnTouchEnd);
    }

    public void Init()
    {
        Line.RemoveAll();
    }

    void OnTouchStart(Vector2 touchPosition)
    {
        Line.RemoveAll();
        IsDrawing = true;
        OnDrawStart.SafeInvoke();
    }

    void OnTouching(Vector2 touchPosition)
    {
        if (!IsDrawing) return;
        Vector3 point = mainCamera.ScreenToWorldPoint(touchPosition.ToXYVector3() + cameraDistance);
        //ローカル座標に変換
        point = transform.InverseTransformPoint(point);

        //初回は追加して終わり
        if(Line.PointCount <= 0)
        {
            Line.AddLastPoint(point);
            return;
        }

        Vector3 current = point;
        Vector3 last = Line.LastPoint;

        //近かったら追加しない
        if (IsNearPoint(current, last)) return;
        
        Line.AddLastPoint(current, false);

        //長さ制限
        while (Line.Length > maxLineLength)
        {
            Line.RemoveFirstPoint(false);
        }

        Line.Refresh();
    }

    void OnTouchEnd(Vector2 touchPosition)
    {
        IsDrawing = false;
        OnDrawEnd.SafeInvoke();
    }

    bool IsNearPoint(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(point1, point2) < nearDistanceThreshold;
    }
}
