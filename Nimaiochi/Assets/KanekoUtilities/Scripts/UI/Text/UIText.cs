using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KanekoUtilities
{
    public interface IUIText : IUIParts
    {
        string Text { get; set; }
        int FontSize { get; set; }
        TextAnchor Alignment { get; set; }
    }

    public abstract class AbstractUGUIText : UGUIParts, IUIText
    {
        public abstract string Text { get; set; }
        public abstract int FontSize { get; set; }
        public abstract TextAnchor Alignment { get; set; }
    }

    public abstract class AbstractTextMesh : UIParts3D, IUIText
    {
        public abstract string Text { get; set; }
        public abstract int FontSize { get; set; }
        public abstract TextAnchor Alignment { get; set; }
    }

    public static class TextUtil
    {
        public static TextAnchor TextOptionToTextAnchor(TextAlignmentOptions options)
        {
            switch (options)
            {
                case TextAlignmentOptions.TopLeft: return TextAnchor.UpperLeft;
                case TextAlignmentOptions.Top: return TextAnchor.UpperCenter;
                case TextAlignmentOptions.TopRight: return TextAnchor.UpperRight;
                case TextAlignmentOptions.Left: return TextAnchor.MiddleLeft;
                case TextAlignmentOptions.Center: return TextAnchor.MiddleCenter;
                case TextAlignmentOptions.Right: return TextAnchor.MiddleRight;
                case TextAlignmentOptions.BottomLeft: return TextAnchor.LowerLeft;
                case TextAlignmentOptions.Bottom: return TextAnchor.LowerCenter;
                case TextAlignmentOptions.BottomRight: return TextAnchor.LowerRight;
            }
            
            return TextAnchor.UpperLeft;
        }
        public static TextAlignmentOptions TextAnchorToTextOption(TextAnchor anchor)
        {
            switch (anchor)
            {
                case TextAnchor.UpperLeft: return TextAlignmentOptions.TopLeft;
                case TextAnchor.UpperCenter: return TextAlignmentOptions.Top;
                case TextAnchor.UpperRight: return TextAlignmentOptions.TopRight;
                case TextAnchor.MiddleLeft: return TextAlignmentOptions.Left;
                case TextAnchor.MiddleCenter: return TextAlignmentOptions.Center;
                case TextAnchor.MiddleRight: return TextAlignmentOptions.Right;
                case TextAnchor.LowerLeft: return TextAlignmentOptions.BottomLeft;
                case TextAnchor.LowerCenter: return TextAlignmentOptions.Bottom;
                case TextAnchor.LowerRight: return TextAlignmentOptions.BottomRight;
            }

            return TextAlignmentOptions.TopLeft;
        }
    }
}