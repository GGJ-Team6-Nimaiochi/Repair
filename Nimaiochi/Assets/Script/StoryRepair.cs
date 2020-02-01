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
                var page = Instantiate(textContent, textParent.transform);
                textContentList.Add(page);
            }
            textContent.SetActive(false);
        }

    }

    public class PageContentData
    {
        string text;

    }
}
