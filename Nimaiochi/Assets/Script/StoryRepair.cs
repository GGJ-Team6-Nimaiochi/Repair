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

        private List<GameObject> pageContentList;
        private List<GameObject> textContentList;

        private void Start() // 後で消す
        {
            CreatDropFildList();
            CreatPageList();
        }


        private void CreatPageList()
        {
            pageContentList = new List<GameObject>();
            pageContent.SetActive(true);
            for (int i = 0; i < 6; i++)
            {
                var page = Instantiate(pageContent, PageParent.transform);
                page.transform.GetComponent<DragManage>().SetPageContentData(new PageContentData(" " + i, i, i));
                pageContentList.Add(page);
            }
            pageContent.SetActive(false);
        }

        private void CreatDropFildList()
        {
            textContentList = new List<GameObject>();
            textContent.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                var pagetext = Instantiate(textContent, textParent.transform);
                textContentList.Add(pagetext);
            }
            textContent.SetActive(false);
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
