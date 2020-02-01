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
        public enum CellType { Text, Select, SelectText, Lock }
        public class Cell
        {
            public string ViewText;
            public CellType Type;
            public Cell(string viewText, CellType type)
            {
                ViewText = viewText;
                Type = type;
            }
        }

        [SerializeField] GameObject PageParent;
        [SerializeField] GameObject pageContent;
        [SerializeField] GameObject textParent;
        [SerializeField] GameObject textContent;
        [SerializeField] GameObject nonSelectTextContent;
        [SerializeField] Button nextButton;
        [SerializeField] private GameObject uGuiButton3D;
        [SerializeField] private KanekoUtilities.Panel stroyRepairPanel; 
        [SerializeField] private StorySimulator stroySimulator; 

        //選択肢を確定した数
        private int selectNum;
        //選択肢を埋める場所の数
        private int dropFildNum;
        //チェックポイントのIndex
        private int currentChapter;

        Cell[] cells;

        private void Start() // 後で消す
        {
            currentChapter = 0;
            Init();

            nextButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    nextButton.gameObject.SetActive(false);
                    AddChapter();
                    Init();
                }).AddTo(this);
        }

        public void AddChapter()
        {
            currentChapter++;
        }

        public void Init()
        {
            selectNum = 0;

            DeleteContent();

            ChapterTextDataToCells();

            //選択肢が存在しなかったら
            if(dropFildNum == 0)
            {
                nextButton.gameObject.SetActive(true);
            }

            CreatPageList(CsvDataInputScript.Instance.CardsCsvDatas[currentChapter].Length);

            nonSelectTextContent.SetActive(true);
            textContent.SetActive(true);
            var dropFildIndex = 0;
            for(int i = 0 ; i < cells.Length ; i++)
            {
                if(cells[i].Type == CellType.Select || cells[i].Type == CellType.SelectText)
                {
                    CreatDropFildList(dropFildIndex, cells[i].ViewText);
                    dropFildIndex++;
                    continue;
                }
                var pagetext = Instantiate(nonSelectTextContent, textParent.transform);
                pagetext.transform.GetComponent<Text>().text = CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter][i];
            }
            nonSelectTextContent.SetActive(false);
            textContent.SetActive(false);


            var storyData = SelectStoryData.Instance;
            storyData.Init(cells.Length, currentChapter);
            for(int i = 0 ; i < cells.Length ; i++)
            {
                storyData.SetData(i, cells[i].ViewText, -1);
            }
        }

        private void DeleteContent()
        {
            var childCount = PageParent.transform.childCount;
            for(int i = childCount - 1 ; i >= 0 ; i--)
            {
                Destroy(PageParent.transform.GetChild(i).gameObject);
            }

            childCount = textParent.transform.childCount;
            for(int i = childCount - 1 ; i >= 0 ; i--)
            {
                Destroy(textParent.transform.GetChild(i).gameObject);
            }
        }

        //テキストデータを読み込んでCellの配列へ変換
        private void ChapterTextDataToCells()
        {
            var dataList = CsvDataInputScript.Instance.MystoryCsvDatas[currentChapter];
            cells = new Cell[dataList.Length];
            dropFildNum = 0;

            for(int i = 0 ; i < dataList.Length ; i++)
            {
                var data = dataList[i];
                var type = CellType.Text;

                if(data.Contains("SELECT"))
                {
                    type = data == "SELECT" ? CellType.Select : CellType.SelectText;
                    data = data.Replace("SELECT", "");
                    dropFildNum++;
                }
                else if(data.Contains("<color"))
                {
                    type = CellType.Lock;
                }
                cells[i] = new Cell(data, type);
            }
        }

        //選択肢のカードを生成する
        private void CreatPageList(int selectPointNum)
        {
            if(selectPointNum == 1) return;
            pageContent.SetActive(true);
            for(int i = 0 ; i < selectPointNum ; i++)
            {
                var page = Instantiate(pageContent, PageParent.transform);
                page.transform.GetComponent<DragManage>().SetPageContentData(new PageContentData(CsvDataInputScript.Instance.CardsCsvDatas[currentChapter][i], currentChapter, i));
            }
            pageContent.SetActive(false);
        }

        //選択肢を入れる部分を生成
        private void CreatDropFildList(int dropFildIndex, string text = "")
        {
            var pagetext = Instantiate(textContent, textParent.transform);
            pagetext.transform.GetComponent<DropArea>().SetData(dropFildIndex, text, SelectText);
        }

        //選択肢をドロップした後の挙動
        public void SelectText()
        {
            selectNum++;

            //全て選択し終わったらNEXTを表示
            if(selectNum >= dropFildNum)
            {
                nextButton.gameObject.SetActive(true);
                uGuiButton3D.SetActive(true);
            }
        }

        private int key = 0;
        public void OnPushPlay()
        {
            try
            {
                switch (StorySimulator.Instance.Phase)
                {
                    case 0:
                        StorySimulator.Instance.Chapter = Instantiate(StorySimulator.Instance.ChaptersSelections[0][0]);
                        break;
                    default:
                        StorySimulator.Instance.Chapter = Instantiate(StorySimulator.Instance.ChaptersSelections[StorySimulator.Instance.Phase][SelectStoryData.Instance.id[key]]);
                        break;
                    
                }

                StorySimulator.Instance.IsStory = true;
                StorySimulator.Instance.SetStoryText(key);
                StorySimulator.Instance.Phase++;
                key++;
                stroyRepairPanel.Deactivate();
                uGuiButton3D.SetActive(false);
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
