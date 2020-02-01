using System;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;

public class StorySimulator : MonoBehaviour
{
    [SerializeField] private GameObject[] chapter1Selections;
    [SerializeField] private GameObject[] chapter2Selections;
    [SerializeField] private GameObject[] chapter3Selections;
    [SerializeField] private GameObject[] chapter4Selections;
    [SerializeField] private GameObject[] chapter5Selections;
    [SerializeField] private GameObject[] chapter6Selections;
    [SerializeField] private List<GameObject[]> chaptersSelections = new List<GameObject[]>();
    private int phase = 0;
    private int key = 0;
    public static GameObject Chapter = null;
    
    private void Awake()
    {
        key = 0;
        chaptersSelections.Add(chapter1Selections);
        chaptersSelections.Add(chapter2Selections);
        chaptersSelections.Add(chapter3Selections);
        chaptersSelections.Add(chapter4Selections);
        chaptersSelections.Add(chapter5Selections);
        chaptersSelections.Add(chapter6Selections);
    }

    public void OnPushPlay()
    {
        try
        {
            Chapter = Instantiate(chaptersSelections[phase][key]);
            phase++;
        }
        catch
        {
            Debug.LogError("Error");
        }
    }
}