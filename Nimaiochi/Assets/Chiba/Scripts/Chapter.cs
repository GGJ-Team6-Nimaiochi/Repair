using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class Chapter : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Sprite[] HouseSprites;

    private void Awake()
    {
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            Destroy(animator);
        }).AddTo(this);
    }
}
