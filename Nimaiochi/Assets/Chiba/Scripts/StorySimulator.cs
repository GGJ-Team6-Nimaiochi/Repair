using System;
using UnityEngine;
using System.Collections.Generic;
using MyStory.StoryRepair;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class StorySimulator : MonoBehaviour
{
    [HideInInspector] public bool IsStory = false;
    [HideInInspector] public bool PlayAll = false;
    [HideInInspector] public int Phase = 0;
    [HideInInspector] public Sprite[] SelectHouses = new Sprite[3];
    [HideInInspector] public GameObject Chapter = null;
    [HideInInspector] public Action PageEndAction = null;

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

        this.UpdateAsObservable().Where(_ => IsStory && PlayAll && Input.GetMouseButtonDown(0)).Subscribe(_ => SetStoryText(id)).AddTo(this);
        this.UpdateAsObservable().Where(_ => IsStory && !PlayAll && Input.GetMouseButtonDown(0)).Subscribe(_ => SetNextStory()).AddTo(this);
    }

    // ストーリー表示中の文字処理
    public void SetStoryText(int key)
    {
        if (key >= SelectStoryData.Instance.text.Length)
        {
            storyText.text = "(ページをめくってね！)";
            KanekoUtilities.SwipeGetter.Instance.CanTouch = true;
            if (PageEndAction != null)
                PageEndAction();
            if (Chapter)Destroy(Chapter.transform.GetChild(0).GetComponent<Animator>());
            id++;
            return;
        }
        
        Debug.LogError(key);
        if(!storyText.transform.parent.gameObject.activeSelf)storyText.transform.parent.gameObject.SetActive(true);
        storyText.text = SelectStoryData.Instance.text[key];
        id++;
    }

    // ストーリー表示中の文字処理
    public void SetNextStory()
    {
        if (SelectStoryData.Instance.id[id] == -1)
        {
            SetStoryText(id);
            return;
        }

        if (id >= SelectStoryData.Instance.text.Length)
        {
            storyText.text = "(ページをめくってね！)";
            if (PageEndAction != null)
                PageEndAction();
            if (Chapter)Destroy(Chapter.transform.GetChild(0).GetComponent<Animator>());
            id++;
            return;
        }
        
        var animator = Chapter.transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("isClose", true);
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ => Destroy(animator.transform.parent.gameObject)).AddTo(this);
        Chapter = Instantiate(ChaptersSelections[Phase][SelectStoryData.Instance.id[id]]);
        Chapter.name = "Chapter_" + id;
        id++;
    }
    
    // ストーリーのチャプターを見終わってページをめくったら情報リセット
    public void ResetStoryInfo()
    {
        Phase++;
        id = 0;
        IsStory = false;
        PlayAll = false;
        storyText.transform.parent.gameObject.SetActive(false);
        storyText.text = "";
        StoryRepair.Instance.StoryRepairPanel.Activate();
        Destroy(Chapter);
    }
}