using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace MyStory.StoryRepair
{

    public class StoryRepair : MonoBehaviour
    {
        [SerializeField] GameObject PageParent;
        [SerializeField] GameObject pageContent;
        [SerializeField] GameObject textParent;
        [SerializeField] GameObject textContent;
        [SerializeField] Button nextButton;

        private List<GameObject> pageContentList;
        private List<GameObject> textContentList;

        private int selectNum;

        private int currentChapter;

        private int currentPageContent;
        private int currentTextContent;

        private void Start() // 後で消す
        {
            currentChapter = 0;
            currentPageContent = 0;
            currentTextContent = 0;
            selectNum = 0;
            CheckChapterTextData();
            CreatDropFildList();
            CreatPageList();
            nextButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (currentPageContent == 0)
                    {
                        DestroyPage();
                        AddChapter();
                        CheckChapterTextData();
                        CreatDropFildList();
                        CreatPageList();
                        nextButton.gameObject.SetActive(false);
                    }
                }).AddTo(this);
        }

        public void AddChapter()
        {
            currentChapter++;
        }

        private void CheckChapterTextData()
        {
            for (int i = 0; i < CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter].Length; i++)
            {
                if (CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i] == "END")
                {
                    currentTextContent = i;
                    break;
                }
                currentTextContent = i - 1;
            }

            for (int j = 0; j < CsvDataInputScript.Instance.CardsCsvDatas[currentChapter].Length; j++)
            {
                if (CsvDataInputScript.Instance.CardsCsvDatas[currentChapter][j] == "END")
                {
                    currentPageContent = j;
                    if (j == 0)
                        nextButton.gameObject.SetActive(true);
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

        private void CreatDropFildList()
        {
            textContentList = new List<GameObject>();
            textContent.SetActive(true);
            for (int i = 0; i < currentTextContent; i++)
            {
                var pagetext = Instantiate(textContent, textParent.transform);
                pagetext.transform.GetComponent<DropArea>().SetData(i, CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i]);
                textContentList.Add(pagetext);
            }
            textContent.SetActive(false);
        }

        public void DestroySelectText()
        {
            foreach (var obj in pageContentList.ToArray())
            {
                Destroy(obj);
            }
            pageContentList = null;
        }

        public void DestroyPage()
        {
            if(pageContentList != null)
            {
                foreach (var obj in pageContentList.ToArray())
                {
                    Destroy(obj);
                }
                pageContentList = null;
            }
           
            foreach (var obj in textContentList.ToArray())
            {
                Destroy(obj);
            }
            textContentList = null;
        }

        public void SelectText()
        {
            selectNum++;
            if(selectNum == currentTextContent)
                nextButton.gameObject.SetActive(true);
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
