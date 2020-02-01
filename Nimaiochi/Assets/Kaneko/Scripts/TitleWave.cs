using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWave : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;

    [SerializeField]
    float maxX = 1500.0f;

    float direction = 1.0f;

    RectTransform rect;

    void Start()
    {
        rect = transform as RectTransform;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * direction * Time.deltaTime);

        if(Mathf.Abs(rect.anchoredPosition.x) > maxX) direction *= -1.0f;
    }
}
