using System;
using System.Collections.Generic;
using UnityEngine;
using URandom;

namespace KanekoUtilities
{
    /// <summary>
    /// UnityRandomも使える（Seedが指定したい場合はSetSeedをしようする）
    /// </summary>
    public static class RandomUtil
    {
        static UnityRandom urand;
        public static UnityRandom Urand
        {
            get
            {
                if(urand != null) return urand;
                urand = new UnityRandom();
                return urand;
            }
        }

        public static void SetSeed(int seed)
        {
            urand = new UnityRandom(seed);
        }

        public static void SetUnityRandom(UnityRandom _urand)
        {
            urand = _urand;
        }

        /// <summary>
        /// 指定された確率でtrueを返す
        /// </summary>
        public static bool GetRandomBool(int probability)
        {
            return UnityEngine.Random.Range(0, 100) < probability;
        }

        /// <summary>
        /// 指定された確率でtrueを返す
        /// </summary>
        public static bool GetUnityRandomBool(int probability)
        {
            return RandomUtil.Range(0, 100) < probability;
        }

        /// <summary>
        /// 渡された配列の中からランダムで1つを返す
        /// </summary>
        public static T GetRandomValue<T>(params T[] values)
        {
            return values[UnityEngine.Random.Range(0, values.Length)];

        }
        /// <summary>
        /// 渡された配列の中からランダムで1つを返す
        /// </summary>
        public static T GetUnityRandomValue<T>(params T[] values)
        {
            return values[RandomUtil.Range(0, values.Length)];
        }

        /// <summary>
        /// 配列の要素の重みを考慮して要素のインデックスを返す
        /// </summary>
        public static int GetRandomIndexWithWeight(params int[] weightArray)
        {
            int totalWeight = 0;
            for(int i = 0 ; i < weightArray.Length ; i++)
            {
                totalWeight += weightArray[i];
            }

            int randomValue = UnityEngine.Random.Range(1, totalWeight + 1);
            int index = -1;
            for(var i = 0 ; i < weightArray.Length ; ++i)
            {
                if(weightArray[i] >= randomValue)
                {
                    index = i;
                    break;
                }
                randomValue -= weightArray[i];
            }
            return index;
        }

        /// <summary>
        /// 配列の要素の重みを考慮して要素のインデックスを返す
        /// </summary>
        public static int GetUnityRandomIndexWithWeight(params int[] weightArray)
        {
            int totalWeight = 0;
            for(int i = 0 ; i < weightArray.Length ; i++)
            {
                totalWeight += weightArray[i];
            }

            int randomValue = RandomUtil.Range(1, totalWeight + 1);
            int index = -1;
            for(var i = 0 ; i < weightArray.Length ; ++i)
            {
                if(weightArray[i] >= randomValue)
                {
                    index = i;
                    break;
                }
                randomValue -= weightArray[i];
            }
            return index;
        }



        /// <summary>
        /// 0 - 1
        /// </summary>
        public static float Value()
        {
            return Urand.Value();
        }

        /// <summary>
        /// 0 - 1
        /// </summary>
        public static float Value(UnityRandom.Normalization n, float t)
        {
            return Urand.Value(n, t);
        }

        /// <summary>
        /// min < x < max
        /// </summary>
        public static int Range(int minValue, int maxValue)
        {
            if(minValue == maxValue) return minValue;

            if(minValue > maxValue)
            {
                var temp = minValue;
                minValue = maxValue;
                maxValue = temp;
            }

            maxValue--;

            if(minValue == maxValue) return minValue;

            return Urand.Range(minValue, maxValue);
        }

        /// <summary>
        /// min < x < max
        /// </summary>
        public static float Range(int minValue, int maxValue, UnityRandom.Normalization n, float t)
        {
            if(minValue == maxValue) return minValue;

            if(minValue > maxValue)
            {
                var temp = minValue;
                minValue = maxValue;
                maxValue = temp;
            }

            maxValue--;

            if(minValue == maxValue) return minValue;

            return Urand.Range(minValue, maxValue, n, t);
        }

        /// <summary>
        /// ポアソン分布
        /// </summary>
        public static float Possion(float lambda)
        {
            return Urand.Possion(lambda);
        }

        /// <summary>
        /// 指数分布
        /// </summary>
        public static float Exponential(float lambda)
        {
            return Urand.Exponential(lambda);
        }

        /// <summary>
        /// ガンマ分布
        /// </summary>
        public static float Gamma(float order)
        {
            return Urand.Gamma(order);
        }

        /// <summary>
        /// L = 1 の正方形の中のランダムな点を返す
        /// </summary>
        public static Vector2 PointInASquare()
        {
            return Urand.PointInASquare();
        }

        /// <summary>
        /// L = 1 の正方形の中のランダムな点を返す
        /// </summary>
        public static Vector2 PointInASquare(UnityRandom.Normalization n, float t)
        {
            return Urand.PointInASquare(n, t);
        }

        /// <summary>
        /// R = 1 の円の中のランダムな点を返す
        /// </summary>
        public static Vector2 PointInACircle()
        {
            return Urand.PointInACircle();
        }

        /// <summary>
        /// R = 1 の円の中のランダムな点を返す
        /// </summary>
        public static Vector2 PointInACircle(UnityRandom.Normalization n, float t)
        {
            return Urand.PointInACircle(n, t);
        }

        /// <summary>
        /// R = 1 のディスクの中のランダムな点を返す
        /// </summary>
        public static Vector2 PointInADisk()
        {
            return Urand.PointInADisk();
        }

        /// <summary>
        /// R = 1 のディスクの中のランダムな点を返す
        /// </summary>
        public static Vector2 PointInADisk(UnityRandom.Normalization n, float t)
        {
            return Urand.PointInADisk(n, t);
        }

        /// <summary>
        /// L = 1 の立方体の中のランダムな点を返す
        /// </summary>
        public static Vector3 PointInACube()
        {
            return Urand.PointInACube();
        }

        /// <summary>
        /// L = 1 の立方体の中のランダムな点を返す
        /// </summary>
        public static Vector3 PointInACube(UnityRandom.Normalization n, float t)
        {
            return Urand.PointInACube(n, t);
        }

        /// <summary>
        /// L = 1 の立方体の表面のランダムな点を返す
        /// </summary>
        public static Vector3 PointOnACube()
        {
            return Urand.PointOnACube();
        }

        /// <summary>
        /// L = 1 の立方体の表面のランダムな点を返す
        /// </summary>
        public static Vector3 PointOnACube(UnityRandom.Normalization n, float t)
        {
            return Urand.PointOnACube(n, t);
        }

        /// <summary>
        /// R = 1 の球体の表面のランダムな点を返す
        /// </summary>
        public static Vector3 PointOnASphere()
        {
            return Urand.PointOnASphere();
        }

        /// <summary>
        /// R = 1 の球体の表面のランダムな点を返す
        /// </summary>
        public static Vector3 PointOnASphere(UnityRandom.Normalization n, float t)
        {
            return Urand.PointOnASphere(n, t);
        }

        /// <summary>
        /// R = 1 の球体の中のランダムな点を返す
        /// </summary>
        public static Vector3 PointInASphere()
        {
            return Urand.PointInASphere();
        }

        /// <summary>
        /// R = 1 の球体の中のランダムな点を返す
        /// </summary>
        public static Vector3 PointInASphere(UnityRandom.Normalization n, float t)
        {
            return Urand.PointInASphere(n, t);
        }

        /// <summary>
        /// 指定された角度のコーンの表面のランダムな点を返す(多分)
        /// </summary>
        public static Vector3 PointOnCone(float spotAngle)
        {
            return Urand.PointOnCap(spotAngle);
        }

        /// <summary>
        /// 指定された角度のコーンの表面のランダムな点を返す(多分)
        /// </summary>
        public static Vector3 PointOnCone(float spotAngle, UnityRandom.Normalization n, float t)
        {
            return Urand.PointOnCap(spotAngle, n, t);
        }

        /// <summary>
        /// 扇の表面のランダムな点を返す(多分)
        /// </summary>
        public static Vector3 PointOnRing(float innerAngle, float outerAngle)
        {
            return Urand.PointOnRing(innerAngle, outerAngle);
        }

        /// <summary>
        /// 扇の表面のランダムな点を返す(多分)
        /// </summary>
        public static Vector3 PointOnRing(float innerAngle, float outerAngle, UnityRandom.Normalization n, float t)
        {
            return Urand.PointOnRing(innerAngle, outerAngle, n, t);
        }

        /// <summary>
        /// ランダムな色を返す
        /// </summary>
        public static Color Rainbow()
        {
            return Urand.Rainbow();
        }

        /// <summary>
        /// ランダムな色を返す
        /// </summary>
        public static Color Rainbow(UnityRandom.Normalization n, float t)
        {
            return Urand.Rainbow(n, t);
        }

        /// <summary>
        /// ダイスロール
        /// </summary>
        /// <param name="size">個数</param>
        public static DiceRoll RollDice(int size, DiceRoll.DiceType type)
        {
            return Urand.RollDice(size, type);
        }

        /// <summary>
        /// 配列をシャッフルして返す
        /// </summary>
        public static ShuffleBagCollection<float> ShuffleBag(float[] values)
        {
            return Urand.ShuffleBag(values);
        }

        /// <summary>
        /// 配列をウェイト付きシャッフルして返す。
        /// </summary>
        public static ShuffleBagCollection<float> ShuffleBag(Dictionary<float, int> dict)
        {
            return Urand.ShuffleBag(dict);
        }
    }
}
