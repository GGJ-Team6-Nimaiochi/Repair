using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWave : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
