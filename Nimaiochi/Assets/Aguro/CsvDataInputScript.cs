using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvDataInputScript : MonoBehaviour
{

    TextAsset csvFile; // CSVファイル
    public int MystoryHeight; // CSVの行数
    public int CardsHeight; // CSVの行数
    public int MystoryWidth; // Debug.Logで表示するCSVの列数(自分で設定しなければならないです)
    public int CardsWidth; // Debug.Logで表示するCSVの列数(自分で設定しなければならないです)
    List<string[]> MystoryCsvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    List<string[]> CardsCsvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    static CsvDataInputScript _instance = null;
    public static CsvDataInputScript instance { get { return _instance; } }


    void Start()
    {
        if (_instance == null)
        {
            // マネージャーリストにセット
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        csvFile = Resources.Load("MYSTORY1") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() > -1) // reader.Peaekが0になるまで繰り返す
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            MystoryCsvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            MystoryHeight++;
        }

        csvFile = Resources.Load("CARDS1") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader2 = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader2.Peek() > -1) // reader.Peaekが0になるまで繰り返す
        {
            string line = reader2.ReadLine(); // 一行ずつ読み込み
            CardsCsvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            CardsHeight++; // 行数加算
        }
        // csvDatas[行][列]を指定して値を自由に取り出せる
        //for (int i = 0; i < MystoryHeight; i++)
        //{
        //    for (int j = 0; j < MystoryWidth; j++)
        //    {
        //        Debug.Log("MystoryCsvDatas[" + i + "][" + j + "]=" + MystoryCsvDatas[i][j]);
        //    }
        //}
        //for (int i = 0; i < CardsHeight; i++)
        //{
        //    for (int j = 0; j < CardsWidth; j++)
        //    {
        //        Debug.Log("CardsCsvDatas[" + i + "][" + j + "]=" + CardsCsvDatas[i][j]);
        //    }
        //}
        //Debug.Log(MystoryCsvDatas[0][0]);

    }
}
