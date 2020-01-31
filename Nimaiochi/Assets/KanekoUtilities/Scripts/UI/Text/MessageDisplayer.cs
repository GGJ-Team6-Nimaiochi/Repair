using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class MessageDisplayer : SingletonMonobehaviour<MessageDisplayer>
    {
        [Serializable]
        public struct FontSettings
        {
            public Color color;
            public int fontSize;
            public TextAnchor anchor;
            public UIAnimation showAnimation;
            public UIAnimation hideAnimation;
            public float showAnimationTime;
            public float hideAnimationTime;
            public float limitLife;

            public FontSettings(IUIText text)
            {
                color = text.Color;
                fontSize = text.FontSize;
                anchor = text.Alignment;
                showAnimation = UIAnimationUtil.DefaultShowAnimation;
                hideAnimation = UIAnimationUtil.DefaultHideAnimation;
                showAnimationTime = 0.4f;
                hideAnimationTime = 0.3f;
                limitLife = 0.2f;
            }
        }

        [SerializeField]
        UGUITextPool textPool = null;
        [SerializeField]
        TextMeshPool textMeshPool = null;

        public FontSettings DefaultUGUIFontSettings { get; private set; }
        public FontSettings Default3DFontSettings { get; private set; }

        [SerializeField]
        Camera uiCamera = null;

        protected override void Awake()
        {
            base.Awake();

            DefaultUGUIFontSettings = new FontSettings(textPool.GetOriginal);
            Default3DFontSettings = new FontSettings(textMeshPool.GetOriginal);

            if (uiCamera == null) uiCamera = Camera.main;
        }

        #region ShowMessage引数違い

        /// <summary>
        /// 指定された場所にデフォルトの設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 position)
        {
            ShowMessage(message, position, Quaternion.identity, DefaultUGUIFontSettings);
        }
        /// <summary>
        /// 指定された場所にデフォルトの設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 position, Quaternion rotation)
        {
            ShowMessage(message, position, rotation, DefaultUGUIFontSettings);
        }
        /// <summary>
        /// 指定された場所にデフォルトの設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 position, Vector3 eulerAngle)
        {
            ShowMessage(message, position, Quaternion.Euler(eulerAngle), DefaultUGUIFontSettings);
        }

        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 position, FontSettings settings)
        {
            ShowMessage(message, position, Quaternion.identity, settings);
        }
        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 position, Vector3 eulerAngle, FontSettings settings)
        {
            ShowMessage(message, position, Quaternion.Euler(eulerAngle), settings);
        }
        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 position, Quaternion rotation, FontSettings settings)
        {
            ShowMessage(message, position, rotation, textPool.transform, settings);
        }

        /// <summary>
        /// 指定された場所にデフォルトの設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector3 position)
        {
            ShowMessage(message, position, Quaternion.identity, DefaultUGUIFontSettings);
        }
        /// <summary>
        /// 指定された場所にデフォルトの設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector3 position, Vector3 eulerAngle)
        {
            ShowMessage(message, position, Quaternion.Euler(eulerAngle), DefaultUGUIFontSettings);
        }
        /// <summary>
        /// 指定された場所にデフォルトの設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector3 position, Quaternion rotation)
        {
            ShowMessage(message, position, rotation, DefaultUGUIFontSettings);
        }

        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector3 position, FontSettings settings)
        {
            ShowMessage(message, position, Quaternion.identity, settings);
        }
        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector3 position, Vector3 eulerAngle, FontSettings settings)
        {
            ShowMessage(message, position, Quaternion.Euler(eulerAngle), settings);
        }

        #endregion

        #region ShowMessage3D引数違い

        /// <summary>
        /// 指定された場所にデフォルトの設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position)
        {
            ShowMessage3D(message, position, Quaternion.identity, Default3DFontSettings);
        }
        /// <summary>
        /// 指定された場所にデフォルトの設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position, Vector3 eulerAngle)
        {
            ShowMessage3D(message, position, Quaternion.Euler(eulerAngle), Default3DFontSettings);
        }
        /// <summary>
        /// 指定された場所にデフォルトの設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position, Quaternion rotation)
        {
            ShowMessage3D(message, position, rotation, Default3DFontSettings);
        }

        /// <summary>
        /// 指定された場所に指定された設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position, FontSettings settings)
        {
            ShowMessage3D(message, position, Quaternion.identity, settings);
        }
        /// <summary>
        /// 指定された場所に指定された設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position, Vector3 eulerAngle, FontSettings settings)
        {
            ShowMessage3D(message, position, Quaternion.Euler(eulerAngle), settings);
        }
        /// <summary>
        /// 指定された場所に指定された設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position, Quaternion rotation, FontSettings settings)
        {
            ShowMessage3D(message, position, rotation, textMeshPool.transform, settings);
        }

        #endregion

        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector2 localPosition, Quaternion localRotation, Transform parent, FontSettings settings)
        {
            AbstractUGUIText text = textPool.GetInstance();
            InitializeUGUIText(text, message, settings);
            text.RectTransform.SetParent(parent);
            text.RectTransform.anchoredPosition = localPosition;
            text.RectTransform.localRotation = localRotation;


            StartCoroutine(settings.showAnimation.GetAnimation(text, settings.showAnimationTime).OnCompleted(() =>
            {
                if (settings.limitLife <= 0.0f)
                {
                    StartCoroutine(settings.hideAnimation.GetAnimation(text, settings.hideAnimationTime).OnCompleted(() =>
                    {
                        textPool.ReturnInstance(text);
                    }));
                }
                else
                {
                    KKUtilities.Delay(settings.limitLife, () =>
                    {
                        StartCoroutine(settings.hideAnimation.GetAnimation(text, settings.hideAnimationTime).OnCompleted(() =>
                        {
                            textPool.ReturnInstance(text);
                        }));
                    }, this);
                }
            }));
        }

        //3D座標を指定するUGUIの場合は親を設定するメリットがないので用意しない
        /// <summary>
        /// 指定された場所に指定された設定でUGUIのメッセージを表示する
        /// </summary>
        public void ShowMessage(string message, Vector3 position, Quaternion rotation, FontSettings settings)
        {
            AbstractUGUIText text = textPool.GetInstance();
            text.transform.SetPositionAndRotation(uiCamera.WorldToScreenPoint(position), rotation);

            InitializeUGUIText(text, message, settings);

            StartCoroutine(settings.showAnimation.GetAnimation(text, settings.showAnimationTime).OnCompleted(() =>
            {
                if (settings.limitLife <= 0.0f)
                {
                    StartCoroutine(settings.hideAnimation.GetAnimation(text, settings.hideAnimationTime).OnCompleted(() =>
                    {
                        textPool.ReturnInstance(text);
                    }));
                }
                else
                {
                    KKUtilities.Delay(settings.limitLife, () =>
                    {
                        StartCoroutine(settings.hideAnimation.GetAnimation(text, settings.hideAnimationTime).OnCompleted(() =>
                        {
                            textPool.ReturnInstance(text);
                        }));
                    }, this);
                }
            }));
        }

        /// <summary>
        /// 指定された場所に指定された設定で３Dのメッセージを表示する
        /// </summary>
        public void ShowMessage3D(string message, Vector3 position, Quaternion rotation, Transform parent, FontSettings settings)
        {
            AbstractTextMesh text = textMeshPool.GetInstance();

            InitializeTextMesh(text, message, settings);

            text.transform.SetParent(parent);
            text.transform.SetPositionAndRotation(position, rotation);

            StartCoroutine(settings.showAnimation.GetAnimation(text, settings.showAnimationTime).OnCompleted(() =>
            {
                if (settings.limitLife <= 0.0f)
                {
                    StartCoroutine(settings.hideAnimation.GetAnimation(text, settings.hideAnimationTime).OnCompleted(() =>
                    {
                        textMeshPool.ReturnInstance(text);
                    }));
                }
                else
                {
                    KKUtilities.Delay(settings.limitLife, () =>
                    {
                        StartCoroutine(settings.hideAnimation.GetAnimation(text, settings.hideAnimationTime).OnCompleted(() =>
                        {
                            textMeshPool.ReturnInstance(text);
                        }));
                    }, this);
                }
            }));
        }

        void InitializeUIText(IUIText text, string message, FontSettings settings)
        {
            text.Text = message;
            text.Color = settings.color;
            text.FontSize = settings.fontSize;
            text.Alignment = settings.anchor;
        }
        void InitializeUGUIText(AbstractUGUIText text, string message, FontSettings settings)
        {
            InitializeUIText(text, message, settings);
            text.transform.localScale = Vector3.one;
            text.gameObject.SetActive(true);
        }
        void InitializeTextMesh(AbstractTextMesh text, string message, FontSettings settings)
        {
            InitializeUIText(text, message, settings);
            text.transform.localScale = Vector3.one;
            text.gameObject.SetActive(true);
        }
    }
}
