using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {
    
    Item item;
    
    void OnEnable() {
        item = target as Item;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector ();
        
        EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Save Changes")) {
                item.SaveChanges();
            }

            if(GUILayout.Button("Update Current Data")) {
                item.UpdateCurrentData();
            }
        EditorGUILayout.EndHorizontal();
    }
}
