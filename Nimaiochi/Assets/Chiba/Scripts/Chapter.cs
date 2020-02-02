using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class Chapter : MonoBehaviour
{
    public Sprite[] HouseSprites;
    public SpriteRenderer[] houses;

    // 各子豚の家の画像を変える
    private void Awake()
    {
        if (houses.Length <= SelectStoryData.Instance.id.Length)
        {
            for (int i = 0; i < houses.Length; i++)
            {
                if(houses[i] == null || SelectStoryData.Instance.id[i] == -1) continue;
                StorySimulator.Instance.SelectHouses[i] = HouseSprites[SelectStoryData.Instance.id[i]];
                houses[i].sprite = StorySimulator.Instance.SelectHouses[i];
            }
        }
    }
}
