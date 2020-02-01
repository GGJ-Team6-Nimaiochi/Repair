using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KanekoUtilities
{
    public class KanekoUtilitiesHierarchyMenu
    {
        const string UIPrefabDirectory = "Assets/KanekoUtilities/Prefabs/UI/";

        [MenuItem("GameObject/KKUtilities/UGUIImage/Image", false, 20)]
        public static void CreateUGUIImage()
        {
            CreateUGUIPartsObj("UGUIImage");
        }

        [MenuItem("GameObject/KKUtilities/UGUIImage/Triangle", false, 20)]
        public static void CreateTriangleImage()
        {
            CreateUGUIPartsObj("TriangleUGUIImage");
        }

        [MenuItem("GameObject/KKUtilities/UGUIImage/Square", false, 20)]
        public static void CreateSquareImage()
        {
            CreateUGUIPartsObj("SquareUGUIImage");
        }

        [MenuItem("GameObject/KKUtilities/UGUIImage/Circle", false, 20)]
        public static void CreateCircleImage()
        {
            CreateUGUIPartsObj("CircleUGUIImage");
        }

        [MenuItem("GameObject/KKUtilities/UGUIImage/RoundedRectangle", false, 20)]
        public static void CreateRoundedRectangleImage()
        {
            CreateUGUIPartsObj("RoundedRectangleUGUIImage");
        }

        [MenuItem("GameObject/KKUtilities/UGUIText/Text", false, 20)]
        public static void CreateUGUIText()
        {
            CreateUGUIPartsObj("UGUIText");
        }

        [MenuItem("GameObject/KKUtilities/UGUIText/TextPro", false, 20)]
        public static void CreateUGUITextPro()
        {
            CreateUGUIPartsObj("UGUITextPro");
        }

        [MenuItem("GameObject/KKUtilities/Panel", false, 20)]
        public static void CreatePanel()
        {
            CreateUGUIPartsObj("Panel");
        }

        [MenuItem("GameObject/KKUtilities/Window", false, 20)]
        public static void CreateWindow()
        {
            CreateUGUIPartsObj("Window");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/Daialog", false, 20)]
        public static void CreateDialog()
        {
            CreateUGUIPartsObj("Dialog");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/OkDaialog", false, 20)]
        public static void CreateOkDialog()
        {
            CreateUGUIPartsObj("OkDialog");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/OkCancelDaialog", false, 20)]
        public static void CreateOkCancelDialog()
        {
            CreateUGUIPartsObj("OkCancelDialog");
        }

        [MenuItem("GameObject/KKUtilities/Dialog/MessageDaialog", false, 20)]
        public static void CreateMessageDialog()
        {
            CreateUGUIPartsObj("MessageDialog");
        }

        [MenuItem("GameObject/KKUtilities/Canvas", false, 20)]
        public static void CreateCanvas()
        {
            CreateObject(UIPrefabDirectory, "Canvas");
        }

        [MenuItem("GameObject/KKUtilities/Text3D/Text", false, 20)]
        public static void CreateText3DUnity()
        {
            CreateObject(UIPrefabDirectory, "Text3D");
        }

        [MenuItem("GameObject/KKUtilities/Text3D/TextPro", false, 20)]
        public static void CreateText3DPro()
        {
            CreateObject(UIPrefabDirectory, "Text3DPro");
        }

        [MenuItem("GameObject/KKUtilities/UGUIButton", false, 20)]
        public static void CreateUGUIButton()
        {
            CreateObject(UIPrefabDirectory, "UGUIButton");
        }

        [MenuItem("GameObject/KKUtilities/UGUIButton3D", false, 20)]
        public static void CreateUGUIButton3D()
        {
            CreateObject(UIPrefabDirectory, "UGUIButton3D");
        }

        static T LoadPrefab<T>(string path, string name) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath(path + name + ".prefab", typeof(T)) as T;
        }

        static void CreateUGUIPartsObj(string name)
        {
            CreateObject(UIPrefabDirectory, name);
        }

        static void CreateObject(string path, string name)
        {
            GameObject prefab = LoadPrefab<GameObject>(path, name);
            Transform parent = Selection.activeGameObject != null ? Selection.activeGameObject.transform : null;
            GameObject obj = Object.Instantiate(prefab, parent);
            obj.name = prefab.name;
            Selection.activeGameObject = obj;
        }
    }
}
