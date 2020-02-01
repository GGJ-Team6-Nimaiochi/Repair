using UnityEngine;

namespace KanekoUtilities
{
    public static class PositiveWords
    {
        static int currentIndex;
        static string[] words = {
            "gorgeous", "awesome", "fantastic", "marvelous",
            "perfect", "wonderful", "great"
        };

        public static string GetWord()
        {
            if (currentIndex >= words.Length) currentIndex = 0;
            return words[currentIndex++];
        }
    }

    public class PositiveWordDisplayer : SingletonMonobehaviour<PositiveWordDisplayer>
    {
        //[SerializeField]
        //AbstractUGUIText uguiFontSettingsOriginal = null;
        //[SerializeField]
        //AbstractTextMesh textMeshFontSettingsOriginal = null;

        //MessageDisplayer.FontSettings uguiSettings;
        //MessageDisplayer.FontSettings textMeshSettings;

        //protected override void Awake()
        //{
        //    base.Awake();

        //    if(uguiFontSettingsOriginal != null) uguiSettings = new MessageDisplayer.FontSettings(uguiFontSettingsOriginal);
        //    if(textMeshFontSettingsOriginal != null) textMeshSettings = new MessageDisplayer.FontSettings(textMeshFontSettingsOriginal);
        //}

        //以下プロジェクトに合わせて要拡張
    }
}