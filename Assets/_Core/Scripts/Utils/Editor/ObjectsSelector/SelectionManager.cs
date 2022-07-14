using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : UnityEditor.Editor
{
    void Start()
    {
        
    }

    public static void SelectByName(string name,int layer)
    {
        var objects = GetAllObjects();
        var finded = GetByName(objects, name,layer);
        
        Selection.objects = finded.ToArray();
    }

    public static void SelectByComponentName(string componentName,int layer)
    {
        var objects = GetAllObjects();
        var finded = GetByComponentName(objects, componentName, layer);
    }

    static List<GameObject> GetByName(List<GameObject> origin,string name,int layer)
    {
        var list = new List<GameObject>();
        foreach (var gameObject in origin)
        {
            if(gameObject.name.Contains(name) && gameObject.transform.Parents() == layer)
                list.Add(gameObject);
        }

        return list;
    }
    static List<GameObject> GetByComponentName(List<GameObject> origin,string name,int layer)
    {
        var list = new List<GameObject>();
        foreach (var gameObject in origin)
        {
            List<Component> components = new List<Component>();
            gameObject.GetComponents(components);
            foreach (var component in components)
            {
                if(component.name == name && gameObject.transform.Parents() == layer)
                    list.Add(gameObject);
            }
        }

        return list;
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

    void Update()
    {
        
    }
}
