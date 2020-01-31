using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class WeightedTableElement<T>
    {
        public bool Ignore;
        public int Priority;
        public T Data;
    }

    [System.Serializable]
    public class IntWeightedTableElement : WeightedTableElement<int> { }

    [System.Serializable]
    public class GameObjectWeightedTableElement : WeightedTableElement<GameObject> { }

    public class WeightedTable<T>
    {
        WeightedTableElement<T>[] elements;

        int[] priorityArray;

        public WeightedTable(WeightedTableElement<T>[] elements)
        {
            this.elements = elements;
            priorityArray = new int[elements.Length];

            for(int i = 0 ; i < priorityArray.Length ; i++)
            {
                priorityArray[i] = elements[i].Priority;
            }
        }

        public void SetPriorityArray(int[] priorityArray)
        {
            if(this.priorityArray.Length != priorityArray.Length) return;
            this.priorityArray = priorityArray;
        }

        void UpdatePriority()
        {
            for(int i = 0 ; i < priorityArray.Length ; i++)
                priorityArray[i] = elements[i].Ignore ? 0 : elements[i].Priority;
        }

        public T GetData(UnityRandom rand)
        {
            UpdatePriority();
            var urand = RandomUtil.Urand;
            RandomUtil.SetUnityRandom(rand);
            int index = RandomUtil.GetUnityRandomIndexWithWeight(priorityArray);
            RandomUtil.SetUnityRandom(urand);

            return elements[index].Data;
        }

        public T GetData()
        {
            UpdatePriority();
            int index = RandomUtil.GetRandomIndexWithWeight(priorityArray);

            return elements[index].Data;
        }
    }
}