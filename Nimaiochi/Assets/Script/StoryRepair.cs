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

        private List<GameObject> pageContentList;

        private void Start() // 後で消す
        {
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
            pageContent.gameObject.SetActive(false);
        }

    }

    public class PageContentData
    {
        string clause;
    }
}
