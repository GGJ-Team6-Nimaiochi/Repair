using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum ItemType { VirtualCoin, Skin }

    public interface IGameItem
    {
        ItemType Type { get; }
        string ID { get; }
        int Count { get; }

        void Earn(int count);
        bool Spend(int count);
    }

    public abstract class BaseVirtualCoin : IGameItem
    {
        public ItemType Type { get { return ItemType.VirtualCoin; } }
        public abstract string ID { get; }
        int count = 0;
        public virtual int Count { get { return count; } }

        public BaseVirtualCoin(int count)
        {
            this.count = count;
        }

        public virtual void Earn(int value)
        {
            count += value;
        }

        public virtual bool Spend(int value)
        {
            if (value > count) return false;

            return true;
        }
    }

    public class MyUserDataManager : Singleton<MyUserDataManager>
    {

    }
}