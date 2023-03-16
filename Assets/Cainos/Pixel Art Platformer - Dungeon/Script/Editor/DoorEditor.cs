using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Cainos.PixelArtPlatformer_Dungeon
{


    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        private Door instance;

        private PropertyField IsOpened;

        private void OnEnable()
        {
            instance = target as Door;

            IsOpened = ExposeProperties.GetProperty("IsOpened", instance);


        }


        public override void OnInspectorGUI()
        {
            if (instance == null) return;
            serializedObject.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space();

            ExposeProperties.Expose(IsOpened);

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * 10);
            if (GUILayout.Button("Open")) instance.Open();
            if (GUILayout.Button("Close")) instance.Close();
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            if ( Application.isPlaying == false && GUI.changed)
            {
                EditorUtility.SetDirty(instance);
                EditorSceneManager.MarkSceneDirty(instance.gameObject.scene);
            }
        }
    }
}
