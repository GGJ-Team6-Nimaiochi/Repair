using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KanekoUtilities
{
    public class DeletePlayerPrefsData : MonoBehaviour
    {
        [MenuItem("KanekoUtilities/DeleteAllSaveData")]
        public static void DeleteAll()
        {
            MyPlayerPrefs.DeleteAll();
        }

        [MenuItem("KanekoUtilities/DeleteLevelSaveData")]
        public static void DeleteLevelData()
        {
            MyPlayerPrefs.Delete("CurrentLevel");
        }
    }
}