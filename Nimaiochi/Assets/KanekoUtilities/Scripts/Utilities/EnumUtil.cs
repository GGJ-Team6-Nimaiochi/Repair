using System;
using UnityEngine;

namespace KanekoUtilities
{
    public static class EnumUtil
    {
        /// <summary>
        /// 指定された列挙型の長さを返す
        /// </summary>
        public static int GetLength<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// 指定された列挙型のランダムな要素を返す
        /// </summary>
        public static T Random<T>()
        {
            Array array = Enum.GetValues(typeof(T));
            T[] temp = (T[])array;
            return temp[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}