using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Cainos.PixelArtPlatformer_Dungeon
{

    [CustomEditor(typeof(Switch))]
    public class SwitchEditor : Editor
    {
        private Switch instance;

        private PropertyField IsOn;

        private void OnEnable()
        {
            instance = target as Switch;

            IsOn = ExposeProperties.GetProperty("IsOn", instance);
        }


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ExposeProperties.Expose(IsOn);

            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * 10);
            if (GUILayout.Button("Turn On")) instance.TurnOn();
            if (GUILayout.Button("Turn Off")) instance.TurnOff();
            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            if (Application.isPlaying == false && GUI.changed)
            {
                EditorUtility.SetDirty(instance);
                EditorSceneManager.MarkSceneDirty(instance.gameObject.scene);
            }
        }
    }
}
