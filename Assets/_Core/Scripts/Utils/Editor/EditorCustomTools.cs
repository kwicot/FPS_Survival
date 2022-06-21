using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Editor
{
    public class EditorCustomTools
    {
        [MenuItem("Tools/Destroy disabled")]
        public static void DestroyDisabledObjects()
        {
            var objects = GetAllObjects();
            foreach (var gameObject in objects)
            {
                // if(gameObject.activeSelf == false)
                //     UnityEngine.Object.DestroyImmediate(gameObject);
                if(gameObject != null && gameObject.activeSelf == false)
                {
                    if (PrefabUtility.GetCorrespondingObjectFromSource(gameObject.transform.parent) == null)
                        UnityEngine.Object.DestroyImmediate(gameObject);
                }
            }

            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        static List<GameObject> GetAllObjects()
        {
            var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            var allGameObjects = new List<GameObject>();
            foreach (var rootGameObject in rootGameObjects)
                allGameObjects.AddRange(GetChilds(rootGameObject));
            return allGameObjects;
        }
        static List<GameObject> GetChilds(GameObject target)
        {
            var list = new List<GameObject>();
            list.Add(target);
            if (target.transform.childCount > 0)
            {
                for (int i = 0; i < target.transform.childCount; i++)
                {
                    list.AddRange(GetChilds(target.transform.GetChild(i).gameObject));
                }
            }

            return list;
        }
    }
}