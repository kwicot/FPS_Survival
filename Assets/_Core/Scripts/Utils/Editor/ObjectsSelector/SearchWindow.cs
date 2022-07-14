using UnityEditor;
using UnityEngine;

namespace Utils.Editor.ObjectsSelector
{
    public class SearchWindow : EditorWindow
    
    
    {
        private static string targetName;
        private static int layer;
        private static EditorWindow window;
        
        
        [MenuItem("Tools/Object selector")]
        private static void ShowWindow()
        {
            window = GetWindow<SearchWindow>();
            window.titleContent = new GUIContent("Name");
            window.Show();
            
        }

        private void OnGUI()
        {
            targetName = EditorGUILayout.TextField("Name", targetName);
            layer = EditorGUILayout.IntField("Layer", layer);
            if (GUILayout.Button("Search by object name"))
            {
                SelectionManager.SelectByName(targetName,layer);
                window.Close();
            }

            if (GUILayout.Button("Search by component name"))
            {
                SelectionManager.SelectByComponentName(targetName,layer);
                window.Close();
            }
        }
    }
}