using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

/// <summary>
/// LineRendererに点の追加、削除を付けたラッパークラス
/// </summary>
/// 
/// 用語説明
///  先頭：一番最初に追加された要素(indexが0番目の要素)
///  末尾：一番最後に追加された要素(indexがlist.Count - 1番目の要素)
[RequireComponent(typeof(LineRenderer))]
public class CustomLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<Vector3> positionList = new List<Vector3>();

    /// <summary>
    /// 先頭に点が追加された時に呼ばれる
    /// </summary>
    public System.Action<Vector3, Vector3> OnAddFirstPoint;

    /// <summary>
    /// 末尾に点が追加された時に呼ばれる
    /// </summary>
    public System.Action<Vector3, Vector3> OnAddLastPoint;

    /// <summary>
    /// 先頭の点
    /// </summary>
    public Vector3 FirstPoint { get { return GetPoint(0, false); } }
    /// <summary>
    /// 末尾の点
    /// </summary>
    public Vector3 LastPoint { get { return GetPoint(PointCount - 1, false); } }
    /// <summary>
    /// 点の数
    /// </summary>
    public int PointCount { get { return positionList.Count; } }

    /// <summary>
    /// 色を設定する(startとendを別で指定したい場合はSetColorを使う)
    /// </summary>
    public Color Color { get { return lineRenderer.startColor; } set { SetColor(value, value); } }
    /// <summary>
    /// 透明度を設定する(startとendを別で指定したい場合はSetAlphaを使う)
    /// </summary>
    public float Alpha { get { return Color.a; } set { SetAlpha(value, value); } }
    /// <summary>
    /// 幅を設定する(startとendを別で指定したい場合はSetWidthを使う)
    /// </summary>
    public float Width { get { return lineRenderer.startWidth; } set { SetWidth(value, value); } }
    /// <summary>
    /// 全ての点を経由した線の長さの合計
    /// </summary>
    public float Length
    {
        get
        {
            float length = 0.0f;

            for (int i = 1; i < PointCount; i++)
            {
                length += Vector3.Distance(positionList[i], positionList[i - 1]);
            }

            return length;
        }
    }

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 先頭に点を追加する
    /// </summary>
    /// <param name="point">点</param>
    /// <param name="isRefresh">Viewを更新するか</param>
    /// <param name="callEvent">点を追加したイベントを呼ぶか</param>
    public void AddFirstPoint(Vector3 point, bool isRefresh = true, bool callEvent = true)
    {
        //イベントを呼ぶよりも先に点の追加を行いたいので一時変数を使う
        Vector3 temp = FirstPoint;

        positionList.Insert(0, point);

        if (callEvent && PointCount > 0) OnAddFirstPoint.SafeInvoke(temp, point);

        if (isRefresh) Refresh();
    }
    /// <summary>
    /// 末尾に点を追加する
    /// </summary>
    /// <param name="point">点</param>
    /// <param name="isRefresh">Viewを更新するか</param>
    /// <param name="callEvent">点を追加したイベントを呼ぶか</param>
    public void AddLastPoint(Vector3 point, bool isRefresh = true, bool callEvent = true)
    {
        //イベントを呼ぶよりも先に点の追加を行いたいので一時変数を使う
        Vector3 temp = LastPoint;

        positionList.Add(point);

        if (callEvent && PointCount > 0) OnAddLastPoint.SafeInvoke(temp, point);

        if (isRefresh) Refresh();
    }

    /// <summary>
    /// 先頭の点を削除する
    /// </summary>
    /// <param name="isRefresh">Viewを更新するか</param>
    public void RemoveFirstPoint(bool isRefresh = true)
    {
        RemovePoint(0, isRefresh);
    }
    /// <summary>
    /// 末尾の点を削除する
    /// </summary>
    /// <param name="isRefresh">Viewを更新するか</param>
    public void RemoveLastPoint(bool isRefresh = true)
    {
        RemovePoint(PointCount - 1, isRefresh);
    }
    /// <summary>
    /// 指定されたindexの点を削除する
    /// </summary>
    /// <param name="isRefresh">Viewを更新するか</param>
    public void RemovePoint(int index, bool isRefresh = true)
    {
        //範囲外
        if (!IsAvailableIndex(index))
        {
            return;
        }

        positionList.RemoveAt(index);
        if (isRefresh) Refresh();
    }

    /// <summary>
    /// 点をすべて削除する
    /// </summary>
    /// <param name="isRefresh">Viewを更新するか</param>
    public void RemoveAll(bool isRefresh = true)
    {
        positionList.Clear();
        if (isRefresh) Refresh();
    }

    /// <summary>
    /// 指定されたindexの点を移動させる
    /// </summary>
    /// <param name="isRefresh">Viewを更新するか</param>
    public void ChangePoint(int index, Vector3 point, bool isRefresh = true)
    {
        //範囲外
        if (!IsAvailableIndex(index)) return;

        positionList[index] = point;

        if (isRefresh) Refresh();
    }

    /// <summary>
    /// 更新(点が増減または変更された場合に呼ぶ)
    /// </summary>
    public void Refresh()
    {
        lineRenderer.SetVertexCount(positionList.Count);
        lineRenderer.SetPositions(positionList.ToArray());
    }

    /// <summary>
    /// 指定されたindexの点を返す
    /// </summary>
    /// <param name="index"></param>
    public Vector3 GetPoint(int index, bool isAlert = true)
    {
        //範囲外
        if (!IsAvailableIndex(index, isAlert)) return Vector3.zero;

        return positionList[index];
    }

    /// <summary>
    /// すべての点を取得する
    /// </summary>
    public Vector3[] GetPoints()
    {
        return positionList.ToArray();
    }

    /// <summary>
    /// 色を設定する(startとendが同じで良ければColorを使う)
    /// </summary>
    public void SetColor(Color startColor, Color endColor)
    {
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
    }
    /// <summary>
    /// 色を設定する
    /// </summary>
    public void SetColor(Gradient gradient)
    {
        lineRenderer.colorGradient = gradient;
    }
    /// <summary>
    /// 透明度を設定する(startとendが同じで良ければAlphaを使う)
    /// </summary>
    public void SetAlpha(float startAlpha, float endAlpha)
    {
        Color temp1 = lineRenderer.startColor;
        Color temp2 = lineRenderer.endColor;

        temp1.a = startAlpha;
        temp2.a = endAlpha;
        SetColor(temp1, temp2);
    }
    /// <summary>
    /// 幅を設定する(startとendが同じで良ければWidthを使う)
    /// </summary>
    public void SetWidth(float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
    }

    /// <summary>
    /// 指定されたindexのpointが存在するかを返す
    /// </summary>
    public bool IsAvailableIndex(int index, bool isAlert = true)
    {
        bool isAvailable = index >= 0 && index < PointCount;

        if (isAlert && !isAvailable) Debug.LogWarning("out of range of line :" + name + " index = " + index);

        return isAvailable;
    }
}
