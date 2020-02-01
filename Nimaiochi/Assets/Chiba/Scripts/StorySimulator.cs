using System;
using UnityEngine;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class StorySimulator : MonoBehaviour
{
    [HideInInspector] public int Phase = 0;
    [HideInInspector] public GameObject Chapter = null;

    [SerializeField] private GameObject[] chapter1Selections;
    [SerializeField] private GameObject[] chapter2Selections;
    [SerializeField] private GameObject[] chapter3Selections;
    [SerializeField] private GameObject[] chapter4Selections;
    [SerializeField] private GameObject[] chapter5Selections;
    [SerializeField] private GameObject[] chapter6Selections;
    [SerializeField] private GameObject[] ending;

    [SerializeField] private Text storyText;
    
    public List<GameObject[]> ChaptersSelections = new List<GameObject[]>();
    public static StorySimulator Instance = null;
    public bool IsStory = false;
    private int id = 0;
    
    private void Awake()
    {
        if (!Instance) Instance = this;
        
        ChaptersSelections.Add(chapter1Selections);
        ChaptersSelections.Add(chapter2Selections);
        ChaptersSelections.Add(chapter3Selections);
        ChaptersSelections.Add(chapter4Selections);
        ChaptersSelections.Add(chapter5Selections);
        ChaptersSelections.Add(chapter6Selections);
        ChaptersSelections.Add(ending);

        this.UpdateAsObservable().Where(_ => IsStory && Input.GetMouseButtonDown(0)).Subscribe(_ => SetStoryText(id)).AddTo(this);
    }

    public void SetStoryText(int key)
    {
        if (key >= SelectStoryData.Instance.text.Length)
        {
            IsStory = false;
            id = 0;
            
        }
        
        if(!storyText.transform.parent.gameObject.activeSelf)storyText.transform.parent.gameObject.SetActive(true);
        storyText.text = SelectStoryData.Instance.text[key];
        id++;
    }
}