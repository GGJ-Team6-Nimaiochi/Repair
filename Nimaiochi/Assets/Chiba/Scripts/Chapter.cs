using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class Chapter : MonoBehaviour
{
    [SerializeField] private Sprite[] houseSprites;
    [SerializeField] private Image buta1HouseImage;
    [SerializeField] private Image buta2HouseImage;
    [SerializeField] private Image buta3HouseImage;
    [SerializeField] private Animator animator;
    
    private void Awake()
    {
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            Destroy(animator);
        }).AddTo(this);
    }
}
