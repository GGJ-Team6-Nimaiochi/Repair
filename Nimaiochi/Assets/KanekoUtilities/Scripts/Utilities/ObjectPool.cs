using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    /// <summary>
    /// Monobehaviourを継承しないクラスにも使えるようにinterfaceで実装
    /// </summary>
    public interface IObjectPool<T>
    {
        T GetOriginal { get; }
        T GetInstance();
        void ReturnInstance(T instance);
    }

    public class ObjectPool<T> : MonoBehaviour, IObjectPool<T>
        where T : PoolMonoBehaviour
    {
        [SerializeField]
        int maxCount = 3;
        public int MaxCount { get { return maxCount; } set { maxCount = value; } }

        [SerializeField]
        float removeIntervalTime = 10.0f;
        [SerializeField]
        bool isAwakeInstance = false;

        //プレハブを入れなかったら子を使用する
        [SerializeField]
        protected T original = null;

        List<T> instanceList;
        public List<T> InstanceList
        {
            get
            {
                if (instanceList == null)
                {
                    instanceList = new List<T>();
                }
                return instanceList;
            }
        }
        List<T> activeInstanceList;
        public List<T> ActiveInstanceList
        {
            get
            {
                if (activeInstanceList == null)
                {
                    activeInstanceList = new List<T>();
                }
                return activeInstanceList;
            }
        }

        public List<T> deactiveInstanceList = new List<T>();

        protected virtual void Awake()
        {
            if (!isAwakeInstance) return;

            for (int i = 0; i < maxCount; i++)
            {
                GetInstance();
            }

            ReturnAllInstance();
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(RemoveChecker());
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator RemoveChecker()
        {
            WaitForSeconds wait = new WaitForSeconds(removeIntervalTime);
            while (true)
            {
                yield return wait;
                RemoveCheck();
            }
        }

        //MaxCountを超えてオブジェクトが生成されている場合は使用していないオブジェクトを破棄する
        void RemoveCheck()
        {
            CheckInstanceAvailable();
            T removeInstance;
            while (0 < deactiveInstanceList.Count)
            {
                if (InstanceList.Count <= maxCount) break;
                removeInstance = deactiveInstanceList[0];
                deactiveInstanceList.Remove(removeInstance);
                InstanceList.Remove(removeInstance);
                Destroy(removeInstance.gameObject);

            }
        }

        //インスタンスが利用可能かをチェックする
        void CheckInstanceAvailable()
        {
            for (int i = deactiveInstanceList.Count - 1; i >= 0; i--)
            {
                if (deactiveInstanceList[i] != null) continue;

                T removeInstance = deactiveInstanceList[i];
                deactiveInstanceList.Remove(removeInstance);
                InstanceList.Remove(removeInstance);
            }
        }

        public virtual T GetInstance()
        {
            CheckInstanceAvailable();
            T instance;
            if (deactiveInstanceList.Count >= 1)
            {
                instance = deactiveInstanceList[0];
                deactiveInstanceList.Remove(instance);
                ActiveInstanceList.Add(instance);
                instance.gameObject.SetActive(true);
                instance.Init();
                return instance;
            }

            //足りない場合は生成する
            instance = Instantiate(GetOriginal, transform);
            InstanceList.Add(instance);
            ActiveInstanceList.Add(instance);
            instance.gameObject.SetActive(true);
            instance.Init();
            return instance;
        }

        public virtual void ReturnInstance(T instance)
        {
            instance.transform.SetParent(transform);
            instance.gameObject.SetActive(false);
            deactiveInstanceList.Add(instance);
            ActiveInstanceList.Remove(instance);
        }

        /// <summary>
        /// すべてのインスタンスを返す（使うときは注意）
        /// </summary>
        public virtual void ReturnAllInstance()
        {
            for (int i = ActiveInstanceList.Count - 1; i >= 0; i--)
            {
                ReturnInstance(ActiveInstanceList[i]);
            }
        }

        public virtual T GetOriginal
        {
            get
            {
                if (original != null) return original;
                original = transform.GetChild(0).GetComponent<T>();

                return original;
            }
        }

        public virtual void SetOriginal(T original)
        {
            if (original.Equals(this.original)) return;
            this.original = original;

            for (int i = 0; i < InstanceList.Count; i++)
            {
                Destroy(InstanceList[i].gameObject);
            }

            InstanceList.Clear();
            ActiveInstanceList.Clear();
            deactiveInstanceList.Clear();
        }
    }

    public class ObjectPool : ObjectPool<PoolMonoBehaviour> { }
}
