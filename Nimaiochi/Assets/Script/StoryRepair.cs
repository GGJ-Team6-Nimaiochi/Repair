using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace MyStory.StoryRepair
{

    public class StoryRepair : MonoBehaviour
    {
        [SerializeField] GameObject PageParent;
        [SerializeField] GameObject pageContent;
        [SerializeField] GameObject textParent;
        [SerializeField] GameObject textContent;
        [SerializeField] GameObject nonSelectTextContent;
        [SerializeField] Button nextButton;
        [SerializeField] private GameObject uGuiButton3D;
        [SerializeField] private StorySimulator stroySimulator; 

        public static StoryRepair Instance;
        public KanekoUtilities.Panel StoryRepairPanel; 

        private List<GameObject> pageContentList;
        private List<GameObject> textContentList;
        private List<GameObject> nonSelectTextContentList;

        private int selectTextPoint;

        private int selectNum;

        private int currentChapter;

        private int currentPageContent;
        private int currentTextContent;
        private int currentNonSelectTextContent;

        private void Start() // 後で消す
        {
            Instance = this;
            currentChapter = 0;
            InitNums();
            CheckChapterTextData();
            CreatNonSelectText();
            CreatPageList();
            nextButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (selectTextPoint == -1)
                    {
                        InitNums();
                        nextButton.gameObject.SetActive(false);
                        DestroyPage();
                        AddChapter();
                        CheckChapterTextData();
                        CreatNonSelectText();
                        CreatPageList();
                    }
                    else
                    {
                        nextButton.gameObject.SetActive(false);
                        selectTextPoint = -1;
                        DestroyPage();
                        AddChapter();
                        CheckChapterTextData();
                        CreatNonSelectText();
                        CreatPageList();
                    }
                }).AddTo(this);
        }

        public void AddChapter()
        {
            currentChapter++;
        }

        public void InitNums()
        {
            currentPageContent = 0;
            currentTextContent = 0;
            selectNum = 0;
            selectTextPoint = -1;
        }

        private void CheckChapterTextData()
        {
            for (int i = 0; i < CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter].Length; i++)
            {
                if(CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i] == "SELECT")
                {
                    selectTextPoint = i;
                }

                if (CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i] == "END")
                {
                    currentNonSelectTextContent = i;
                    break;
                }
                currentNonSelectTextContent = i - 1;
            }

            if (selectTextPoint == -1)
            {
                nextButton.gameObject.SetActive(true);
            }

            for (int k = 0; k < CsvDataInputScript.Instance.SelectstoryCsvDatas[currentChapter].Length; k++)
            {
                if (CsvDataInputScript.Instance.SelectstoryCsvDatas[currentChapter][k] == "END")
                {
                    currentTextContent = k;
                    break;
                }
                currentTextContent = k - 1;
            }

            for (int j = 0; j < CsvDataInputScript.Instance.CardsCsvDatas[currentChapter].Length; j++)
            {
                if (CsvDataInputScript.Instance.CardsCsvDatas[currentChapter][j] == "END")
                {
                    currentPageContent = j;
                    break;
                }
                currentPageContent = j - 1;
            }
        }

        private void CreatPageList()
        {
            pageContentList = new List<GameObject>();
            pageContent.SetActive(true);
            for (int i = 0; i < currentPageContent; i++)
            {
                var page = Instantiate(pageContent, PageParent.transform);
                page.transform.GetComponent<DragManage>().SetPageContentData(new PageContentData(CsvDataInputScript.Instance.CardsCsvDatas[currentChapter][i], currentChapter, i));
                pageContentList.Add(page);
            }
            pageContent.SetActive(false);
        }

        //必ず、CreatNonSelectText()の中
        private void CreatDropFildList()
        {
            textContentList = new List<GameObject>();
            for (int i = 0; i < currentTextContent; i++)
            {
                var pagetext = Instantiate(textContent, textParent.transform);
                pagetext.transform.GetComponent<DropArea>().SetData(i, CsvDataInputScript.Instance.SelectstoryCsvDatas[currentChapter][i], SelectText);
                textContentList.Add(pagetext);
            }
            SelectStoryData.Instance.Init(currentTextContent);
        }

        private void CreatNonSelectText()
        {
            nonSelectTextContentList = new List<GameObject>();
            nonSelectTextContent.SetActive(true);
            textContent.SetActive(true);
            for (int i = 0; i < currentNonSelectTextContent; i++)
            {
                if (selectTextPoint == i)
                {
                    CreatDropFildList();
                    continue;
                }
                var pagetext = Instantiate(nonSelectTextContent, textParent.transform);
                pagetext.transform.GetComponent<Text>().text = CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i];
                nonSelectTextContentList.Add(pagetext);
            }
            nonSelectTextContent.SetActive(false);
            textContent.SetActive(false);
        }

        public void DestroySelectText()
        {
            foreach (var obj in nonSelectTextContentList.ToArray())
            {
                Destroy(obj);
            }
            nonSelectTextContentList = null;

            foreach (var obj in pageContentList.ToArray())
            {
                Destroy(obj);
            }
            pageContentList = null;
        }

        public void DestroyPage()
        {
            if (nonSelectTextContentList != null)
            {
                foreach (var obj in nonSelectTextContentList.ToArray())
                {
                    Destroy(obj);
                }
                nonSelectTextContentList = null;
            }

            if (pageContentList != null)
            {
                foreach (var obj in pageContentList.ToArray())
                {
                    Destroy(obj);
                }
                pageContentList = null;
            }

            if (textContentList != null)
            {
                foreach (var obj in textContentList.ToArray())
                {
                    Destroy(obj);
                }
                textContentList = null;
            }
           
        }

        public void SelectText()
        {
            selectNum++;
            if (selectNum == currentTextContent)
            {
                nextButton.gameObject.SetActive(true);
                uGuiButton3D.SetActive(true);
            }
        }

        public void OnPushPlay()
        {
            try
            {
                switch (StorySimulator.Instance.Phase)
                {
                    case 0:
                        StorySimulator.Instance.Chapter = Instantiate(StorySimulator.Instance.ChaptersSelections[0][0]);
                        StorySimulator.Instance.Chapter.name = "Chapter_0";
                        StorySimulator.Instance.PlayAll = true;
                        break;
                    default:
                        StorySimulator.Instance.Chapter = Instantiate(StorySimulator.Instance.ChaptersSelections[StorySimulator.Instance.Phase][SelectStoryData.Instance.id[0]]);
                        StorySimulator.Instance.Chapter.name = "Chapter_" + SelectStoryData.Instance.id[0];
                        StorySimulator.Instance.PlayAll = false;
                        break;
                    
                }
                
                StorySimulator.Instance.IsStory = true;
                StorySimulator.Instance.SetStoryText(0);
                StoryRepairPanel.Deactivate();
                selectTextPoint = -1;
                DestroyPage();
                AddChapter();
                CheckChapterTextData();
                CreatNonSelectText();
                CreatPageList();
                nextButton.gameObject.SetActive(false);
            }
            catch
            {
                Debug.LogError("Error");
            }
        }
    }

    public class PageContentData
    {
        public string text;
        public int chapter;
        public int id;

        public PageContentData(string text, int chapter, int id)
        {
            this.text = text;
            this.chapter = chapter;
            this.id = id;
        }

        public PageContentData(PageContentData pageContentData)
        {
            this.text = pageContentData.text;
            this.chapter = pageContentData.chapter;
            this.id = pageContentData.id;
        }
    }
}
