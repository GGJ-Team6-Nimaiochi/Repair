using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    //オブジェクトを一定間隔に生成する人
    //使うときは空のクラスを定義して明示的に生成するオブジェクトの型を指定してください
    public class RegularIntervalObjectGenerator<T> : ObjectPool<T>
        where T : PoolMonoBehaviour
    {
        [SerializeField]
        protected Vector3 objectDistance = Vector3.zero;
        public Vector3 ObjectDistance { get { return objectDistance; } }
        [SerializeField]
        protected int startGenerateNum = 10;
        public int StartGenerateNum { get { return startGenerateNum; } }
        protected Vector3 startPosition;
        protected int totalObjectCount;
        public int TotalObjectCount { get { return totalObjectCount; } }

        protected override void Awake()
        {
            base.Awake();
            startPosition = transform.position;
        }

        protected virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            for (int i = 0; i < ActiveInstanceList.Count; i++)
            {
                ReturnInstance(ActiveInstanceList[i]);
            }

            ActiveInstanceList.Clear();
            totalObjectCount = 0;

            for (int i = 0; i < startGenerateNum; i++)
            {
                GenerateAtLast();
            }
        }

        /// <summary>
        /// 後尾に追加
        /// </summary>
        public virtual T GenerateAtLast()
        {
            T obj = GetInstance();

            obj.transform.position = startPosition + objectDistance * totalObjectCount;

            totalObjectCount++;
            return obj;
        }

        /// <summary>
        /// 先頭に追加
        /// </summary>
        public virtual T GenerateAtFirst()
        {
            T obj = GetInstance();

            //座標
            Vector3 generatePosition = startPosition;

            if (ActiveInstanceList.Count > 0)
            {
                generatePosition.z = ActiveInstanceList[0].transform.position.z - ObjectDistance.z;
            }

            obj.transform.position = generatePosition;

            //登録
            ActiveInstanceList.Remove(obj);
            ActiveInstanceList.Insert(0, obj);

            return obj;
        }
    }
}