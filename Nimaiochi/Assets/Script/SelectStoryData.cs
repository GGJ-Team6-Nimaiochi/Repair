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

    public void Init(int arrayNum)
    {
        text = null;
        chapter = -1;
        id = null;
        text = new string[arrayNum];
        id = new int[arrayNum];
    }

    public void SetData(int arrayNo,string text,int chapter,int id)
    {
        this.text[arrayNo] = text;
        this.chapter = chapter;
        this.id[arrayNo] = id;
    }


    private void OnDestroy()
    {
        text = null;
        id = null;
    }
}
