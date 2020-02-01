using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    /// <summary>
    /// Monobehaviourを継承しないクラスにも使えるようにinterfaceで実装
    /// </summary>
    /// <typeparam name="T">poolするクラス</typeparam>
    public interface IPoolObject
    {
        void Init();
    }

    public class PoolMonoBehaviour : MonoBehaviour, IPoolObject
    {
        public virtual void Init() { }
    }
}