using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public enum AdjustmentValueName
    {
        //todo:ここに調整要素の名前を定義していく
    }

    //難易度を調整してくれる人
    public class LevelAdjuster : SingletonMonobehaviour<LevelAdjuster>
    {
        [SerializeField]
        AdjustmentValueByGameTime[] adjustmentValuesByGameTime = null;

        [SerializeField]
        AdjustmentValueByTrigger[] adjustmentValuesByTrigger = null;

        [SerializeField]
        IntAdjustmentValueByScore[] intAdjustmentValuesByScore = null;

        [SerializeField]
        FloatAdjustmentValueByScore[] floatAdjustmentValuesByScore = null;

        Dictionary<AdjustmentValueName, IAdjustmentValue> AdjustmentValueDic = new Dictionary<AdjustmentValueName, IAdjustmentValue>();

        protected override void Awake()
        {
            base.Awake();

            AddDictionary(adjustmentValuesByGameTime);
            AddDictionary(adjustmentValuesByTrigger);
            AddDictionary(intAdjustmentValuesByScore);
            AddDictionary(floatAdjustmentValuesByScore);
        }

        void AddDictionary(IAdjustmentValue[] adjustmentValues)
        {
            for (int i = 0; i < adjustmentValues.Length; i++)
            {
                IAdjustmentValue value = adjustmentValues[i];
                AdjustmentValueDic[value.Name] = value;
            }
        }

        public void Init()
        {
            foreach (var key in AdjustmentValueDic.Keys)
            {
                AdjustmentValueDic[key].Init();
            }
        }

        public void StartAdjustment()
        {
            for (int i = 0; i < adjustmentValuesByGameTime.Length; i++)
            {
                adjustmentValuesByGameTime[i].Start();
            }
        }
        void Update()
        {
            for (int i = 0; i < adjustmentValuesByGameTime.Length; i++)
            {
                adjustmentValuesByGameTime[i].Update();
            }
        }

        public BaseAdjustmentValue<T> GetAdjustmentValue<T>(AdjustmentValueName name)
        {
            IAdjustmentValue value;

            if (!AdjustmentValueDic.TryGetValue(name, out value)) return null;

            return (BaseAdjustmentValue<T>)value;
        }

        public void SetScore(int totalScore)
        {
            for (int i = 0; i < intAdjustmentValuesByScore.Length; i++)
            {
                intAdjustmentValuesByScore[i].UpdateAdjustmentValue(totalScore);
            }
            for (int i = 0; i < floatAdjustmentValuesByScore.Length; i++)
            {
                floatAdjustmentValuesByScore[i].UpdateAdjustmentValue(totalScore);
            }
        }

        public float GetFloat(AdjustmentValueName name)
        {
            return GetValue<float>(name);
        }

        public int GetInt(AdjustmentValueName name)
        {
            return GetValue<int>(name);
        }

        T GetValue<T>(AdjustmentValueName name)
        {
            BaseAdjustmentValue<T> value = GetAdjustmentValue<T>(name);

            if (value == null) return default(T);
            else return value.CurrentValue;
        }
    }
}