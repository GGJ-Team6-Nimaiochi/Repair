using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public static class VectorUtil
    {
        /// <summary>
        /// ２点間の(Y成分に限定した)角度を返す
        /// </summary>
        public static float GetAngleY(Vector3 vec1, Vector3 vec2)
        {
            Vector3 temp = vec2 - vec1;
            float vecY = temp.y;
            //X方向だけのベクトルに変換
            temp = Vector3.right * temp.magnitude;
            temp.y = vecY;

            return Vector3.Angle(Vector3.right, temp) * 2.0f;
        }

        /// <summary>
        /// 指定した角度の球体座標を返す
        /// </summary>
        /// <param name="longitude">経度</param>
        /// <param name="latitude">緯度</param>
        public static Vector3 SphereCoordinate(float longitude, float latitude, float distance)
        {
            Vector3 position = Vector3.zero;

            //重複した計算
            float temp1 = distance * Mathf.Cos(latitude * Mathf.Deg2Rad);
            float temp2 = longitude * Mathf.Deg2Rad;

            position.x = temp1 * Mathf.Sin(temp2);
            position.y = distance * Mathf.Sin(latitude * Mathf.Deg2Rad);
            position.z = temp1 * Mathf.Cos(temp2);

            return position;
        }

        /// <summary>
        /// 指定された角度の正規化されたベクトルを返す
        /// </summary>
        public static Vector2 AngleToVector2(float angle)
        {
            float rad = angle * Mathf.Deg2Rad;
            return (new Vector2(Mathf.Sin(rad), Mathf.Cos(rad))).normalized;
        }

        /// <summary>
        /// 指定された角度の正規化されたベクトルを返す
        /// </summary>
        public static Vector3 AngleToXZVector3(float angle)
        {
            float rad = angle * Mathf.Deg2Rad;
            return (new Vector3(Mathf.Sin(rad), 0.0f, Mathf.Cos(rad))).normalized;
        }
    }

    public static class Vector2Extentions
    {
        /// <summary>
        /// 指定されたベクトルをXZのVector3に変換する
        /// </summary>
        public static Vector3 ToXZVector3(this Vector2 vec)
        {
            return new Vector3(vec.x, 0.0f, vec.y);
        }

        /// <summary>
        /// 指定されたベクトルをXYのVector3に変換する
        /// </summary>
        public static Vector3 ToXYVector3(this Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0.0f);
        }
    }

    public static class Vector3Extentions
    {
        /// <summary>
        /// 指定されたベクトルのXZの角度を返す
        /// </summary>
        public static float GetAngleXZ(this Vector3 vec)
        {
            return Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;
        }
    }
}