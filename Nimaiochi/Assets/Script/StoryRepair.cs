using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private int currentChapter = 0;

        private int currentPageContent = 0;
        private int currentTextContent = 0;

        private void Start() // 後で消す
        {
            CheckChapterTextData();
            CreatDropFildList();
            CreatPageList();
        }

        public void AddChapter()
        {
            currentChapter++;
        }

        private void CheckChapterTextData()
        {
            for(int i = 0; i < CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter].Length;i++)
            {
                if(CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i] == "END")
                {
                    currentTextContent = i;
                    break;
                }
                currentTextContent = i;
            }

            for (int i = 0; i < CsvDataInputScript.Instance.CardsCsvDatas[currentChapter].Length; i++)
            {
                if (CsvDataInputScript.Instance.CardsCsvDatas[currentChapter][i] == "END")
                {
                    currentPageContent = i;
                    break;
                }
                currentPageContent = i;
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

        public void DestroyPage()
        {
            foreach(var obj in pageContentList.ToArray())
            {
                Destroy(obj);
            }
            pageContentList = null;
            foreach (var obj in textContentList.ToArray())
            {
                Destroy(obj);
            }
            textContentList = null;
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
