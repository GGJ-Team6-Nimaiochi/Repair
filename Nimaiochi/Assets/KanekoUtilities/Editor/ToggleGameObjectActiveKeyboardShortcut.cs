using UnityEditor;

namespace KanekoUtilities
{
    public static class ToggleGameObjectActiveKeyboardShortcut
    {
        public static bool IsAvailable()
        {
            return Selection.activeGameObject != null;
        }

        public static void Execute()
        {
            foreach (var go in Selection.gameObjects) {
                Undo.RecordObject(go, go.name + ".activeSelf");
                go.SetActive(!go.activeSelf);
            }
        }
    }
}