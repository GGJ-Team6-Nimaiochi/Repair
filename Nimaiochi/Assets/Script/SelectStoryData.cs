using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStoryData : MonoBehaviour
{
    static public SelectStoryData Instance = null;

    [HideInInspector] public string[] text;
    [HideInInspector] public int chapter;
    [HideInInspector] public int[] id;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Init(int arrayNum, int chapter)
    {
        text = new string[arrayNum];
        this.chapter = chapter;
        id = new int[arrayNum];
        text = new string[arrayNum];
        id = new int[arrayNum];
    }

    public void SetData(int index,string text,int id)
    {
        this.text[index] = text;
        this.id[index] = id;
    }

    public void SetID(int index, int id)
    {
        this.id[index] = id;
    }

    private void OnDestroy()
    {
        text = null;
        id = null;
    }
}
