using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KanekoUtilities
{
    public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static readonly string findTag = TagName.GameController;

        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                GameObject[] objs = GameObject.FindGameObjectsWithTag(findTag);

                for (int i = 0; i < objs.Length; i++)
                {
                    instance = objs[i].GetComponent<T>();
                    if (instance != null) return instance;
                }

                if (instance == null)
                {
                    T prefab = Resources.Load<T>("SingletonMonobehaviour/" + typeof(T).Name);

                    if (prefab == null)
                    {
                        Debug.LogWarning(typeof(T).Name + "のPrefabがResourcesに存在しない \n" +
                            "もしくはTagがGameControllerに設定されていません");
                    }
                    else
                    {
                        instance = Instantiate(prefab);

                    }
                }
                return instance;
            }
        }

        [SerializeField]
        bool dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            if (Instance == this) return;

            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(this);
            }
        }
        protected virtual void Start()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
}