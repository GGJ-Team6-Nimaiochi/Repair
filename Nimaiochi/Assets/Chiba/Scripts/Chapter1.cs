using System;
using UnityEngine;
using UnityEngine.UI;

public class Chapter1 : Chapter
{
    [SerializeField] private SpriteRenderer[] houses;

    // 各子豚の家の画像を変える
    private void Start()
    {
        if (houses.Length == SelectStoryData.Instance.id.Length)
        {
            for (int i = 0; i < houses.Length; i++)
            {
                houses[i].sprite = HouseSprites[SelectStoryData.Instance.id[i]];
                StorySimulator.Instance.SelectHouses[i] = HouseSprites[SelectStoryData.Instance.id[i]];
            }
        }

    }
}
