using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        Button m_button;
        [SerializeField]
        string loadSceneName = "";

        void Awake()
        {
            m_button = GetComponent<Button>();


            m_button.onClick.AddListener(() =>
            {
                LoadSceneManager.Instance.LoadSceneAsync(loadSceneName, Color.black, null);
            });
        }
    }
}